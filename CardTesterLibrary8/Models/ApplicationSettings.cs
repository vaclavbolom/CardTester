using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public  class ApplicationSettings
    {
        public string PortName { get; set; } = string.Empty;

        public string OutputDirectory { get; set; } = string.Empty;

        public int NumberOfCycles { get; set; }

        public decimal DelayBasic { get; set; }

        public decimal DelayBent { get; set; }

        public IList<string> CardTypes { get; set; } = new List<string>();

        public int CalibrationValidityInHours { get; set; }
    }
}
