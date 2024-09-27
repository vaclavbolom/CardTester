using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
    internal class CardTesterException : Exception
    {
        public CardTesterException()
        {
        }

        public CardTesterException(string message) : base(message)
        {
        }

        public CardTesterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CardTesterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
