using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester.Exceptions
{
	public class OperatorException : Exception
	{
		public OperatorException() { }

		public OperatorException(string message) : base(message)
		{
		}

		public OperatorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
