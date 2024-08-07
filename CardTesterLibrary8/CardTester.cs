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
        
        private const int DELAY_COMMAND = 10;


        private readonly ILogger _Logger;
        private readonly ISerialPortOperator _SerialPortOperator;

        private string _State;

        public CardTester(ILogger logger, ISerialPortOperator serialPortOperator)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(serialPortOperator, nameof(serialPortOperator));
            _Logger = logger;
            _SerialPortOperator = serialPortOperator;
            _State = CardTesterConstants.STATE_UNKNOWN;

            Task.Run(() => _SerialPortOperator.Run(ProcessStateChanged));            
        }

        public async Task ResetDownAsync()
        {            
            if (StateEquals(CardTesterConstants.STATE_STOPPED))
            {
                await _SerialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_RESET);
                await Task.Delay(DELAY_COMMAND);
                while (StateEquals(CardTesterConstants.STATE_BACKWARD))
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
            if (StateEquals(CardTesterConstants.STATE_DOWN))
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
            return Task.Run(_SerialPortOperator.Stop);
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
            var movingStates = new string[] { CardTesterConstants.STATE_FORWARD, CardTesterConstants.STATE_STOPPED };

            //read state
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                await _SerialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_FORWARD);
                await Task.Delay(DELAY_COMMAND);
                while( StateEquals(CardTesterConstants.STATE_FORWARD))
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
            if (StateEquals(CardTesterConstants.STATE_DOWN))
            {
                await _SerialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_MOVE_BACKWARD);
                await Task.Delay(DELAY_COMMAND);
                while( StateEquals(CardTesterConstants.STATE_BACKWARD))
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

        private async Task PrepareMeasurement()
        {
            _Logger.Debug("Prepare measurement");
            await _SerialPortOperator.RunCommandAsync(CardTesterConstants.COMMAND_STOP);
            var state = _SerialPortOperator.GetState();
            while (state != CardTesterConstants.STATE_STOPPED)
            {
                await Task.Delay(DELAY_COMMAND);
                state = _SerialPortOperator.GetState();
            }
            await ResetDownAsync();
        }

        private bool StateEquals(string expectedState) => _State.Equals(expectedState);

        private void ProcessStateChanged(string message) => _State = message;
    }
}
