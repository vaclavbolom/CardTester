using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTester;
using Microsoft.Extensions.Logging;

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

        private const string MOVE_FORWARD = "1";
        private const string MOVE_BACKWARD = "2";
        private const string STOP = "0";
        private const string RESET = "3";


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
        }

        public Task MoveDownAsync()
        {
            throw new NotImplementedException();
        }

        public Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }

        private Task MoveForwardAsync()
        {
            throw new NotImplementedException();
        }

        private Task MoveBackwardAsync()
        {
            throw new NotImplementedException();
        }

        private Task MeasureAsync()
        {
            throw new NotImplementedException();
        }
    }
}
