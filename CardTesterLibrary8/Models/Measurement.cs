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
        /// Card 1 result
        /// </summary>
        public string? Card1Result { get; set; }

        /// <summary>
        /// Card2 result
        /// </summary>
        public string? Card2Result { get; set; }

        /// <summary>
        /// Card 3 result
        /// </summary>
        public string? Card3Result { get; set; }

        /// <summary>
        /// Card 4 result
        /// </summary>
        public string? Card4Result { get; set; }

        /// <summary>
        /// Card 1 Note
        /// </summary>
        public string? Card1Note { get; set; }

        /// <summary>
        /// Card2 Note
        /// </summary>
        public string? Card2Note { get; set; }

        /// <summary>
        /// Card 3 Note
        /// </summary>
        public string? Card3Note { get; set; }

        /// <summary>
        /// Card 4 Note
        /// </summary>
        public string? Card4Note { get; set; }

        /// <summary>
        /// Position of bending machine
        /// expected BASIC/BENT
        /// </summary>
        public string? Position { get; set; } = string.Empty;

        public int MeasurementIndex { get; set; }

    }
}
