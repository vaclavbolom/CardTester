using System.Security.Policy;
using CardTesterLibrary;
using Serilog;
using Serilog.Events;

namespace CardTesterApp
{
    public partial class TestingApp : Form
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
        private string CardTesterState { get; set; }


        public TestingApp()
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
            _cardTester = new CardTesterLibrary.CardTester(_logger, serialPortOperator);
        }

        public void CardTesterStateChanged(string message)
        {
            _logger.Debug($"state changed to {message}");

            var labelMessage = message switch
            {
                CardTesterConstants.STATE_FORWARD => "Moving FORWARD",
                CardTesterConstants.STATE_BACKWARD => "Moving BACKWARD",
                CardTesterConstants.STATE_STOPPED => "Stopped",
                CardTesterConstants.STATE_DOWN => "Position DOWN",
                CardTesterConstants.STATE_UP => "Position UP",
                _ => "Unknown"
            };
                
            label_State.Text = labelMessage;
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
            //if (!string.IsNullOrEmpty(Message)) {
            //    label_State.Text = Message;
            //}
        }

        private async void btn_Forward_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Moving forward";
            SetWidgetsState();

            await MoveForwardAsync();
            while (Running && CardTesterState != CardTesterConstants.STATE_UP)
            {
                await Task.Delay(10);
            }

            Running = false;
            CardTesterStateChanged(CardTesterState);
            SetWidgetsState();
        }

        private async Task MoveForwardAsync()
        {
            CardTesterState = CardTesterConstants.STATE_FORWARD;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(5000);
            CardTesterState = CardTesterConstants.STATE_UP;            
        }

        private async Task MoveBackward()
        {
            CardTesterState = CardTesterConstants.STATE_BACKWARD;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(5000);
            CardTesterState = CardTesterConstants.STATE_DOWN;
        }

        private async Task DoReset()
        {
            CardTesterState = CardTesterConstants.STATE_BACKWARD;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(5000);
            CardTesterState = CardTesterConstants.STATE_DOWN;
        }

        private async Task DoTest()
        {
            CardTesterState = CardTesterConstants.STATE_FORWARD;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(1000);
            CardTesterState = CardTesterConstants.STATE_UP;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(1000);
            CardTesterState = CardTesterConstants.COMMAND_MOVE_BACKWARD;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(1000);
            CardTesterState = CardTesterConstants.STATE_DOWN;            
        }

        private async void btn_Backward_Click(object sender, EventArgs e)
        {
            Running = true;
            SetWidgetsState();

            await MoveBackward();

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(10);
            }

            Running = false;
            CardTesterStateChanged(CardTesterState);
            SetWidgetsState();
        }

        private async void btn_Run_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Test running";
            SetWidgetsState();
            await DoTest();

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(10);
            }

            Running = false;
            CardTesterStateChanged(CardTesterState);
            SetWidgetsState();
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            Running = false;
            Message = "Stopped";
            SetWidgetsState();
            CardTesterState = CardTesterConstants.STATE_STOPPED;
            CardTesterStateChanged(CardTesterState);
            await Task.Delay(100);
        }

        private async void btn_Reset_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Returning backward - resetting";
            SetWidgetsState();

            await DoReset();

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(10);
            }

            Running = false;
            CardTesterStateChanged(CardTesterState);
            SetWidgetsState();
        }

        private void label_State_Click(object sender, EventArgs e)
        {

        }
    }
}
