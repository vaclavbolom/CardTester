using CardTesterLibrary;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CardTesterTests
{
    public class MeasurementRecorderTest
    {
        private const string _dllName = "lib/acfmistub.dll";
        private readonly ILogger _logger;

        public MeasurementRecorderTest()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("measurement_recorder.log", LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .CreateLogger();
            _logger = Log.Logger;
        }


        [Fact]
        public void RunTest_Default_Expected()
        {
            var cardIds = new List<int> { 1, 2, 3, 4 };
            var recorder = new MeasurementRecorder(_logger, cardIds);
            var measurementService = new CardMeasurementService(_logger, _dllName, 4);

            measurementService.InitializeMeasurement("nice measurement", "work order id 1", cardIds);

            recorder.StartMeasurement(cardIds);

            for (int i = 0; i < 10; i++) {
                var result = measurementService.MeasureCards(i);
                recorder.AddMeasurement(result);
            }
            
            Assert.NotNull(recorder);
        }
    }
}
