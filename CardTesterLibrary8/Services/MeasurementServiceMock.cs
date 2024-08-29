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
        public MeasurementServiceMock(ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(nameof(logger));
            _logger = logger;
        }
        public Measurement MeasureCards()
        {
            var result = new Measurement
            {
                Timestamp = DateTime.Now,
                Card1 = "OK",
                Card2 = "OK",
                Card3 = "OK",
                Card4 = "OK"
            };

            _logger.Debug("Cards measured");
            return result;
        }
    }
}
