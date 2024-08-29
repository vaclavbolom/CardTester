using System.Security.Policy;
using System.Windows.Forms;
using CardTesterLibrary;
using Serilog;
using Serilog.Events;

namespace CardTesterApp
{
    public partial class TestingAppForm : Form
    {
        private readonly ILogger _logger;
        private readonly ICardTester _cardTester;
        private readonly ISerialPortOperator _serialPortOperator;

        private const int DELAY_COMMAND = 50;

        private object[]? _disabledDuringRunWidgets;
        private object[]? _disabledDuringStopWidgets;

        private System.Windows.Forms.Timer _timer;


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

        private string CardTesterPreviousState { get; set; }


        public TestingAppForm(ISerialPortOperator serialPortOperator, ICardTester cardTester, ILogger logger)
        {
            _serialPortOperator = serialPortOperator ?? throw new ArgumentNullException(nameof(serialPortOperator));
            _cardTester = cardTester ?? throw new ArgumentNullException(nameof(cardTester));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(StateEventProcessor);
            _timer.Start();

            _logger.Debug("Form created");
            Message = "Started";
            CardTesterState = _serialPortOperator.GetState();

            InitializeComponent();
            Running = false;

            _disabledDuringRunWidgets = new object[]
            {
                tb_NubmerOfCycles,
                tb_DelayBasic,
                tb_DelayBend,
                btn_Reset,
                btn_Run
            };
            _disabledDuringStopWidgets = new object[]
            {
                btn_Stop
            };

            _serialPortOperator.RegisterStateChangeMethod(CardTesterStateChanged);
        }

        /// <summary>
        /// Updates message every _timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StateEventProcessor(object? sender, EventArgs e)
        {
            UpdateMessage();
            label_Cycle.Text = _cardTester.TestCycleIndex.ToString();
        }

        public void UpdateMessage()
        {
            var labelMessage = CardTesterState switch
            {
                CardTesterConstants.STATE_FORWARD => "Moving FORWARD",
                CardTesterConstants.STATE_BACKWARD => "Moving BACKWARD",
                CardTesterConstants.STATE_STOPPED => "Stopped",
                CardTesterConstants.STATE_DOWN => "Position DOWN",
                CardTesterConstants.STATE_UP => "Position UP",
                _ => "Unknown"
            };

            if (label_State.Text.Equals(labelMessage.ToString()))
            {
                return;
            }

            label_State.Text = labelMessage;
            _logger.Debug($"Message changed, state:{CardTesterState}, message:{labelMessage}");
        }

        public void CardTesterStateChanged(string message)
        {
            CardTesterPreviousState = CardTesterState;
            CardTesterState = message;

            _logger.Debug($"Card Tester App state changed to {message}");
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


        private async Task DoReset()
        {
            _logger.Debug("DoReset started");
            UpdateMessage();

            await _cardTester.ResetDownAsync();
            await Task.Delay(10);
            UpdateMessage();
            _logger.Debug("DoReset finished");
        }

        private async Task DoTest()
        {
            var delayBendInMilliseconds = (int)(1000 * DelayBend);
            var delayBasicInMilliseconds = (int)(1000 * DelayBasic);
            await _cardTester.RunTestAsync(NumberOfCycles, delayBendInMilliseconds, delayBasicInMilliseconds);
            await Task.Delay(DELAY_COMMAND);
        }

        private async Task DoStop()
        {
            _logger.Debug("DoStop started");

            await _cardTester.StopAsync();
            await Task.Delay(DELAY_COMMAND);
            UpdateMessage();

            _logger.Debug("DoStop finished");
        }



        private async void btn_Run_Click(object sender, EventArgs e)
        {
            _logger.Debug("Run clicked");
            Running = true;
            Message = "Test running";
            SetWidgetsState();
            await DoTest();

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            Running = false;
            UpdateMessage();
            SetWidgetsState();
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            _logger.Debug("Stop clicked");
            Running = false;
            Message = "Stopped";
            SetWidgetsState();
            await DoStop();

            while (CardTesterState != CardTesterConstants.STATE_STOPPED)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            UpdateMessage();
            SetWidgetsState();
            await Task.Delay(DELAY_COMMAND);
            _logger.Debug($"Stop finished, state:{CardTesterState}");
        }

        private async void btn_Reset_Click(object sender, EventArgs e)
        {
            Running = true;
            Message = "Returning backward - resetting";
            SetWidgetsState();

            await DoReset();

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            Running = false;
            UpdateMessage();
            SetWidgetsState();
        }

        private void label_State_Click(object sender, EventArgs e)
        {

        }

        private void TestingAppForm_Load(object sender, EventArgs e)
        {
            var task = _cardTester.PrepareMeasurement();
            task.Wait(100);
            UpdateMessage();
            SetWidgetsState();
            CardTesterPreviousState = CardTesterState;
        }

        private void text_ProtocolPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_ProtocolPath_DoubleClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    text_ProtocolPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
