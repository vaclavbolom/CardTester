using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CardTester;

namespace CardTesterTests
{
	public class SerialPortOperatorTests
	{
		[Fact]
		public void SerialPortOperatorTest()
		{
			Assert.True(true);
		}

		[Fact]
		public async void Run_Default_Expected()
		{
			var portOperator = new SerialPortOperator("COM4");

			await portOperator.Run();
			Assert.NotNull(portOperator);

			//TODO: test operator states
		}
	}
}
