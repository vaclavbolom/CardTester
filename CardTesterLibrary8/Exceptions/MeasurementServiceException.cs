using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    /// <summary>
    /// Exception for Measuremnt service
    /// </summary>
    public class MeasurementServiceException : Exception
    {
        public MeasurementServiceException() { }

        public MeasurementServiceException(string? message) : base(message)
        {
        }

        public MeasurementServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
        
        
    }
}
