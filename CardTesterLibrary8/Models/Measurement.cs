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
        public string? Card1 { get; set; }

        /// <summary>
        /// Card2 result
        /// </summary>
        public string? Card2 { get; set; }

        /// <summary>
        /// Card 3 result
        /// </summary>
        public string? Card3 { get; set; }

        /// <summary>
        /// Card 4 result
        /// </summary>
        public string? Card4 { get; set; }

        /// <summary>
        /// Position of bending machine
        /// expected BASIC/BENT
        /// </summary>
        public string? Position { get; set; } = string.Empty;

    }
}
