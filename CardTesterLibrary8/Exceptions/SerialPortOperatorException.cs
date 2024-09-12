using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public class SerialPOrtOperatorException : Exception
    {
        public SerialPOrtOperatorException()
        {
        }

        public SerialPOrtOperatorException(string? message) : base(message)
        {
        }

        public SerialPOrtOperatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }     
    }
}
