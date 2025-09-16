using System.Text.Json;
using CardTesterLibrary;
using Serilog;
using Serilog.Events;

namespace CardTesterApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settingsString = File.ReadAllText("appsettings.json");
            var configuration = JsonSerializer.Deserialize<ApplicationSettings>(settingsString)
                ?? throw new ArgumentException(nameof(settingsString));

            var logFile = "CardTesterApp.log";

            if (Path.Exists(configuration.LogDirectory))
                logFile = Path.Combine(configuration.LogDirectory, logFile);
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File(logFile, LogEventLevel.Debug)
              .MinimumLevel.Debug()
              .CreateLogger();
            var logger = Log.Logger;
            logger.Debug("\n-----------------\nApplication started");


            var serialPortOperator = new SerialPortOperator(configuration.PortName, logger);
            //var measurementService = new MeasurementServiceMock(logger);
            var measurementService = new CardMeasurementService(logger, configuration.CardReaderDll, configuration.DefaultCardIds.Count);
            var measurementRecorder = new MeasurementRecorder(logger, configuration.DefaultCardIds, configuration.CsvDelimiter);
            var cardTester = new CardTester(logger, serialPortOperator, measurementService, measurementRecorder);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new TestingAppForm(serialPortOperator, cardTester, logger, measurementService, configuration));
        }
    }
}