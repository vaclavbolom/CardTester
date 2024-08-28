using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Serilog;
using Serilog.Events;
using CardTesterLibrary;
using System.Reflection;

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

        /// <summary>
        /// Tests Preparation of measuremet
        /// use script card_tester.ino
        /// instructions:
        ///   - run test
        ///   - after red LED is ON press DOWN switch
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PrepareMeasurement_Defaut_Expected()
        {
            _Logger.Debug("\n---------------\nPrepareMeasurement test");
            var portOperator = new SerialPortOperator("COM4", _Logger);
            var tester = new CardTester(_Logger, portOperator);

            await tester.PrepareMeasurement(); ;
            await Task.Delay(100);

            var stateField = tester.GetType().GetField("_State", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(stateField);

            var state = stateField.GetValue(tester);

            Assert.Equal("d", state);
            
        }
    }
}
