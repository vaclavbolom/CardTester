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

        private List<int> CardIds { get; set; }
       

        public MeasurementRecorder(ILogger logger, List<int> cardIds, string csvDelimiter=",")
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            if (cardIds.Count == 0)
                throw new ArgumentException("cardIds is empty");
            
            _measurements = new List<Measurement>();
            _logger = logger;
            _csvDelimiter = csvDelimiter;
            CardIds = cardIds;            
        }
        public void AddMeasurement(Measurement data)
        { 
            _measurements.Add(data);
            _logger.Debug("Measurement added");
        }
       

        public void SaveProtocol(string outputDirectory, string description, string workOrderId)
        {            
            var timestamp = DateTime.Now;
            var fileName = $"MeasurementProtocol-{timestamp.ToString(TIME_FORMAT_FILE)}.csv";
            
            var filePath = Path.Exists(outputDirectory) ? Path.Combine(outputDirectory, fileName) : fileName;           

            var descriptionLine = $"Job Name{_csvDelimiter}{description}{Environment.NewLine}";
            File.WriteAllText(filePath, descriptionLine);
            var workOrderIdLine = $"Work Order ID{_csvDelimiter}{workOrderId}{Environment.NewLine}";
            File.WriteAllText(filePath, workOrderIdLine);

            string headerLine = BuildMeasurementHeader();
            File.AppendAllText(filePath, headerLine);
            var results = _measurements
                .Select(x => WriteMeasurement(x))
                .ToList();
            File.AppendAllLinesAsync(filePath, results);
            _logger.Debug($"Protocol saved to {filePath}");
        }

        public void StartMeasurement(List<int> cardIds)
        {
            if (cardIds == null || cardIds.Count == 0)
                throw new ArgumentNullException(nameof(cardIds));

            CardIds = cardIds;
            _measurements = new List<Measurement>();
        }

        private string WriteMeasurement(Measurement x)
        {
            var N = x.CardResults.Count;
            var record = 
                x.Timestamp.ToString(TIME_FORMAT) + _csvDelimiter
                + x.MeasurementIndex.ToString() + _csvDelimiter
                + x.Position + _csvDelimiter;
            for (int i = 0; i < N; i++)
            {
                record += x.CardResults[i] + _csvDelimiter
                    + x.CardNotes[i] + _csvDelimiter;
            }

            return record;            
        }

        private string BuildMeasurementHeader()
        {
            var header = "timestamp" + _csvDelimiter
                + "index" + _csvDelimiter
                + "position" + _csvDelimiter;

            for (int i = 0; i < CardIds.Count; i++)
            {
                header += $"{CardIds[i]} result" + _csvDelimiter
                    + $"{CardIds[i]} note" + _csvDelimiter;
            }

            return header;
        }

    }
}
