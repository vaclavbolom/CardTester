using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public class CardTesterException : Exception
    {
        public CardTesterException()
        {
            
        }

        public CardTesterException(string? message) : base(message)
        {
            
        }

        public CardTesterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
