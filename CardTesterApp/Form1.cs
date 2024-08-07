using System.Security.Policy;
using CardTesterLibrary;
using Serilog;
using Serilog.Events;

namespace CardTesterApp
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger;
        private readonly ICardTester _cardTester;

        private object[]? _disabledDuringRunWidgets;
        private object[]? _disabledDuringStopWidgets;


        private bool Running { get; set; }

        private int NumberOfCycles
        {
            get => (int)tb_NubmerOfCycles.Value;
        }

        private decimal DelayBasic
        {
            get => tb_DelayBasic.Value;
        }

        private decimal DelayBend
        {
            get => tb_DelayBend.Value;
        }

        private string Message { get; set; }
        private string State { get; set; }


        public Form1()
        {
            InitializeComponent();
            Running = false;

            _disabledDuringRunWidgets = new object[]
            {
                tb_NubmerOfCycles,
                tb_DelayBasic,
                tb_DelayBend,
                btn_Forward,
                btn_Backward,
                btn_Reset,
                btn_Run
            };
            _disabledDuringStopWidgets = new object[]
            {
                btn_Stop
            };

            Log.Logger = new LoggerConfiguration()
               .WriteTo.File("CardTesterApp.log", LogEventLevel.Debug)
               .MinimumLevel.Debug()
               .CreateLogger();
            _logger = Log.Logger;

            var serialPortOperator = new SerialPortOperator("COM4", _logger);
            _cardTester = new CardTester(_logger, serialPortOperator);
        }

        private void SetWidgetsState()
        {
            if (_disabledDuringRunWidgets != null)
            {

                var desiredState = !Running;
                foreach (var widget in _disabledDuringRunWidgets)
                {
                    if (widget is Button)
                        ((Button)widget).Enabled = desiredState;
                    if (widget is NumericUpDown)
                        ((NumericUpDown)widget).Enabled = desiredState;
                }
            }

            if (_disabledDuringStopWidgets != null)
            {

                var desiredState = Running;
                foreach (var widget in _disabledDuringStopWidgets)
                {
                    if (widget is Button)
                        ((Button)widget).Enabled = desiredState;
                    if (widget is NumericUpDown)
                        ((NumericUpDown)widget).Enabled = desiredState;

                }
            }
            if (!string.IsNullOrEmpty(Message)) {
                label_State.Text = Message;
            }
        }

        private async void btn_Forward_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Moving forward";
            SetWidgetsState();

            await Task.Delay(5000);

            Running = false;
            Message = "Position UP";
            SetWidgetsState();
        }

        private async void btn_Backward_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Moving backward";
            SetWidgetsState();

            await Task.Delay(5000);

            Running = false;
            Message = "Position Down";
            SetWidgetsState();
        }

        private async void btn_Run_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Test running";
            SetWidgetsState();

            await Task.Delay(5000);

            Running = false;
            Message = "Position DOWN";
            SetWidgetsState();
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            Running = false;
            Message = "Stopped";
            SetWidgetsState();

            await Task.Delay(100);
        }

        private async void btn_Reset_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Returning backward - resetting";
            SetWidgetsState();

            await Task.Delay(5000);

            Running = false;
            Message = "Position DOWN";
            SetWidgetsState();
        }

        private void label_State_Click(object sender, EventArgs e)
        {

        }
    }
}
