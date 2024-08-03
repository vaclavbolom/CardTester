using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTesterLibrary;
using Serilog;
using Serilog.Events;
using Xunit;


namespace CardTesterTests
{
	public class SerialPortOperatorTests
	{
		private readonly ILogger _Logger;
        public SerialPortOperatorTests()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("test.log", LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .CreateLogger();
			_Logger = Log.Logger;
        }


        [Fact]
		public void Test()
		{
			Assert.True(true);
		}

		
		[Fact]
		public async Task Run_Default_Expected()
		{			
			var portOperator = new SerialPortOperator("COM4", _Logger);

			var task =  portOperator.Run(ProcessMessage);
			Assert.NotNull(portOperator);
			await Task.Delay(100);
			await Task.Delay(100);
			
			var state = portOperator.GetState();
			Assert.Equal("s", state);
			//TODO: test operator states
		}


        /// <summary>
        /// Tests write to Arduino
        ///   use test_write.ino
        /// </summary>
        /// <returns></returns>
        [Fact]
		public async Task RunCommand_Default_Expected()
		{
            var portOperator = new SerialPortOperator("COM4", _Logger);

            var task = portOperator.Run(ProcessMessage);			
            portOperator.RunCommand("3");
			await Task.Delay(100);
			var state = portOperator.GetState();
			Assert.NotEqual(string.Empty, state);
        }

        private void ProcessMessage(string message)
		{
			_Logger.Debug($"Processed message: {message}");
		}
	}
}
