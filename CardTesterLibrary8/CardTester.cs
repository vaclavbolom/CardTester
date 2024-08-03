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
        private const string STATE_FORWARD = "f";
        private const string STATE_BACKWARD = "b";
        private const string STATE_UP = "u";
        private const string STATE_DOWN = "d";
        private const string STATE_STOPPED = "s";
        private const string STATE_UNKNOWN = "x";

        private const string COMMAND_MOVE_FORWARD = "1";
        private const string COMMAND_MOVE_BACKWARD = "2";
        private const string COMMAND_STOP = "0";
        private const string COMMAND_RESET = "3";


        private readonly ILogger _Logger;
        private readonly ISerialPortOperator _serialPortOperator;

        private string _State;

        public CardTester(ILogger logger, ISerialPortOperator serialPortOperator)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(serialPortOperator, nameof(serialPortOperator));
            _Logger = logger;
            _serialPortOperator = serialPortOperator;
            _State = STATE_UNKNOWN;

            Task.Run(() => _serialPortOperator.Run(ProcessStateChanged));
        }

        public async Task MoveDownAsync()
        {            
            if (StateEquals(STATE_STOPPED))
            {
                await _serialPortOperator.RunCommandAsync(COMMAND_RESET);
                while (StateEquals(STATE_BACKWARD))
                {
                    await Task.Delay(0);
                }
            }
            else
            {
                _Logger.Information($"Cannot return to lower position, state = {_State}");
            }
            return;
        }

        public async Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown)
        {
            if (StateEquals(STATE_DOWN))
            {
                //TODO: create buffer for test results
                await MeasureAsync();
                for (int i = 0; i < numberOfCycles; i++)
                {
                    await RunMeasurementCycleAsync(delayUp, delayDown);
                }
                //TODO: write test results
            }
            else
            {
                _Logger.Information($"Cannot start measurement, state = {_State}");
            }

        }

        public Task StopAsync()
        {
            return Task.Run(_serialPortOperator.Stop);
        }

        private async Task RunMeasurementCycleAsync(int delayUp, int delayDown)
        { 
            await MoveForwardAsync();
            await Task.Delay(delayUp);
            await MeasureAsync();
            await MoveBackwardAsync();
            await Task.Delay(delayDown);
            await MeasureAsync();
        }

        private async Task MoveForwardAsync()
        {
            var movingStates = new string[] { STATE_FORWARD, STATE_STOPPED };

            //read state
            if (StateEquals(STATE_DOWN))
            {
                await _serialPortOperator.RunCommandAsync(COMMAND_MOVE_FORWARD);
                while( StateEquals(STATE_FORWARD))
                {
                    await Task.Delay(0);
                }
            }
            else
            {
                _Logger.Information($"Cannot move forward, state = {_State}");
            }
            return;
        }

        private async Task MoveBackwardAsync()
        {
            if (StateEquals(STATE_DOWN))
            {
                await _serialPortOperator.RunCommandAsync(COMMAND_MOVE_BACKWARD);
                while( StateEquals(STATE_BACKWARD))
                {
                    await Task.Delay(0);
                }
            }
            else
            {
                _Logger.Information($"Cannot move backward, state = {_State}");
            }
            return;
        }

        private async Task MeasureAsync()
        {

            _Logger.Information("Measurement...");
            await Task.Delay(10);

        }

        private bool StateEquals(string expectedState) => _State.Equals(expectedState);

        private void ProcessStateChanged(string message) => _State = message;
    }
}
