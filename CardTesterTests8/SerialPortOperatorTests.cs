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
			var portOperator = new SerialPortOperator("COM4", Log.Logger);

			var task = portOperator.Run();
			Assert.NotNull(portOperator);
			await Task.Delay(20000);
			portOperator.Stop();

			//TODO: test operator states
		}
	}
}
