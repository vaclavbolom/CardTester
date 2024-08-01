namespace CardTesterApp
{
	partial class Form1
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
			btn_SetParameters = new Button();
			label_DelayBend = new Label();
			label_DelayBasic = new Label();
			label_NumberOfCycles = new Label();
			tb_NubmerOfCycles = new NumericUpDown();
			tb_DelayBasic = new NumericUpDown();
			tb_DelayBend = new NumericUpDown();
			gb_Test = new GroupBox();
			gb_Buttons = new GroupBox();
			btn_Forward = new Button();
			btn_Backward = new Button();
			btn_Reset = new Button();
			btn_Run = new Button();
			btn_Stop = new Button();
			lablel_CycleLabel = new Label();
			label_StateLabel = new Label();
			label_Cycle = new Label();
			label_State = new Label();
			gb_Parameters.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).BeginInit();
			((System.ComponentModel.ISupportInitialize)tb_DelayBasic).BeginInit();
			((System.ComponentModel.ISupportInitialize)tb_DelayBend).BeginInit();
			gb_Test.SuspendLayout();
			gb_Buttons.SuspendLayout();
			SuspendLayout();
			// 
			// gb_Parameters
			// 
			gb_Parameters.Controls.Add(tb_DelayBend);
			gb_Parameters.Controls.Add(tb_DelayBasic);
			gb_Parameters.Controls.Add(tb_NubmerOfCycles);
			gb_Parameters.Controls.Add(btn_SetParameters);
			gb_Parameters.Controls.Add(label_DelayBend);
			gb_Parameters.Controls.Add(label_DelayBasic);
			gb_Parameters.Controls.Add(label_NumberOfCycles);
			gb_Parameters.Location = new Point(12, 12);
			gb_Parameters.Name = "gb_Parameters";
			gb_Parameters.Size = new Size(776, 186);
			gb_Parameters.TabIndex = 0;
			gb_Parameters.TabStop = false;
			gb_Parameters.Text = "Test parameters";
			gb_Parameters.Enter += groupBox1_Enter;
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
			label_DelayBasic.Click += label2_Click;
			// 
			// label_NumberOfCycles
			// 
			label_NumberOfCycles.AutoSize = true;
			label_NumberOfCycles.Location = new Point(18, 42);
			label_NumberOfCycles.Name = "label_NumberOfCycles";
			label_NumberOfCycles.Size = new Size(124, 20);
			label_NumberOfCycles.TabIndex = 0;
			label_NumberOfCycles.Text = "Number of cycles";
			label_NumberOfCycles.Click += label1_Click;
			// 
			// tb_NubmerOfCycles
			// 
			tb_NubmerOfCycles.Location = new Point(235, 40);
			tb_NubmerOfCycles.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
			tb_NubmerOfCycles.Name = "tb_NubmerOfCycles";
			tb_NubmerOfCycles.Size = new Size(97, 27);
			tb_NubmerOfCycles.TabIndex = 7;
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
			// gb_Test
			// 
			gb_Test.Controls.Add(label_State);
			gb_Test.Controls.Add(label_Cycle);
			gb_Test.Controls.Add(label_StateLabel);
			gb_Test.Controls.Add(lablel_CycleLabel);
			gb_Test.Location = new Point(12, 204);
			gb_Test.Name = "gb_Test";
			gb_Test.Size = new Size(776, 128);
			gb_Test.TabIndex = 1;
			gb_Test.TabStop = false;
			gb_Test.Text = "Test";
			// 
			// gb_Buttons
			// 
			gb_Buttons.Controls.Add(btn_Stop);
			gb_Buttons.Controls.Add(btn_Run);
			gb_Buttons.Controls.Add(btn_Reset);
			gb_Buttons.Controls.Add(btn_Backward);
			gb_Buttons.Controls.Add(btn_Forward);
			gb_Buttons.Location = new Point(12, 338);
			gb_Buttons.Name = "gb_Buttons";
			gb_Buttons.Size = new Size(776, 100);
			gb_Buttons.TabIndex = 2;
			gb_Buttons.TabStop = false;
			// 
			// btn_Forward
			// 
			btn_Forward.Location = new Point(18, 55);
			btn_Forward.Name = "btn_Forward";
			btn_Forward.Size = new Size(94, 29);
			btn_Forward.TabIndex = 0;
			btn_Forward.Text = "Forward";
			btn_Forward.UseVisualStyleBackColor = true;
			// 
			// btn_Backward
			// 
			btn_Backward.Location = new Point(129, 55);
			btn_Backward.Name = "btn_Backward";
			btn_Backward.Size = new Size(94, 29);
			btn_Backward.TabIndex = 1;
			btn_Backward.Text = "Backward";
			btn_Backward.UseVisualStyleBackColor = true;
			// 
			// btn_Reset
			// 
			btn_Reset.Location = new Point(386, 55);
			btn_Reset.Name = "btn_Reset";
			btn_Reset.Size = new Size(94, 29);
			btn_Reset.TabIndex = 2;
			btn_Reset.Text = "Release";
			btn_Reset.UseVisualStyleBackColor = true;
			// 
			// btn_Run
			// 
			btn_Run.Location = new Point(500, 55);
			btn_Run.Name = "btn_Run";
			btn_Run.Size = new Size(94, 29);
			btn_Run.TabIndex = 3;
			btn_Run.Text = "Run test";
			btn_Run.UseVisualStyleBackColor = true;
			btn_Run.Click += button4_Click;
			// 
			// btn_Stop
			// 
			btn_Stop.Location = new Point(667, 55);
			btn_Stop.Name = "btn_Stop";
			btn_Stop.Size = new Size(94, 29);
			btn_Stop.TabIndex = 4;
			btn_Stop.Text = "Stop";
			btn_Stop.UseVisualStyleBackColor = true;
			// 
			// lablel_CycleLabel
			// 
			lablel_CycleLabel.AutoSize = true;
			lablel_CycleLabel.Location = new Point(18, 34);
			lablel_CycleLabel.Name = "lablel_CycleLabel";
			lablel_CycleLabel.Size = new Size(92, 20);
			lablel_CycleLabel.TabIndex = 0;
			lablel_CycleLabel.Text = "Curent cycle:";
			lablel_CycleLabel.Click += label1_Click_1;
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
			// label_Cycle
			// 
			label_Cycle.Location = new Point(129, 34);
			label_Cycle.Name = "label_Cycle";
			label_Cycle.Size = new Size(62, 25);
			label_Cycle.TabIndex = 2;
			label_Cycle.Text = "N/A";
			label_Cycle.Click += label1_Click_2;
			// 
			// label_State
			// 
			label_State.Location = new Point(129, 77);
			label_State.Name = "label_State";
			label_State.Size = new Size(62, 25);
			label_State.TabIndex = 3;
			label_State.Text = "unknown";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(gb_Buttons);
			Controls.Add(gb_Test);
			Controls.Add(gb_Parameters);
			Name = "Form1";
			Text = "Form1";
			gb_Parameters.ResumeLayout(false);
			gb_Parameters.PerformLayout();
			((System.ComponentModel.ISupportInitialize)tb_NubmerOfCycles).EndInit();
			((System.ComponentModel.ISupportInitialize)tb_DelayBasic).EndInit();
			((System.ComponentModel.ISupportInitialize)tb_DelayBend).EndInit();
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
		private Button btn_SetParameters;
		private TextBox tb_DelayBend;
		private TextBox tb_DelayBasic;
		private NumericUpDown tb_NubmerOfCycles;
		private NumericUpDown tb_DelayBend;
		private NumericUpDown tb_DelayBasic;
		private GroupBox gb_Test;
		private GroupBox gb_Buttons;
		private Button btn_Stop;
		private Button btn_Run;
		private Button btn_Reset;
		private Button btn_Backward;
		private Button btn_Forward;
		private Label lablel_CycleLabel;
		private Label label_Cycle;
		private Label label_StateLabel;
		private Label label_State;
	}
}
