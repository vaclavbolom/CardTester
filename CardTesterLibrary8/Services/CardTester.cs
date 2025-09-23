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
            await Reset(CardTesterConstants.COMMAND_RESET, CardTesterConstants.STATE_UP);
        }

        /// <inheritdoc />
        public async Task ResetUpAsync()
        {
            await Reset(CardTesterConstants.COMMAND_RESET_UP, CardTesterConstants.STATE_DOWN);
        }

        private async Task Reset(string command, string possibleState)
        {
            var movingState = command switch
            {
                CardTesterConstants.COMMAND_RESET => CardTesterConstants.STATE_FORWARD,
                CardTesterConstants.COMMAND_RESET_UP => CardTesterConstants.STATE_BACKWARD,
                _ => throw new CardTesterException($"Cannot reset with command {command}")
            };

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
                _logger.Information($"Cannot return to lower position, state = {_State}");
            }
            return;
        }



        /// <inheritdoc />
        public async Task<MeasurementResult> RunTestAsync(int numberOfCycles, int delayUp, int delayDown, string outputPath, string description, string workOrderId, string jobName, List<int> cardIds)
        {
            var result = MeasurementResult.Empty();
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
                    if (testInterrupted)
                        break;
                    TestCycleIndex = i + 1;
                    await RunMeasurementCycleAsync(delayUp, delayDown, TestCycleIndex);
                }
                if (!testInterrupted)
                {
                    _measurementRecorder.SaveProtocol(outputPath, description, workOrderId, jobName);
                    result = _measurementRecorder.GetResult();
                }

            }
            else
            {
                _logger.Information($"Cannot start measurement, state = {_State}");
            }

            return result;
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
            await MoveForwardAsync();
            await Task.Delay(delayUp);
            var measurementResult = _measurementService.MeasureCards(measurementIndex);
            measurementResult.Position = POSITION_BENT;
            _measurementRecorder.AddMeasurement(measurementResult);
            await MoveBackwardAsync();
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
                _logger.Information($"Cannot move backward, state = {_State}");
            }
            return;
        }

        /// <inheritdoc />
        public async Task MoveForwardUnsafeAsync()
        {
            await _serialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_FORWARD);
            await Task.Delay(DELAY_COMMAND);

            if (_State != CardTesterConstants.STATE_FORWARD)
            {
                var msg = $"Cannot move forward. State: {_State}";
                _logger.Error(msg);
                throw new CardTesterException(msg);
            }

            while (StateEquals(CardTesterConstants.STATE_FORWARD))
            {
                await Task.Delay(DELAY_COMMAND);
            }
        }

        /// <inheritdoc />
        public async Task MoveBackwardUnsafeAsync()
        {
            await _serialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_BACKWARD);
            await Task.Delay(DELAY_COMMAND);

            if (_State != CardTesterConstants.STATE_BACKWARD)
            {
                var msg = $"Cannot move backward. State: {_State}";
                _logger.Error(msg);
                throw new CardTesterException(msg);
            }

            while (StateEquals(CardTesterConstants.STATE_BACKWARD))
            {
                await Task.Delay(0);
            }
        }

        private async Task MeasureAsync()
        {

            _logger.Information("Measurement...");
            await Task.Delay(10);

        }

        public async Task PrepareMeasurement()
        {
            _logger.Debug("Prepare measurement");

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
            await ResetDownAsync();
            _logger.Debug("Prepare measurement finished");
        }

        private bool StateEquals(string expectedState) => _State.Equals(expectedState);

        private void ProcessStateChanged(string message) => _State = message;

        private void ProcessPortOperatorState(string message) => PortOperatorState = message;

        
    }
}
