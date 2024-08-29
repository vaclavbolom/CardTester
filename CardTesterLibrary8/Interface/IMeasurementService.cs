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
        /// <returns>result of measurement</returns>
        public Measurement MeasureCards();
    }
}
