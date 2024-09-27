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
        private readonly ApplicationSettings _settings;

        private const int DELAY_COMMAND = 50;

        private object[]? _disabledDuringRunWidgets;
        private object[]? _disabledDuringStopWidgets;
        private object[]? _disabledWhenStoppedWidgets;

        private System.Windows.Forms.Timer _timer;


        private bool Running { get; set; }
        private bool Initialization { get; set; } = false;

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

        private string CardTesterState { get; set; }

        private string CardTesterPreviousState { get; set; }

        private DateTime CalibrationStamp { get; set; }

        private bool ServiceMode { get; set; } = false;


        public TestingAppForm(ISerialPortOperator serialPortOperator, ICardTester cardTester, ILogger logger, ApplicationSettings settings)
        {
            _serialPortOperator = serialPortOperator ?? throw new ArgumentNullException(nameof(serialPortOperator));
            _cardTester = cardTester ?? throw new ArgumentNullException(nameof(cardTester));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(StateEventProcessor);
            _timer.Start();

            CalibrationStamp = DateTime.MinValue;

            _logger.Debug("Form created");
            CardTesterState = _serialPortOperator.GetState();

            InitializeComponent();
            tb_NubmerOfCycles.Value = _settings.NumberOfCycles;
            tb_DelayBasic.Value = _settings.DelayBasic;
            tb_DelayBend.Value = _settings.DelayBent;
            text_ProtocolPath.Text = _settings.OutputDirectory;
            label_Calibration.Text = AppConstants.CALIBRATION_NOT_SET;
            foreach (var item in _settings.CardTypes)
            {
                cb_CardType.Items.Add(item);
            }

            cb_CardType.SelectedIndex = 0;
            Running = false;

            _disabledDuringRunWidgets = new object[]
            {
                tb_NubmerOfCycles,
                tb_DelayBasic,
                tb_DelayBend,
                btn_Reset,
                btn_Run,
                text_ProtocolPath,
                textbox_Description
            };
            _disabledDuringStopWidgets = new object[]
            {
                btn_Stop
            };
            _disabledWhenStoppedWidgets = new object[]
            {
                btn_Run,
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
            if (Running)
            {
                UpdateMessage();
                label_Cycle.Text = _cardTester.TestCycleIndex.ToString();
            }
            else 
            {
                if (Initialization)
                    Initialization = false;
                UpdateMessage();
                SetWidgetsState();
            }

            CheckCalibration();
        }

        private void CheckCalibration()
        {
            var currentStamp = DateTime.Now;
            var calibrationLabel = string.Empty;
            var CalibrationValid = (CalibrationStamp > currentStamp);
            if (!CalibrationValid)
            {
                calibrationLabel = (CalibrationStamp.Equals(DateTime.MinValue))
                    ? AppConstants.CALIBRATION_NOT_SET
                    : AppConstants.CALIBRATION_EXPIRED;
            }
            else
            {
                calibrationLabel = AppConstants.CALIBRATION_OK;
            }
            if (!label_Calibration.Equals(calibrationLabel))
            {
                label_Calibration.Text = calibrationLabel;
                label_Calibration.ForeColor = CalibrationValid ? Color.Green : Color.Red;
            }
        }

        private void Calibrate()
        {
            //TODO: run release
            CalibrationStamp = DateTime.Now.AddHours(_settings.CalibrationValidityInHours);
            //TODO: interact with smart card reader, set up to initial position
        }

        public void UpdateMessage(string message = "")
        {
            var labelMessage = (message.Equals(string.Empty))
                ? CardTesterState switch
                {
                    CardTesterConstants.STATE_FORWARD => "Moving FORWARD",
                    CardTesterConstants.STATE_BACKWARD => "Moving BACKWARD",
                    CardTesterConstants.STATE_STOPPED => "Stopped",
                    CardTesterConstants.STATE_DOWN => "Position DOWN",
                    CardTesterConstants.STATE_UP => "Position UP",
                    CardTesterConstants.STATE_DOOR_OPEN => "Door opened",
                    _ => "Unknown"
                }
                 : message;

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

            if (Initialization && CardTesterState == CardTesterConstants.STATE_DOWN && CardTesterPreviousState != string.Empty)
            {
                Running = false;
            }

            _logger.Debug($"Card Tester App state changed to {message}");
        }

        private void SetWidgetsState()
        {
            if (_disabledDuringRunWidgets != null)
            {
                var desiredState = !Running;
                SetWidgets(_disabledDuringRunWidgets, desiredState);
            }

            if (_disabledDuringStopWidgets != null)
            {
                var desiredState = Running;
                SetWidgets(_disabledDuringStopWidgets, desiredState);
            }
            if (_disabledWhenStoppedWidgets != null && CardTesterState == CardTesterConstants.STATE_STOPPED)
            {
                var desiredState = (CardTesterState == CardTesterConstants.STATE_STOPPED);
                SetWidgets(_disabledWhenStoppedWidgets, desiredState);
            }

        }

        private void SetWidgets(object[] widgets, bool desiredState)
        {
            foreach (var widget in widgets)
            {
                if (widget is Button)
                    ((Button)widget).Enabled = desiredState;
                if (widget is NumericUpDown)
                    ((NumericUpDown)widget).Enabled = desiredState;
                if (widget is TextBox)
                    ((TextBox)widget).Enabled = desiredState;
            }
        }

        private async Task DoReset()
        {
            _logger.Debug("DoReset started");

            Running = true;
            SetWidgetsState();
            UpdateMessage("Returning backward - resetting");

            await _cardTester.ResetDownAsync();
            await Task.Delay(10);

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            Running = false;
            UpdateMessage();

            _logger.Debug("DoReset finished");
        }

        private async Task DoTest()
        {
            var delayBendInMilliseconds = (int)(1000 * DelayBend);
            var delayBasicInMilliseconds = (int)(1000 * DelayBasic);
            var outputDirectory = text_ProtocolPath.Text;
            var description = textbox_Description.Text;
            await _cardTester.RunTestAsync(NumberOfCycles, delayBendInMilliseconds, delayBasicInMilliseconds, outputDirectory, description);
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
            SetWidgetsState();
            try
            {
                await DoTest();

                while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
                {
                    await Task.Delay(DELAY_COMMAND);
                }

                Running = false;
                if (CardTesterState != CardTesterConstants.STATE_STOPPED && CardTesterState != CardTesterConstants.STATE_DOOR_OPEN)
                    UpdateMessage("Test finished");
            }
            catch(CardTesterException ex)
            {
                Running = false;
                UpdateMessage("Failed to run test");
            }
            SetWidgetsState();
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            _logger.Debug("Stop clicked");
            Running = false;
            UpdateMessage("Stopped");
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
            await DoReset();
            SetWidgetsState();
        }


        private void TestingAppForm_Load(object sender, EventArgs e)
        {
            Running = true;
            Initialization = true;
            _logger.Debug("Load form");
            var task = _cardTester.PrepareMeasurement();
            task.Wait(100);
            _logger.Debug("after 100");
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

        private async void btn_Calibration_Click(object sender, EventArgs e)
        {
            await  DoReset();
            Calibrate();

            SetWidgetsState();
        }
        

        private void TestingAppForm_KeyDown(object sender, KeyEventArgs e)
        {
            // enter service mode
            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                ServiceMode = !ServiceMode;

                btn_Bend.Visible = ServiceMode;
                btn_Unbend.Visible = ServiceMode;


                e.SuppressKeyPress = true;
            }
        }

        private async void btn_Bend_Click(object sender, EventArgs e)
        {
            Running = true;

            await _cardTester.MoveForwardUnsafeAsync();

            Running = false;
            UpdateMessage();
        }

        private async void btn_Unbend_Click(object sender, EventArgs e)
        {
            Running = true;

            await _cardTester.MoveBackwardUnsafeAsync();

            Running = false;
            UpdateMessage();
        }
    }
}
