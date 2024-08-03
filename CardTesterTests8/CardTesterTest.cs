using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Serilog;
using Serilog.Events;
using CardTesterLibrary;

namespace CardTesterTests
{
    public class CardTesterTest
    {
        private readonly ILogger _Logger;
        public CardTesterTest()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("test.log", LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .CreateLogger();
            _Logger = Log.Logger;
        }

        [Fact]
        public async Task Run_Defaut_Expected()
        {
            var portOperator = new SerialPortOperator("COM4", _Logger);
            var tester = new CardTesterLibrary.CardTester(_Logger, portOperator);

            
        }
    }
}
