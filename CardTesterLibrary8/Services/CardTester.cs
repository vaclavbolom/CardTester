using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CardTesterLibrary
{
    public class CardTester : ICardTester
    {

        private const int DELAY_COMMAND = 20;
        private const string POSITION_BASIC = "FLAT";
        private const string POSITION_BENT = "BENT";

        private readonly ILogger _logger;
        private readonly ISerialPortOperator _serialPortOperator;
        private readonly IMeasurementService _measurementService;
        private readonly IMeasurementRecorder _measurementRecorder;

        private string _State;

        public string PortOperatorState { get; private set; }

        public int TestCycleIndex { get; private set; }

        /// <summary>
        /// Creates instance of CardTester
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serialPortOperator"></param>
        /// <param name="measurementService"></param>
        /// <param name="measuremntRecorder"></param>
        public CardTester(ILogger logger, ISerialPortOperator serialPortOperator, IMeasurementService measurementService, IMeasurementRecorder measuremntRecorder)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(serialPortOperator, nameof(serialPortOperator));
            ArgumentNullException.ThrowIfNull(measurementService, nameof(measurementService));
            ArgumentNullException.ThrowIfNull(measuremntRecorder, nameof(measuremntRecorder));

            _logger = logger;
            _serialPortOperator = serialPortOperator;
            _serialPortOperator.RegisterStateChangeMethod(ProcessPortOperatorState);
            _measurementService = measurementService;
            _measurementRecorder = measuremntRecorder;
            _State = CardTesterConstants.STATE_UNKNOWN;

            try
            {
                Task.Run(() => _serialPortOperator.Run(ProcessStateChanged));
                //var task = _serialPortOperator.Run(ProcessStateChanged);
                //task.Wait();
                Task.Delay(100);
                _logger.Debug("CardTester created");
            }
            catch (SerialPortOperatorException e)
            {
                _logger.Error($"Card tester failed to start: {e.Message}");
                _State = CardTesterConstants.STATE_ERROR;
            }
            catch (Exception e)
            {
                _logger.Error($"Card tester failed to start: {e.Message}");
                _State = CardTesterConstants.STATE_ERROR;
            }

        }

        /// <inheritdoc />
        public async Task ResetDownAsync()
        {
            _logger.Debug("ResetDownAsync");
            await Reset(CardTesterConstants.COMMAND_RESET, CardTesterConstants.STATE_UP);
        }

        /// <inheritdoc />
        public async Task ResetUpAsync()
        {
            _logger.Debug("REsetUpAsync");
            await Reset(CardTesterConstants.COMMAND_RESET_UP, CardTesterConstants.STATE_DOWN);
        }

        private async Task Reset(string command, string possibleState)
        {
            _logger.Debug($"Reset, command: {command}, expected state: {possibleState}");
            var movingState = command switch
            {
                CardTesterConstants.COMMAND_RESET => CardTesterConstants.STATE_BACKWARD,
                CardTesterConstants.COMMAND_RESET_UP => CardTesterConstants.STATE_FORWARD,
                _ => throw new CardTesterException($"Cannot reset with command {command}")
            };

            _logger.Debug($"Reset, moving state: {movingState}");
            if (StateEquals(CardTesterConstants.STATE_STOPPED) || StateEquals(CardTesterConstants.STATE_UNKNOWN) || StateEquals(possibleState))
            {
                await _serialPortOperator.RunCommandAsync(command);
                await Task.Delay(DELAY_COMMAND);
                while (StateEquals(movingState))
                {
                    await Task.Delay(DELAY_COMMAND);
                }
            }
            else
            {
                _logger.Information($"Cannot return to desired position position, state = {_State}, moving state: {movingState}");
            }
            return;
        }



        /// <inheritdoc />
        public async Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown, string outputPath, string description, string workOrderId, string jobName, List<int> cardIds)
        {
            _logger.Debug($"RunTestAsync, state: {_State}");
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                _measurementRecorder.StartMeasurement(cardIds);
                var testInterrupted = false;
                var testResult = _measurementService.MeasureCards(0);
                testResult.Position = POSITION_BASIC;
                _measurementRecorder.AddMeasurement(testResult);

                for (int i = 0; i < numberOfCycles; i++)
                {
                    testInterrupted = (_State == CardTesterConstants.STATE_STOPPED || _State == CardTesterConstants.STATE_DOOR_OPEN);
                    _logger.Debug($"RunTestAsync, interrupted: {testInterrupted}, state: {_State}");
                    if (testInterrupted)
                        break;
                    TestCycleIndex = i + 1;
                    await RunMeasurementCycleAsync(delayUp, delayDown, TestCycleIndex);
                }
                if (!testInterrupted)
                    _measurementRecorder.SaveProtocol(outputPath, description, workOrderId, jobName);
            }
            else
            {
                _logger.Information($"Cannot start measurement, state = {_State}");
            }

        }


        /// <inheritdoc />
        public async Task StopAsync()
        {           
            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_GET_STATE);
            await Task.Delay(DELAY_COMMAND);
            var state = _serialPortOperator.GetState();

            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_STOP);
            await Task.Delay(DELAY_COMMAND);
            state = _serialPortOperator.GetState();
            while (state != CardTesterConstants.STATE_STOPPED)
            {
                await Task.Delay(DELAY_COMMAND);
                state = _serialPortOperator.GetState();
            }
        }

        /// <summary>
        /// Runs one measurement cysle
        /// </summary>
        /// <param name="delayUp">delay in bend state [milliseconds]</param>
        /// <param name="delayDown">delay in basic state [milliseconds]</param>
        /// <param name="testCycleIndex"></param>
        /// <returns></returns>
        private async Task RunMeasurementCycleAsync(int delayUp, int delayDown, int testCycleIndex)
        {
            var measurementIndex = (testCycleIndex -  1) * 2 + 1;
            _logger.Debug($"RunMeasurementCycle, cycle index: {testCycleIndex}, measurement index: {measurementIndex}");

            await MoveForwardAsync();
            _logger.Debug($"Run cycle, after forward, state: {_State}");
            await Task.Delay(delayUp);
            var measurementResult = _measurementService.MeasureCards(measurementIndex);
            measurementResult.Position = POSITION_BENT;
            _measurementRecorder.AddMeasurement(measurementResult);

            await MoveBackwardAsync();
            _logger.Debug($"Run cycle, after backward, state: {_State}");
            await Task.Delay(delayDown);
            measurementResult = _measurementService.MeasureCards(measurementIndex + 1);
            measurementResult.Position = POSITION_BASIC;
            _measurementRecorder.AddMeasurement(measurementResult);
        }


        /// <summary>
        /// Bends the card
        /// </summary>
        /// <returns></returns>
        private async Task MoveForwardAsync()
        {
            _logger.Debug($"MoveForward: {_State}");
            //read state
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                await MoveForwardUnsafeAsync();
            }
            else
            {
                _logger.Information($"Cannot move forward, state = {_State}");
            }
            return;
        }

        /// <summary>
        /// Unbends the card
        /// </summary>
        /// <returns></returns>
        private async Task MoveBackwardAsync()
        {
            _logger.Debug($"MoveBackward: {_State}");
            if (StateEquals(CardTesterConstants.STATE_UP))
            {
                await MoveBackwardUnsafeAsync();
            }
            else
            {
                _logger.Information($"MoveBackwardAsync, Cannot move backward, state = {_State}");
            }
            return;
        }

        /// <inheritdoc />
        public async Task MoveForwardUnsafeAsync()
        {
            _logger.Debug($"MoveForwardUnsafeAsync, state: {_State}");
            await MoveUnsafeAsync(CardTesterConstants.COMMAND_MOVE_FORWARD, CardTesterConstants.STATE_FORWARD);
            _logger.Debug($"MoveForwardUnsafe - end, state: {_State}");
        }

        /// <inheritdoc />
        public async Task MoveBackwardUnsafeAsync()
        {
            await MoveUnsafeAsync(CardTesterConstants.COMMAND_MOVE_BACKWARD, CardTesterConstants.STATE_BACKWARD);
        }

        public async Task MoveUnsafeAsync(string command, string movingState )
        {
            var desiredState = (command == CardTesterConstants.COMMAND_MOVE_BACKWARD) 
                ? CardTesterConstants.STATE_DOWN
                : CardTesterConstants.STATE_UP;

            _logger.Debug($"MovedUnsafeAsync, state: {_State}, command: {command}, moving state: {movingState}");
            await _serialPortOperator.RunCommandAsync(command);
            await Task.Delay(DELAY_COMMAND);
            _logger.Debug($"MoveUnsafeAsync  - after delay, state: {_State}");          

            if (_State != movingState && _State != desiredState)
            { 
                var direction = (command == CardTesterConstants.COMMAND_MOVE_BACKWARD)
                    ? "BACKWARD"
                    : "FORWARD";
                var msg = $"MovedUnsafeAsync, Cannot move {direction}. State: {_State}";
                _logger.Error(msg);
                throw new CardTesterException(msg);
            }

            if (_State ==  desiredState)
            {
                var msg = $"Moving state skipped, moving state: {movingState}, state: {_State}";
                _logger.Warning(msg);
            }

            while (StateEquals(movingState))
            {
                await Task.Delay(1);
            }
            _logger.Debug($"MoveUnsafe - end, state: {_State}");
        }
        

        public async Task PrepareMeasurement()
        {
            _logger.Debug("Prepare measurement");

            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_GET_STATE);
            await Task.Delay(DELAY_COMMAND);
            _logger.Debug($"PrapareMeasurement, state: {_State}");
            var state = _serialPortOperator.GetState();
            _logger.Debug($"PrapareMeasurement, state/ get state: {_State}/{state}");

            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_STOP);
            await Task.Delay(DELAY_COMMAND);
            state = _serialPortOperator.GetState();
            _logger.Debug($"PrapareMeasurement - after Stop, state: {_State}/{state}");
            while (state != CardTesterConstants.STATE_STOPPED)
            {
                await Task.Delay(DELAY_COMMAND);
                state = _serialPortOperator.GetState();
            }
            _logger.Debug($"PrepareMeasurement - stopped, state: {_State}/{state}");

            await ResetDownAsync();
            _logger.Debug($"Prepare measurement finished, state: {_State}");
        }

        private bool StateEquals(string expectedState) => _State.Equals(expectedState);

        private void ProcessStateChanged(string message)
        {
            _logger.Debug($"ProcessStateChanged: {_State} -> {message}");
            _State = message;
        }

        private void ProcessPortOperatorState(string message)
        {
            _logger.Debug($"Process PortOperatorState: {PortOperatorState} -> {message}");
            PortOperatorState = message;
        }

        
    }
}
