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
        private const string POSITION_BASIC = "BASIC";
        private const string POSITION_BENT = "BENT";

        private readonly ILogger _logger;
        private readonly ISerialPortOperator _serialPortOperator;
        private readonly IMeasurementService _measurementService;
        private readonly IMeasurementRecorder _measurementRecorder;

        private string _State;

        public string PortOperatorState { get; private set; }

        public int TestCycleIndex { get; private set; }

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

            //Task.Run(() => _SerialPortOperator.Run(ProcessStateChanged));
            var task = _serialPortOperator.Run(ProcessStateChanged);
            Task.Delay(100);

        }

        public async Task ResetDownAsync()
        {            
            if (StateEquals(CardTesterConstants.STATE_STOPPED) || StateEquals(CardTesterConstants.STATE_UNKNOWN))
            {
                await _serialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_RESET);
                await Task.Delay(DELAY_COMMAND);
                while (StateEquals(CardTesterConstants.STATE_BACKWARD))
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

        public async Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown, string outputPath, string description)
        {
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                var testInterrupted = false;
                var testResult = _measurementService.MeasureCards();
                testResult.Position = POSITION_BASIC;
                _measurementRecorder.AddMeasurement(testResult);

                for (int i = 0; i < numberOfCycles; i++)
                {
                    testInterrupted = (_State == CardTesterConstants.STATE_STOPPED || _State == CardTesterConstants.STATE_DOOR_OPEN);
                    if (testInterrupted)
                        break;
                    TestCycleIndex = i + 1;
                    await RunMeasurementCycleAsync(delayUp, delayDown);
                }
                if (!testInterrupted)
                    _measurementRecorder.SaveProtocol(outputPath, description);
            }
            else
            {
                _logger.Information($"Cannot start measurement, state = {_State}");
            }

        }

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
        /// <returns></returns>
        private async Task RunMeasurementCycleAsync(int delayUp, int delayDown)
        { 
            await MoveForwardAsync();
            await Task.Delay(delayUp);
            var measurementResult = _measurementService.MeasureCards();
            measurementResult.Position = POSITION_BENT;
            _measurementRecorder.AddMeasurement(measurementResult);
            await MoveBackwardAsync();
            await Task.Delay(delayDown);
            measurementResult = _measurementService.MeasureCards();
            measurementResult.Position = POSITION_BASIC;
            _measurementRecorder.AddMeasurement(measurementResult);
        }

        private async Task MoveForwardAsync()
        {
            var movingStates = new string[] { CardTesterConstants.STATE_FORWARD, CardTesterConstants.STATE_STOPPED };

            //read state
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                await _serialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_FORWARD);
                await Task.Delay(DELAY_COMMAND);

                while ( StateEquals(CardTesterConstants.STATE_FORWARD))
                {
                    await Task.Delay(DELAY_COMMAND);
                }
            }
            else
            {
                _logger.Information($"Cannot move forward, state = {_State}");
            }
            return;
        }

        private async Task MoveBackwardAsync()
        {
            if (StateEquals(CardTesterConstants.STATE_UP))
            {
                await _serialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_BACKWARD);
                await Task.Delay(DELAY_COMMAND);
                while( StateEquals(CardTesterConstants.STATE_BACKWARD))
                {
                    await Task.Delay(0);
                }
            }
            else
            {
                _logger.Information($"Cannot move backward, state = {_State}");
            }
            return;
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
