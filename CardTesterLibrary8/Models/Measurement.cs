using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    /// <summary>
    /// Result of one measurement
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// Measurement timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Measurement results
        /// </summary>
        public List<string> CardResults { get; set; }


        /// <summary>
        /// Additional notes for measurements
        /// </summary>
        public List<string> CardNotes { get; set; }

        
        /// <summary>
        /// Position of bending machine
        /// expected BASIC/BENT
        /// </summary>
        public string? Position { get; set; } = string.Empty;

        public int MeasurementIndex { get; set; }

        public Measurement(int numberOfCards=4)
        {
            CardResults = new List<string>(numberOfCards);
            CardNotes = new List<string>(numberOfCards);
        }

    }
}
