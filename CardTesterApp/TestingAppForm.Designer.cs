using Serilog;

namespace CardTesterApp
{
	partial class TestingAppForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gb_Parameters = new GroupBox();
            label_Card4 = new Label();
            label_Card3 = new Label();
            label_Card2 = new Label();
            label_Card1 = new Label();
            tb_Card4 = new NumericUpDown();
            tb_Card3 = new NumericUpDown();
            tb_Card2 = new NumericUpDown();
            tb_Card1 = new NumericUpDown();
            tb_WorkOrderId = new TextBox();
            label_WorkOrderId = new Label();
            label_CardType = new Label();
            cb_CardType = new ComboBox();
            textbox_Description = new TextBox();
            label_Description = new Label();
            text_ProtocolPath = new TextBox();
            label_ResultPath = new Label();
            tb_DelayBend = new NumericUpDown();
            tb_DelayBasic = new NumericUpDown();
            tb_NubmerOfCycles = new NumericUpDown();
            label_DelayBend = new Label();
            label_DelayBasic = new Label();
            label_NumberOfCycles = new Label();
            gb_Test = new GroupBox();
            label_Message = new Label();
            label_Calibration = new Label();
            label_State = new Label();
            label_Cycle = new Label();
            label_StateLabel = new Label();
            lablel_CycleLabel = new Label();
            gb_Buttons = new GroupBox();
            btn_Calibration = new Button();
            btn_Unbend = new Button();
            btn_Bend = new Button();
            btn_Stop = new Button();
            btn_Run = new Button();
            btn_Reset = new Button();
            gb_Parameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tb_Card4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBasic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).BeginInit();
            gb_Test.SuspendLayout();
            SuspendLayout();
            // 
            // gb_Parameters
            // 
            gb_Parameters.Controls.Add(label_Card4);
            gb_Parameters.Controls.Add(label_Card3);
            gb_Parameters.Controls.Add(label_Card2);
            gb_Parameters.Controls.Add(label_Card1);
            gb_Parameters.Controls.Add(tb_Card4);
            gb_Parameters.Controls.Add(tb_Card3);
            gb_Parameters.Controls.Add(tb_Card2);
            gb_Parameters.Controls.Add(tb_Card1);
            gb_Parameters.Controls.Add(tb_WorkOrderId);
            gb_Parameters.Controls.Add(label_WorkOrderId);
            gb_Parameters.Controls.Add(label_CardType);
            gb_Parameters.Controls.Add(cb_CardType);
            gb_Parameters.Controls.Add(textbox_Description);
            gb_Parameters.Controls.Add(label_Description);
            gb_Parameters.Controls.Add(text_ProtocolPath);
            gb_Parameters.Controls.Add(label_ResultPath);
            gb_Parameters.Controls.Add(tb_DelayBend);
            gb_Parameters.Controls.Add(tb_DelayBasic);
            gb_Parameters.Controls.Add(tb_NubmerOfCycles);
            gb_Parameters.Controls.Add(label_DelayBend);
            gb_Parameters.Controls.Add(label_DelayBasic);
            gb_Parameters.Controls.Add(label_NumberOfCycles);
            gb_Parameters.Location = new Point(10, 9);
            gb_Parameters.Margin = new Padding(3, 2, 3, 2);
            gb_Parameters.Name = "gb_Parameters";
            gb_Parameters.Padding = new Padding(3, 2, 3, 2);
            gb_Parameters.Size = new Size(679, 250);
            gb_Parameters.TabIndex = 0;
            gb_Parameters.TabStop = false;
            gb_Parameters.Text = "Test parameters";
            // 
            // label_Card4
            // 
            label_Card4.AutoSize = true;
            label_Card4.Location = new Point(544, 126);
            label_Card4.Name = "label_Card4";
            label_Card4.Size = new Size(55, 15);
            label_Card4.TabIndex = 27;
            label_Card4.Text = "Card 4 ID";
            // 
            // label_Card3
            // 
            label_Card3.AutoSize = true;
            label_Card3.Location = new Point(371, 126);
            label_Card3.Name = "label_Card3";
            label_Card3.Size = new Size(55, 15);
            label_Card3.TabIndex = 26;
            label_Card3.Text = "Card 3 ID";
            // 
            // label_Card2
            // 
            label_Card2.AutoSize = true;
            label_Card2.Location = new Point(193, 126);
            label_Card2.Name = "label_Card2";
            label_Card2.Size = new Size(55, 15);
            label_Card2.TabIndex = 25;
            label_Card2.Text = "Card 2 ID";
            // 
            // label_Card1
            // 
            label_Card1.AutoSize = true;
            label_Card1.Location = new Point(14, 126);
            label_Card1.Name = "label_Card1";
            label_Card1.Size = new Size(55, 15);
            label_Card1.TabIndex = 24;
            label_Card1.Text = "Card 1 ID";
            // 
            // tb_Card4
            // 
            tb_Card4.Location = new Point(544, 144);
            tb_Card4.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            tb_Card4.Name = "tb_Card4";
            tb_Card4.Size = new Size(120, 23);
            tb_Card4.TabIndex = 23;
            // 
            // tb_Card3
            // 
            tb_Card3.Location = new Point(371, 144);
            tb_Card3.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            tb_Card3.Name = "tb_Card3";
            tb_Card3.Size = new Size(120, 23);
            tb_Card3.TabIndex = 22;
            // 
            // tb_Card2
            // 
            tb_Card2.Location = new Point(193, 144);
            tb_Card2.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            tb_Card2.Name = "tb_Card2";
            tb_Card2.Size = new Size(120, 23);
            tb_Card2.TabIndex = 21;
            // 
            // tb_Card1
            // 
            tb_Card1.Location = new Point(14, 144);
            tb_Card1.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            tb_Card1.Name = "tb_Card1";
            tb_Card1.Size = new Size(120, 23);
            tb_Card1.TabIndex = 20;
            // 
            // tb_WorkOrderId
            // 
            tb_WorkOrderId.Location = new Point(535, 62);
            tb_WorkOrderId.Name = "tb_WorkOrderId";
            tb_WorkOrderId.Size = new Size(131, 23);
            tb_WorkOrderId.TabIndex = 17;
            tb_WorkOrderId.Leave += tb_WorkOrderId_Leave;
            // 
            // label_WorkOrderId
            // 
            label_WorkOrderId.AutoSize = true;
            label_WorkOrderId.Location = new Point(452, 65);
            label_WorkOrderId.Name = "label_WorkOrderId";
            label_WorkOrderId.Size = new Size(85, 15);
            label_WorkOrderId.TabIndex = 16;
            label_WorkOrderId.Text = "Work Order ID:";
            // 
            // label_CardType
            // 
            label_CardType.AutoSize = true;
            label_CardType.Location = new Point(451, 32);
            label_CardType.Name = "label_CardType";
            label_CardType.Size = new Size(61, 15);
            label_CardType.TabIndex = 15;
            label_CardType.Text = "Job name:";
            label_CardType.Click += label_CardType_Click;
            // 
            // cb_CardType
            // 
            cb_CardType.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_CardType.Location = new Point(533, 29);
            cb_CardType.Margin = new Padding(3, 2, 3, 2);
            cb_CardType.Name = "cb_CardType";
            cb_CardType.Size = new Size(133, 23);
            cb_CardType.TabIndex = 14;
            cb_CardType.SelectedIndexChanged += cb_CardType_SelectedIndexChanged;
            // 
            // textbox_Description
            // 
            textbox_Description.Location = new Point(204, 223);
            textbox_Description.Margin = new Padding(3, 2, 3, 2);
            textbox_Description.Name = "textbox_Description";
            textbox_Description.Size = new Size(461, 23);
            textbox_Description.TabIndex = 13;
            // 
            // label_Description
            // 
            label_Description.AutoSize = true;
            label_Description.Location = new Point(14, 223);
            label_Description.Name = "label_Description";
            label_Description.Size = new Size(89, 15);
            label_Description.TabIndex = 12;
            label_Description.Text = "Test description";
            // 
            // text_ProtocolPath
            // 
            text_ProtocolPath.BorderStyle = BorderStyle.FixedSingle;
            text_ProtocolPath.Location = new Point(204, 192);
            text_ProtocolPath.Margin = new Padding(3, 2, 3, 2);
            text_ProtocolPath.Name = "text_ProtocolPath";
            text_ProtocolPath.Size = new Size(460, 23);
            text_ProtocolPath.TabIndex = 11;
            text_ProtocolPath.TextChanged += text_ProtocolPath_TextChanged;
            text_ProtocolPath.DoubleClick += text_ProtocolPath_DoubleClick;
            // 
            // label_ResultPath
            // 
            label_ResultPath.AutoSize = true;
            label_ResultPath.Location = new Point(14, 194);
            label_ResultPath.Name = "label_ResultPath";
            label_ResultPath.Size = new Size(114, 15);
            label_ResultPath.TabIndex = 10;
            label_ResultPath.Text = "Result protocol path";
            // 
            // tb_DelayBend
            // 
            tb_DelayBend.DecimalPlaces = 1;
            tb_DelayBend.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tb_DelayBend.Location = new Point(204, 92);
            tb_DelayBend.Margin = new Padding(3, 2, 3, 2);
            tb_DelayBend.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tb_DelayBend.Name = "tb_DelayBend";
            tb_DelayBend.Size = new Size(85, 23);
            tb_DelayBend.TabIndex = 9;
            // 
            // tb_DelayBasic
            // 
            tb_DelayBasic.DecimalPlaces = 1;
            tb_DelayBasic.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tb_DelayBasic.Location = new Point(204, 61);
            tb_DelayBasic.Margin = new Padding(3, 2, 3, 2);
            tb_DelayBasic.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tb_DelayBasic.Name = "tb_DelayBasic";
            tb_DelayBasic.Size = new Size(85, 23);
            tb_DelayBasic.TabIndex = 8;
            // 
            // tb_NubmerOfCycles
            // 
            tb_NubmerOfCycles.Location = new Point(206, 30);
            tb_NubmerOfCycles.Margin = new Padding(3, 2, 3, 2);
            tb_NubmerOfCycles.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            tb_NubmerOfCycles.Name = "tb_NubmerOfCycles";
            tb_NubmerOfCycles.Size = new Size(85, 23);
            tb_NubmerOfCycles.TabIndex = 7;
            // 
            // label_DelayBend
            // 
            label_DelayBend.AutoSize = true;
            label_DelayBend.Location = new Point(14, 97);
            label_DelayBend.Name = "label_DelayBend";
            label_DelayBend.Size = new Size(120, 15);
            label_DelayBend.TabIndex = 2;
            label_DelayBend.Text = "Wait in bend position";
            // 
            // label_DelayBasic
            // 
            label_DelayBasic.AutoSize = true;
            label_DelayBasic.Location = new Point(14, 62);
            label_DelayBasic.Name = "label_DelayBasic";
            label_DelayBasic.Size = new Size(120, 15);
            label_DelayBasic.TabIndex = 1;
            label_DelayBasic.Text = "Wait in basic position";
            // 
            // label_NumberOfCycles
            // 
            label_NumberOfCycles.AutoSize = true;
            label_NumberOfCycles.Location = new Point(16, 32);
            label_NumberOfCycles.Name = "label_NumberOfCycles";
            label_NumberOfCycles.Size = new Size(100, 15);
            label_NumberOfCycles.TabIndex = 0;
            label_NumberOfCycles.Text = "Number of cycles";
            // 
            // gb_Test
            // 
            gb_Test.Controls.Add(label_Message);
            gb_Test.Controls.Add(label_Calibration);
            gb_Test.Controls.Add(label_State);
            gb_Test.Controls.Add(label_Cycle);
            gb_Test.Controls.Add(label_StateLabel);
            gb_Test.Controls.Add(lablel_CycleLabel);
            gb_Test.Location = new Point(9, 263);
            gb_Test.Margin = new Padding(3, 2, 3, 2);
            gb_Test.Name = "gb_Test";
            gb_Test.Padding = new Padding(3, 2, 3, 2);
            gb_Test.Size = new Size(679, 96);
            gb_Test.TabIndex = 1;
            gb_Test.TabStop = false;
            gb_Test.Text = "Test";
            // 
            // label_Message
            // 
            label_Message.Location = new Point(409, 63);
            label_Message.Name = "label_Message";
            label_Message.Size = new Size(257, 23);
            label_Message.TabIndex = 5;
            label_Message.Text = "label1";
            // 
            // label_Calibration
            // 
            label_Calibration.Location = new Point(409, 23);
            label_Calibration.Name = "label_Calibration";
            label_Calibration.Size = new Size(257, 23);
            label_Calibration.TabIndex = 4;
            label_Calibration.Text = "label1";
            // 
            // label_State
            // 
            label_State.Location = new Point(113, 63);
            label_State.Name = "label_State";
            label_State.Size = new Size(290, 19);
            label_State.TabIndex = 3;
            label_State.Text = "unknown";
            // 
            // label_Cycle
            // 
            label_Cycle.Location = new Point(113, 31);
            label_Cycle.Name = "label_Cycle";
            label_Cycle.Size = new Size(54, 19);
            label_Cycle.TabIndex = 2;
            label_Cycle.Text = "N/A";
            // 
            // label_StateLabel
            // 
            label_StateLabel.AutoSize = true;
            label_StateLabel.Location = new Point(16, 63);
            label_StateLabel.Name = "label_StateLabel";
            label_StateLabel.Size = new Size(81, 15);
            label_StateLabel.TabIndex = 1;
            label_StateLabel.Text = "Current state: ";
            // 
            // lablel_CycleLabel
            // 
            lablel_CycleLabel.AutoSize = true;
            lablel_CycleLabel.Location = new Point(16, 31);
            lablel_CycleLabel.Name = "lablel_CycleLabel";
            lablel_CycleLabel.Size = new Size(76, 15);
            lablel_CycleLabel.TabIndex = 0;
            lablel_CycleLabel.Text = "Curent cycle:";
            // 
            // gb_Buttons
            // 
            gb_Buttons.Location = new Point(10, 363);
            gb_Buttons.Margin = new Padding(3, 2, 3, 2);
            gb_Buttons.Name = "gb_Buttons";
            gb_Buttons.Padding = new Padding(3, 2, 3, 2);
            gb_Buttons.Size = new Size(679, 54);
            gb_Buttons.TabIndex = 2;
            gb_Buttons.TabStop = false;
            gb_Buttons.Enter += gb_Buttons_Enter;
            // 
            // btn_Calibration
            // 
            btn_Calibration.Location = new Point(331, 385);
            btn_Calibration.Margin = new Padding(3, 2, 3, 2);
            btn_Calibration.Name = "btn_Calibration";
            btn_Calibration.Size = new Size(82, 22);
            btn_Calibration.TabIndex = 7;
            btn_Calibration.Text = "Calibration";
            btn_Calibration.UseVisualStyleBackColor = true;
            btn_Calibration.Click += btn_Calibration_Click;
            // 
            // btn_Unbend
            // 
            btn_Unbend.Location = new Point(134, 385);
            btn_Unbend.Margin = new Padding(3, 2, 3, 2);
            btn_Unbend.Name = "btn_Unbend";
            btn_Unbend.Size = new Size(82, 22);
            btn_Unbend.TabIndex = 6;
            btn_Unbend.Text = "Unbend";
            btn_Unbend.UseVisualStyleBackColor = true;
            btn_Unbend.Click += btn_Unbend_Click;
            // 
            // btn_Bend
            // 
            btn_Bend.Location = new Point(31, 385);
            btn_Bend.Margin = new Padding(3, 2, 3, 2);
            btn_Bend.Name = "btn_Bend";
            btn_Bend.Size = new Size(82, 22);
            btn_Bend.TabIndex = 5;
            btn_Bend.Text = "Bend";
            btn_Bend.UseVisualStyleBackColor = true;
            btn_Bend.Click += btn_Bend_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.Location = new Point(594, 385);
            btn_Stop.Margin = new Padding(3, 2, 3, 2);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(82, 22);
            btn_Stop.TabIndex = 4;
            btn_Stop.Text = "Stop";
            btn_Stop.UseVisualStyleBackColor = true;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // btn_Run
            // 
            btn_Run.Location = new Point(506, 385);
            btn_Run.Margin = new Padding(3, 2, 3, 2);
            btn_Run.Name = "btn_Run";
            btn_Run.Size = new Size(82, 22);
            btn_Run.TabIndex = 3;
            btn_Run.Text = "Run test";
            btn_Run.UseVisualStyleBackColor = true;
            btn_Run.Click += btn_Run_Click;
            // 
            // btn_Reset
            // 
            btn_Reset.Location = new Point(419, 385);
            btn_Reset.Margin = new Padding(3, 2, 3, 2);
            btn_Reset.Name = "btn_Reset";
            btn_Reset.Size = new Size(82, 22);
            btn_Reset.TabIndex = 2;
            btn_Reset.Text = "Release";
            btn_Reset.UseVisualStyleBackColor = true;
            btn_Reset.Click += btn_Reset_Click;
            // 
            // TestingAppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 423);
            Controls.Add(btn_Calibration);
            Controls.Add(btn_Unbend);
            Controls.Add(btn_Bend);
            Controls.Add(gb_Test);
            Controls.Add(btn_Stop);
            Controls.Add(gb_Parameters);
            Controls.Add(btn_Run);
            Controls.Add(btn_Reset);
            Controls.Add(gb_Buttons);
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "TestingAppForm";
            Text = "Bending Machine Operator";
            Load += TestingAppForm_Load;
            KeyDown += TestingAppForm_KeyDown;
            gb_Parameters.ResumeLayout(false);
            gb_Parameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tb_Card4).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card3).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card2).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_Card1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBend).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBasic).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).EndInit();
            gb_Test.ResumeLayout(false);
            gb_Test.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gb_Parameters;
		private Label label_DelayBend;
		private Label label_DelayBasic;
		private Label label_NumberOfCycles;
		private NumericUpDown tb_NubmerOfCycles;
		private NumericUpDown tb_DelayBend;
		private NumericUpDown tb_DelayBasic;
		private GroupBox gb_Test;
		private GroupBox gb_Buttons;
		private Button btn_Stop;
		private Button btn_Run;
		private Button btn_Reset;
		private Label lablel_CycleLabel;
		private Label label_Cycle;
		private Label label_StateLabel;
		private Label label_State;
        private TextBox text_ProtocolPath;
        private Label label_ResultPath;
        private TextBox textbox_Description;
        private Label label_Description;
        private Button btn_Unbend;
        private Button btn_Bend;
        private Button btn_Calibration;
        private Label label_CardType;
        private ComboBox cb_CardType;
        private Label label_Calibration;
        private Label label_Message;
        private TextBox tb_WorkOrderId;
        private Label label_WorkOrderId;
        private Label label_Card4;
        private Label label_Card3;
        private Label label_Card2;
        private Label label_Card1;
        private NumericUpDown tb_Card4;
        private NumericUpDown tb_Card3;
        private NumericUpDown tb_Card2;
        private NumericUpDown tb_Card1;
    }
}
