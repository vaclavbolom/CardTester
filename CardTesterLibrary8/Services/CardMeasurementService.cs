using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CardTesterLibrary
{
    public class CardMeasurementService : IMeasurementService
    {
        //generic Dynamic dll loading
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern nint LoadLibrary(string path);

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern nint FreeLibrary(nint hModule);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern nint GetProcAddress(nint hModule, string procName);

        [DllImport("Kernel32.dll")]
        private static extern int GetLastError();

        public static Delegate LoadFunction<T>(string dllPath, string functionName)
        {
            var pathExist = Path.Exists(dllPath);

            var hModule = LoadLibrary(dllPath);
            if (hModule.ToString() == "0")
            {
                int iError = GetLastError();
                //throw error
            }
            var functionAddress = GetProcAddress(hModule, functionName);
            return Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(T));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int JobStartDelegate(
                 char[] cJobName, int iJobNameSize,
                 char[] cWoid, int iWoidSize,
                 ref CodingResult eResult,
                 /*ref*/ byte[] cErrorMessage,    //for efficiency: please allocate a buffer with a size of JOBSTART_RESULT_BUFFERSIZE
                 ref int iErrorMessage);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void CardRunDelegate(ref int hHandle,
                 int cardPosition,
                 int iCardId1,
                 int iBentcounter,
                 ref CodingResult eResult,
                 byte[] cErrorMessage,      //for efficiency: please allocate a buffer with a size of CARDRUN_RESULT_BUFFERSIZE
                 ref int iErrorMessage);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void JobEndDelegate(ref int hHandle);

        private JobStartDelegate JobStart;
        private CardRunDelegate CardRun;
        private JobEndDelegate JobEnd;


        private readonly ILogger _logger;
        private readonly string _dllName;
        private readonly int _numberOfCards;

        private string MeasurementName { get; set; } = string.Empty;
        private string WorkOrderId { get; set; } = string.Empty;
        private List<int> CardIds { get; set; } = new List<int>();
        private int MeasurementHandle { get; set; }

        public CardMeasurementService(ILogger logger, string dllName, int numberOfCards=4)
        {
            ArgumentNullException.ThrowIfNull(nameof(logger));
            _logger = logger;
            _dllName = dllName;
            _numberOfCards = numberOfCards;

            JobStart = (JobStartDelegate)LoadFunction<JobStartDelegate>(_dllName, "JobStart");
            CardRun = (CardRunDelegate)LoadFunction<CardRunDelegate>(_dllName, "CardRun");
            JobEnd = (JobEndDelegate)LoadFunction<JobEndDelegate>(_dllName, "JobEnd");
        }

        public void FinishMeasurement()
        {
            int measurementHandle = 0;
            JobEnd(ref measurementHandle);

            MeasurementHandle = measurementHandle;
        }

        public void InitializeMeasurement(string measurementName, string workOrderId, List<int> cardIds)
        {
            MeasurementName = measurementName;
            WorkOrderId = workOrderId;
            CardIds = cardIds;

            if (_numberOfCards != cardIds.Count)
                throw new MeasurementServiceException("Number of card is different than count of provided Card IDs");

            var result = CodingResult.CODING_RESULT_UNINITIALIZED;
            var errorSize = 1024;
            var error = new byte[1024];
            var handle = 0;

            MeasurementHandle = JobStart(
                MeasurementName.ToCharArray(), 
                MeasurementName.Length, 
                WorkOrderId.ToCharArray(), 
                WorkOrderId.Length, 
                ref result, 
                error, 
                ref errorSize);

            if (errorSize > 0)
            {
                var clearError = error.Skip(0).Take(errorSize).ToArray();
                var errorMessage = Encoding.UTF8.GetString(clearError);
                var message = $"Cannot initialize measurement, {error}: {errorMessage}";

                _logger.Error(message);
                throw new MeasurementServiceException(message);
            }

            MeasurementHandle = handle;
        }

        public Measurement MeasureCards(int measurementIndex)
        {
            var result = CodingResult.CODING_RESULT_UNINITIALIZED;
            var errorSize = 1024;
            var error = new byte[1024];
            var handle = 0;
            var position = Convert.ToBoolean(measurementIndex % 2) ? "FLAT" : "BENT";

            var measurementResult = new Measurement
            {
                MeasurementIndex = measurementIndex,
                Timestamp = DateTime.UtcNow,
                Position = position
            };

            for (int i = 0; i < _numberOfCards; i++)
            {
                CardRun(ref handle, i + 1, CardIds[i], measurementIndex, ref result, error, ref errorSize);

                var message = string.Empty;
                if (errorSize > 0)
                {
                    var clearMessage = error.Skip(0).Take(errorSize).ToArray();
                    if (clearMessage != null)
                        message = System.Text.Encoding.UTF8.GetString(clearMessage);
                }
                measurementResult.CardResults[i] = result.ToString();
                measurementResult.CardNotes[i] = message;               
            }

            return measurementResult;
        }
    }
}
