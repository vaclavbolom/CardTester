using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTester;
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

			Log.Logger = new LoggerConfiguration()
				.WriteTo.File("test.log", LogEventLevel.Debug)
				.MinimumLevel.Debug()
				.CreateLogger();
			var portOperator = new SerialPortOperator("COM4", _Logger);

			var task =  portOperator.Run(ProcessMessage);
			Assert.NotNull(portOperator);
			await Task.Delay(500);
			portOperator.RunCommand("1");
			await Task.Delay(1000);
			portOperator.Stop();

			//TODO: test operator states
		}

		private void ProcessMessage(string message)
		{
			_Logger.Debug($"Processed message: {message}");
		}
	}
}
