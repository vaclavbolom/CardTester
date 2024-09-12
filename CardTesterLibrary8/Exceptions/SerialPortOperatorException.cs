using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public class SerialPortOperatorException : Exception
    {
        public SerialPortOperatorException()
        {
        }

        public SerialPortOperatorException(string? message) : base(message)
        {
        }

        public SerialPortOperatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }     
    }
}
