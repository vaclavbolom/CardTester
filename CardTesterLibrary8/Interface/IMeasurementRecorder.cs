using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    /// <summary>
    /// Buffer for recording test results
    /// </summary>
    public interface IMeasurementRecorder
    {
        /// <summary>
        /// Adds measurement to buffer
        /// </summary>
        /// <param name="data"></param>
        void AddMeasurement(Measurement data);

        /// <summary>
        /// Saves results to file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="measurementDescription"></param>
        void SaveProtocol(string filePath, string measurementDescription);
    }
}
