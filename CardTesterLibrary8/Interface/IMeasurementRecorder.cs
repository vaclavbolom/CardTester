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
        /// <param name="outputDirectory">directory for protocol</param>
        /// <param name="description">measurement user description</param>
        /// <param name="workOrderId"></param>
        /// <param name="jobName"></param>
        void SaveProtocol(string outputDirectory, string description, string workOrderId, string jobName);

        /// <summary>
        /// Prepares new measeurement buffer
        /// </summary>
        /// <param name="cardIds"></param>
        void StartMeasurement(List<int> cardIds);

        /// <summary>
        /// Returns summary of test
        /// </summary>
        /// <returns></returns>
        MeasurementResult GetResult();
       
    }
}
