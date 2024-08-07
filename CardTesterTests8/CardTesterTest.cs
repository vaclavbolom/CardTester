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
            var portOperator = new SerialPortOperator("COM4", _Logger);
            var tester = new CardTester(_Logger, portOperator);

            var method = tester.GetType().GetMethod("PrepareMeasurement", BindingFlags.Instance | BindingFlags.NonPublic);

            var task = (Task) method.Invoke(tester, null);
            await task;
            await Task.Delay(100);

            Assert.NotNull(task);

            var stateField = tester.GetType().GetField("_State", BindingFlags.Instance | BindingFlags.NonPublic);
            var state = stateField.GetValue(tester);

            Assert.Equal("d", state);
            
        }
    }
}
