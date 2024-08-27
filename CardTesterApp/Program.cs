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
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File("CardTesterApp.log", LogEventLevel.Debug)
              .MinimumLevel.Debug()
              .CreateLogger();
            var logger = Log.Logger;
			logger.Debug("Application started");

			var serialPortOperator = new SerialPortOperator("COM4", logger);
			var cardTester = new CardTester(logger, serialPortOperator);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
			Application.Run(new TestingAppForm(cardTester, logger));
		}
	}
}