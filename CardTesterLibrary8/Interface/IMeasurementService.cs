using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    /// <summary>
    /// Service for measurement of cards
    /// </summary>
    public interface IMeasurementService
    {
        /// <summary>
        /// Measures card response
        /// </summary>
        /// <param name="measurementIndex"></param>
        /// <returns></returns>
        public Measurement MeasureCards(int measurementIndex);

        /// <summary>
        /// Initializes measurement
        ///   - sets measurement parametes
        ///   - initializes Card reader
        /// </summary>
        /// <param name="measurementName"></param>
        /// <param name="workOrderId"></param>
        /// <param name="cardIds"></param>
        public void InitializeMeasurement(string measurementName, string workOrderId, List<int> cardIds);

        /// <summary>
        /// Finalizes measurement at card reader side
        /// </summary>
        public void FinishMeasurement();
    }
}
