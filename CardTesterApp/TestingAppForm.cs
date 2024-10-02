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
        private readonly IMeasurementService _measurementService;
        private readonly ApplicationSettings _settings;

        private const int DELAY_COMMAND = 50;

        private readonly object[]? _fillWidgets;

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

        private DateTime StateChangeStamp { get; set; } = DateTime.UtcNow;

        private bool ServiceMode { get; set; } = false;


        public TestingAppForm(ISerialPortOperator serialPortOperator, ICardTester cardTester, ILogger logger, IMeasurementService measurementService, ApplicationSettings settings)
        {
            _serialPortOperator = serialPortOperator ?? throw new ArgumentNullException(nameof(serialPortOperator));
            _cardTester = cardTester ?? throw new ArgumentNullException(nameof(cardTester));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _measurementService = measurementService ?? throw new ArgumentNullException(nameof(measurementService));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(StateEventProcessor);
            _timer.Start();

            CalibrationStamp = DateTime.MinValue;

            _logger.Debug("Form created");
            CardTesterPreviousState = CardTesterConstants.STATE_UNKNOWN;
            CardTesterState = _serialPortOperator.GetState();

            InitializeComponent();
            tb_NubmerOfCycles.Value = _settings.NumberOfCycles;
            tb_DelayBasic.Value = _settings.DelayBasic;
            tb_DelayBend.Value = _settings.DelayBent;
            text_ProtocolPath.Text = _settings.OutputDirectory;
            label_Calibration.Text = AppConstants.CALIBRATION_NOT_SET;
            label_Message.Text = string.Empty;
            btn_Bend.Visible = false;
            btn_Unbend.Visible = false;

            foreach (var item in _settings.CardTypes)
            {
                cb_CardType.Items.Add(item);
            }

            cb_CardType.SelectedIndex = 0;
            Running = false;

            _fillWidgets = new object[]
            {
                tb_NubmerOfCycles,
                tb_DelayBasic,
                tb_DelayBend,
                text_ProtocolPath,
                textbox_Description,
                cb_CardType

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
            UpdateStateMessage();
            if (Running)
            {
                label_Cycle.Text = _cardTester.TestCycleIndex.ToString();
                CheckMeasurementTimeout();
            }
            else
            {
                if (Initialization)
                    Initialization = false;
            }

            SetWidgetsState();
            CheckCalibration();
        }

        private void SetStateChangeStamp()
        {
            StateChangeStamp = DateTime.UtcNow;
        }

        private void CheckMeasurementTimeout()
        {
            var currentStamp = DateTime.UtcNow;
            var delay = currentStamp - StateChangeStamp;

            if ((delay.TotalMilliseconds / 1000) > _settings.MeasurementTimeoutInSeconds)
            {
                Running = false;
                Task.Run(() => _cardTester.StopAsync());
                UpdateMessage("Stopped - measurement timeout");
                _logger.Error("Measurement timeout");
                //SetWidgetsState();
            }
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
            CalibrationStamp = DateTime.Now.AddHours(_settings.CalibrationValidityInHours);
            try
            {
                _measurementService.InitializeMeasurement("", "", [1, 2, 3, 4]);
            }
            catch (MeasurementServiceException e)
            {
                var msg = "Cannot initialize measurement";
                UpdateMessage(msg);
            }
        }

        public void UpdateStateMessage()
        {
            var labelMessage = CardTesterState switch
            {
                CardTesterConstants.STATE_FORWARD => "Moving FORWARD",
                CardTesterConstants.STATE_BACKWARD => "Moving BACKWARD",
                CardTesterConstants.STATE_STOPPED => "Stopped",
                CardTesterConstants.STATE_DOWN => "Position DOWN",
                CardTesterConstants.STATE_UP => "Position UP",
                CardTesterConstants.STATE_DOOR_OPEN => "Door opened",
                _ => "Unknown"
            };

            if (!label_State.Text.Equals(labelMessage.ToString()))
            {
                label_State.Text = labelMessage;
                _logger.Debug($"Message changed, state:{CardTesterState}, message:{labelMessage}");
            }
            if (!_serialPortOperator.IsDeviceConnected)
            {
                var msg = "Device disconnected";
                _logger.Warning(msg);
                UpdateMessage(msg);

            }
        }

        public void UpdateMessage(string message)
        {
            if (!label_Message.Text.Equals(message))
            {
                label_Message.Text = message;
                _logger.Debug($"Message changed: {message}");
            }
        }

        public void CardTesterStateChanged(string message)
        {
            CardTesterPreviousState = CardTesterState;
            CardTesterState = message;
            StateChangeStamp = DateTime.UtcNow;

            if (Initialization && CardTesterState == CardTesterConstants.STATE_DOWN && CardTesterPreviousState != string.Empty)
            {
                Running = false;
            }

            _logger.Debug($"Card Tester App state changed to {message}");
        }

        private void SetWidgetsState()
        {
            //Calibration, fill widgets
            bool calibrationAndFillWidgetState = !Running
                || CardTesterState == CardTesterConstants.STATE_DOWN
                || CardTesterState == CardTesterConstants.STATE_STOPPED
                || !_serialPortOperator.IsDeviceConnected;
            SetWidgets(_fillWidgets, calibrationAndFillWidgetState);
            btn_Calibration.Enabled = calibrationAndFillWidgetState;

            // Release
            bool releaseState = _serialPortOperator.IsDeviceConnected
                && (
                    !Running
                    || CardTesterState == CardTesterConstants.STATE_DOWN
                    || CardTesterState == CardTesterConstants.STATE_STOPPED
                    );
            btn_Reset.Enabled = releaseState;

            // Stop
            bool stopState = _serialPortOperator.IsDeviceConnected
                && Running;
            btn_Stop.Enabled = stopState;

            //RUN
            bool runState = _serialPortOperator.IsDeviceConnected
                && !Running
                && CardTesterState == CardTesterConstants.STATE_DOWN;
            btn_Run.Enabled = runState;

            // service buttons
            bool serviceButtonsState = _serialPortOperator.IsDeviceConnected
                && !Running
                && (CardTesterState != CardTesterConstants.STATE_BACKWARD)
                && (CardTesterState != CardTesterConstants.STATE_FORWARD);
            btn_Bend.Enabled = serviceButtonsState;
            btn_Unbend.Enabled = serviceButtonsState;

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
            //SetWidgetsState();
            UpdateMessage("Returning backward - resetting");

            await _cardTester.ResetDownAsync();
            await Task.Delay(10);

            while (Running && CardTesterState != CardTesterConstants.STATE_DOWN)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            Running = false;
            UpdateMessage("Prepared");

            _logger.Debug("DoReset finished");
        }

        private async Task DoTest()
        {
            var delayBendInMilliseconds = (int)(1000 * DelayBend);
            var delayBasicInMilliseconds = (int)(1000 * DelayBasic);
            var outputDirectory = text_ProtocolPath.Text;
            var description = textbox_Description.Text;
            var workOrderId = tb_WorkOrderId.Text;
            var cardIds = new List<int>([
                (int)tb_Card1.Value,
                (int)tb_Card2.Value,
                (int)tb_Card3.Value,
                (int)tb_Card4.Value
                ]);
            await _cardTester.RunTestAsync(NumberOfCycles, delayBendInMilliseconds, delayBasicInMilliseconds, outputDirectory, description, workOrderId, cardIds);
            await Task.Delay(DELAY_COMMAND);
        }

        private async Task DoStop()
        {
            _logger.Debug("DoStop started");

            await _cardTester.StopAsync();
            await Task.Delay(DELAY_COMMAND);

            _logger.Debug("DoStop finished");
        }



        private async void btn_Run_Click(object sender, EventArgs e)
        {
            SetStateChangeStamp();
            _logger.Debug("Run clicked");
            Running = true;
            //SetWidgetsState();
            UpdateMessage("Running test");
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
            catch (CardTesterException ex)
            {
                Running = false;
                UpdateMessage("Failed to run test");
            }
            //SetWidgetsState();
        }

        private async void btn_Stop_Click(object sender, EventArgs e)
        {
            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_GET_STATE);
            _logger.Debug("Stop clicked");
            Running = false;
            UpdateMessage("Stopped");
            //SetWidgetsState();
            await DoStop();

            while (CardTesterState != CardTesterConstants.STATE_STOPPED)
            {
                await Task.Delay(DELAY_COMMAND);
            }

            //SetWidgetsState();
            await Task.Delay(DELAY_COMMAND);
            _logger.Debug($"Stop finished, state:{CardTesterState}");
        }

        private async void btn_Reset_Click(object sender, EventArgs e)
        {
            _serialPortOperator.RunCommand(CardTesterConstants.COMMAND_GET_STATE);
            SetStateChangeStamp();
            await DoReset();
            //SetWidgetsState();
        }


        private void TestingAppForm_Load(object sender, EventArgs e)
        {
            Running = true;
            Initialization = true;
            _logger.Debug("Load form");
            var task = _cardTester.PrepareMeasurement();
            task.Wait(100);
            _logger.Debug("after 100");
            //SetWidgetsState();
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
            SetStateChangeStamp();
            await DoReset();
            Calibrate();

            //SetWidgetsState();
        }

        private async void btn_Bend_Click(object sender, EventArgs e)
        {
            SetStateChangeStamp();
            Running = true;

            await _cardTester.ResetUpAsync();

            Running = false;
        }

        private async void btn_Unbend_Click(object sender, EventArgs e)
        {
            SetStateChangeStamp();
            Running = true;

            await _cardTester.ResetDownAsync();

            Running = false;
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

        private void cb_CardType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label_CardType_Click(object sender, EventArgs e)
        {

        }

        private void gb_Buttons_Enter(object sender, EventArgs e)
        {

        }
    }
}
