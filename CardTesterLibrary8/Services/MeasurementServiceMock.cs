using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CardTesterLibrary
{
    /// <inheritdoc/>
    public class MeasurementServiceMock : IMeasurementService
    {
        private readonly ILogger _logger;

        private string MeasurementName { get; set; } = string.Empty;
        private string WorkOrderId { get; set; } = string.Empty;

        private List<int> CardIds { get; set; } = new List<int>();

        public MeasurementServiceMock(ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(nameof(logger));
            _logger = logger;
        }

        public void FinishMeasurement()
        {
            
        }

        public void InitializeMeasurement(string measurementName, string workOrderId, List<int> cardIds)
        {
            MeasurementName = measurementName;
            WorkOrderId = workOrderId;
            CardIds = cardIds;
        }

        public Measurement MeasureCards(int measurementIndex)
        {
            var position = Convert.ToBoolean(measurementIndex % 2) ? "FLAT" : "BENT";
            var N = CardIds.Count;
            var result = new Measurement
            {
                Timestamp = DateTime.Now,                
                MeasurementIndex = measurementIndex,
                Position = position,
                CardResults = new List<string>(new string[N]),
                CardNotes = new List<string>(new string[N])
            };
            for (int i = 0; i < CardIds.Count; i++)
            {
                result.CardResults[i] = "OK";

            }                

            _logger.Debug("Cards measured");
            return result;
        }
    }
}
