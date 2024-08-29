using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CardTesterLibrary
{
    public class MeasurementRecorder : IMeasurementRecorder
    {
        private const string DELIMITER = ",";
        private const string TIME_FORMAT = "yyyy-MM-ddTHH:mm:sszz";

        private readonly ILogger _logger;

        private List<Measurement> _measurements;

        public MeasurementRecorder(ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _measurements = new List<Measurement>();
            _logger = logger;
        }
        public void AddMeasurement(Measurement data)
        { 
            _measurements.Add(data);
            _logger.Debug("Measurement added");
        }
       

        public void SaveProtocol(string filePath, string measurementDescription)
        {            
            var timestamp = DateTime.Now;
            var fileName = $"MeasurementProtocol-{timestamp.ToString(TIME_FORMAT)}";

            filePath = Path.Combine(filePath, fileName);

            var descriptionLine = $"Description{DELIMITER}{measurementDescription}";
            File.WriteAllText(filePath, descriptionLine);
            string headerLine = $"timestamp{DELIMITER}card 1{DELIMITER}card 2{DELIMITER}card 3{DELIMITER}card 4";            
            File.AppendAllText(filePath, headerLine);
            var results = _measurements
                .Select(x => WriteMeasurement(x))
                .ToList();
            File.AppendAllLinesAsync(filePath, results);
            _logger.Debug($"Protocol saved to {filePath}");
        }

        private string WriteMeasurement(Measurement x)
            => $"{x.Timestamp.ToString(TIME_FORMAT)}{DELIMITER}{x.Card1}{DELIMITER}{x.Card2}{DELIMITER}{x.Card3}{DELIMITER}{x.Card4}";
       
    }
}
