using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using CardTester;
using Assert = Xunit.Assert;


namespace CartReaderTests
{
	public class SerialPortOperatorTests
	{
		[Fact]
		public async Task Run_Default_Expected()
		{
			var portOperator = new SerialPortOperator("COM4");

			await portOperator.Run();

			Assert.NotNull(portOperator);
		}

		[Fact]
		public void Test()
		{
			var result = 1 + 2;

			Assert.Equal(3, result);
		}

	}
}
