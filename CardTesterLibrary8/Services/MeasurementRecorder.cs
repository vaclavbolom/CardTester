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
        private const string TIME_FORMAT = "yyyy-MM-ddTHH:mm:sszz";
        private const string TIME_FORMAT_FILE = "yyyy-MM-ddTHHmmsszz";

        private readonly ILogger _logger;

        private List<Measurement> _measurements;

        private readonly string _csvDelimiter;
       

        public MeasurementRecorder(ILogger logger, string csvDelimiter=",")
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _measurements = new List<Measurement>();
            _logger = logger;
            _csvDelimiter = csvDelimiter;
            
        }
        public void AddMeasurement(Measurement data)
        { 
            _measurements.Add(data);
            _logger.Debug("Measurement added");
        }
       

        public void SaveProtocol(string outputDirectory, string description)
        {            
            var timestamp = DateTime.Now;
            var fileName = $"MeasurementProtocol-{timestamp.ToString(TIME_FORMAT_FILE)}.csv";
            
            var filePath = Path.Exists(outputDirectory) ? Path.Combine(outputDirectory, fileName) : fileName;           

            var descriptionLine = $"Description{_csvDelimiter}{description}{Environment.NewLine}";
            File.WriteAllText(filePath, descriptionLine);
            string headerLine = $"timestamp{_csvDelimiter}card 1{_csvDelimiter}card 2{_csvDelimiter}card 3{_csvDelimiter}card 4{_csvDelimiter}position{Environment.NewLine}";            
            File.AppendAllText(filePath, headerLine);
            var results = _measurements
                .Select(x => WriteMeasurement(x))
                .ToList();
            File.AppendAllLinesAsync(filePath, results);
            _logger.Debug($"Protocol saved to {filePath}");
        }

        private string WriteMeasurement(Measurement x)
            => $"{x.Timestamp.ToString(TIME_FORMAT)}{_csvDelimiter}{x.Card1}{_csvDelimiter}{x.Card2}{_csvDelimiter}{x.Card3}{_csvDelimiter}{x.Card4}{_csvDelimiter}{x.Position}";
       
    }
}
