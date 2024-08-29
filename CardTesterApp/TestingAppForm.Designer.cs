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
            text_ProtocolPath = new TextBox();
            label_ResultPath = new Label();
            tb_DelayBend = new NumericUpDown();
            tb_DelayBasic = new NumericUpDown();
            tb_NubmerOfCycles = new NumericUpDown();
            btn_SetParameters = new Button();
            label_DelayBend = new Label();
            label_DelayBasic = new Label();
            label_NumberOfCycles = new Label();
            gb_Test = new GroupBox();
            label_State = new Label();
            label_Cycle = new Label();
            label_StateLabel = new Label();
            lablel_CycleLabel = new Label();
            gb_Buttons = new GroupBox();
            btn_Stop = new Button();
            btn_Run = new Button();
            btn_Reset = new Button();
            label_Description = new Label();
            textbox_Description = new TextBox();
            gb_Parameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBasic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).BeginInit();
            gb_Test.SuspendLayout();
            gb_Buttons.SuspendLayout();
            SuspendLayout();
            // 
            // gb_Parameters
            // 
            gb_Parameters.Controls.Add(textbox_Description);
            gb_Parameters.Controls.Add(label_Description);
            gb_Parameters.Controls.Add(text_ProtocolPath);
            gb_Parameters.Controls.Add(label_ResultPath);
            gb_Parameters.Controls.Add(tb_DelayBend);
            gb_Parameters.Controls.Add(tb_DelayBasic);
            gb_Parameters.Controls.Add(tb_NubmerOfCycles);
            gb_Parameters.Controls.Add(btn_SetParameters);
            gb_Parameters.Controls.Add(label_DelayBend);
            gb_Parameters.Controls.Add(label_DelayBasic);
            gb_Parameters.Controls.Add(label_NumberOfCycles);
            gb_Parameters.Location = new Point(12, 12);
            gb_Parameters.Name = "gb_Parameters";
            gb_Parameters.Size = new Size(776, 224);
            gb_Parameters.TabIndex = 0;
            gb_Parameters.TabStop = false;
            gb_Parameters.Text = "Test parameters";
            // 
            // text_ProtocolPath
            // 
            text_ProtocolPath.BorderStyle = BorderStyle.FixedSingle;
            text_ProtocolPath.Location = new Point(235, 141);
            text_ProtocolPath.Name = "text_ProtocolPath";
            text_ProtocolPath.Size = new Size(526, 27);
            text_ProtocolPath.TabIndex = 11;
            text_ProtocolPath.TextChanged += text_ProtocolPath_TextChanged;
            text_ProtocolPath.DoubleClick += text_ProtocolPath_DoubleClick;
            // 
            // label_ResultPath
            // 
            label_ResultPath.AutoSize = true;
            label_ResultPath.Location = new Point(18, 144);
            label_ResultPath.Name = "label_ResultPath";
            label_ResultPath.Size = new Size(144, 20);
            label_ResultPath.TabIndex = 10;
            label_ResultPath.Text = "Result protocol path";
            // 
            // tb_DelayBend
            // 
            tb_DelayBend.DecimalPlaces = 1;
            tb_DelayBend.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tb_DelayBend.Location = new Point(235, 105);
            tb_DelayBend.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tb_DelayBend.Name = "tb_DelayBend";
            tb_DelayBend.Size = new Size(97, 27);
            tb_DelayBend.TabIndex = 9;
            // 
            // tb_DelayBasic
            // 
            tb_DelayBasic.DecimalPlaces = 1;
            tb_DelayBasic.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            tb_DelayBasic.Location = new Point(235, 73);
            tb_DelayBasic.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tb_DelayBasic.Name = "tb_DelayBasic";
            tb_DelayBasic.Size = new Size(97, 27);
            tb_DelayBasic.TabIndex = 8;
            // 
            // tb_NubmerOfCycles
            // 
            tb_NubmerOfCycles.Location = new Point(235, 40);
            tb_NubmerOfCycles.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            tb_NubmerOfCycles.Name = "tb_NubmerOfCycles";
            tb_NubmerOfCycles.Size = new Size(97, 27);
            tb_NubmerOfCycles.TabIndex = 7;
            // 
            // btn_SetParameters
            // 
            btn_SetParameters.Location = new Point(458, 42);
            btn_SetParameters.Name = "btn_SetParameters";
            btn_SetParameters.Size = new Size(160, 90);
            btn_SetParameters.TabIndex = 6;
            btn_SetParameters.Text = "Set parameters";
            btn_SetParameters.UseVisualStyleBackColor = true;
            // 
            // label_DelayBend
            // 
            label_DelayBend.AutoSize = true;
            label_DelayBend.Location = new Point(18, 112);
            label_DelayBend.Name = "label_DelayBend";
            label_DelayBend.Size = new Size(151, 20);
            label_DelayBend.TabIndex = 2;
            label_DelayBend.Text = "Wait in bend position";
            // 
            // label_DelayBasic
            // 
            label_DelayBasic.AutoSize = true;
            label_DelayBasic.Location = new Point(18, 75);
            label_DelayBasic.Name = "label_DelayBasic";
            label_DelayBasic.Size = new Size(151, 20);
            label_DelayBasic.TabIndex = 1;
            label_DelayBasic.Text = "Wait in basic position";
            // 
            // label_NumberOfCycles
            // 
            label_NumberOfCycles.AutoSize = true;
            label_NumberOfCycles.Location = new Point(18, 42);
            label_NumberOfCycles.Name = "label_NumberOfCycles";
            label_NumberOfCycles.Size = new Size(124, 20);
            label_NumberOfCycles.TabIndex = 0;
            label_NumberOfCycles.Text = "Number of cycles";
            // 
            // gb_Test
            // 
            gb_Test.Controls.Add(label_State);
            gb_Test.Controls.Add(label_Cycle);
            gb_Test.Controls.Add(label_StateLabel);
            gb_Test.Controls.Add(lablel_CycleLabel);
            gb_Test.Location = new Point(12, 242);
            gb_Test.Name = "gb_Test";
            gb_Test.Size = new Size(776, 128);
            gb_Test.TabIndex = 1;
            gb_Test.TabStop = false;
            gb_Test.Text = "Test";
            // 
            // label_State
            // 
            label_State.Location = new Point(129, 77);
            label_State.Name = "label_State";
            label_State.Size = new Size(632, 25);
            label_State.TabIndex = 3;
            label_State.Text = "unknown";
            // 
            // label_Cycle
            // 
            label_Cycle.Location = new Point(129, 34);
            label_Cycle.Name = "label_Cycle";
            label_Cycle.Size = new Size(62, 25);
            label_Cycle.TabIndex = 2;
            label_Cycle.Text = "N/A";
            // 
            // label_StateLabel
            // 
            label_StateLabel.AutoSize = true;
            label_StateLabel.Location = new Point(18, 77);
            label_StateLabel.Name = "label_StateLabel";
            label_StateLabel.Size = new Size(100, 20);
            label_StateLabel.TabIndex = 1;
            label_StateLabel.Text = "Current state: ";
            // 
            // lablel_CycleLabel
            // 
            lablel_CycleLabel.AutoSize = true;
            lablel_CycleLabel.Location = new Point(18, 34);
            lablel_CycleLabel.Name = "lablel_CycleLabel";
            lablel_CycleLabel.Size = new Size(92, 20);
            lablel_CycleLabel.TabIndex = 0;
            lablel_CycleLabel.Text = "Curent cycle:";
            // 
            // gb_Buttons
            // 
            gb_Buttons.Controls.Add(btn_Stop);
            gb_Buttons.Controls.Add(btn_Run);
            gb_Buttons.Controls.Add(btn_Reset);
            gb_Buttons.Location = new Point(12, 366);
            gb_Buttons.Name = "gb_Buttons";
            gb_Buttons.Size = new Size(776, 72);
            gb_Buttons.TabIndex = 2;
            gb_Buttons.TabStop = false;
            // 
            // btn_Stop
            // 
            btn_Stop.Location = new Point(667, 26);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(94, 29);
            btn_Stop.TabIndex = 4;
            btn_Stop.Text = "Stop";
            btn_Stop.UseVisualStyleBackColor = true;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // btn_Run
            // 
            btn_Run.Location = new Point(498, 26);
            btn_Run.Name = "btn_Run";
            btn_Run.Size = new Size(94, 29);
            btn_Run.TabIndex = 3;
            btn_Run.Text = "Run test";
            btn_Run.UseVisualStyleBackColor = true;
            btn_Run.Click += btn_Run_Click;
            // 
            // btn_Reset
            // 
            btn_Reset.Location = new Point(383, 26);
            btn_Reset.Name = "btn_Reset";
            btn_Reset.Size = new Size(94, 29);
            btn_Reset.TabIndex = 2;
            btn_Reset.Text = "Release";
            btn_Reset.UseVisualStyleBackColor = true;
            btn_Reset.Click += btn_Reset_Click;
            // 
            // label_Description
            // 
            label_Description.AutoSize = true;
            label_Description.Location = new Point(18, 176);
            label_Description.Name = "label_Description";
            label_Description.Size = new Size(113, 20);
            label_Description.TabIndex = 12;
            label_Description.Text = "Test description";
            // 
            // textbox_Description
            // 
            textbox_Description.Location = new Point(235, 176);
            textbox_Description.Name = "textbox_Description";
            textbox_Description.Size = new Size(526, 27);
            textbox_Description.TabIndex = 13;
            // 
            // TestingAppForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(gb_Buttons);
            Controls.Add(gb_Test);
            Controls.Add(gb_Parameters);
            Name = "TestingAppForm";
            Text = "Form1";
            Load += TestingAppForm_Load;
            gb_Parameters.ResumeLayout(false);
            gb_Parameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBend).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_DelayBasic).EndInit();
            ((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).EndInit();
            gb_Test.ResumeLayout(false);
            gb_Test.PerformLayout();
            gb_Buttons.ResumeLayout(false);
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
        private Button btn_SetParameters;
        private TextBox text_ProtocolPath;
        private Label label_ResultPath;
        private TextBox textbox_Description;
        private Label label_Description;
    }
}
