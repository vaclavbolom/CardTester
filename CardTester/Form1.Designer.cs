namespace CardTester
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn_Start = new System.Windows.Forms.Button();
			this.btn_Stop = new System.Windows.Forms.Button();
			this.label_response = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_Forward = new System.Windows.Forms.Button();
			this.btn_Backward = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btn_Start
			// 
			this.btn_Start.Location = new System.Drawing.Point(583, 95);
			this.btn_Start.Name = "btn_Start";
			this.btn_Start.Size = new System.Drawing.Size(134, 69);
			this.btn_Start.TabIndex = 0;
			this.btn_Start.Text = "Start";
			this.btn_Start.UseVisualStyleBackColor = true;
			this.btn_Start.Click += new System.EventHandler(this.button1_Click);
			// 
			// btn_Stop
			// 
			this.btn_Stop.Location = new System.Drawing.Point(583, 191);
			this.btn_Stop.Name = "btn_Stop";
			this.btn_Stop.Size = new System.Drawing.Size(127, 69);
			this.btn_Stop.TabIndex = 1;
			this.btn_Stop.Text = "Stop";
			this.btn_Stop.UseVisualStyleBackColor = true;
			this.btn_Stop.Click += new System.EventHandler(this.button2_Click);
			// 
			// label_response
			// 
			this.label_response.AutoSize = true;
			this.label_response.Location = new System.Drawing.Point(292, 148);
			this.label_response.Name = "label_response";
			this.label_response.Size = new System.Drawing.Size(52, 16);
			this.label_response.TabIndex = 2;
			this.label_response.Text = "no data";
			this.label_response.Click += new System.EventHandler(this.response_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(213, 148);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Response:";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// btn_Forward
			// 
			this.btn_Forward.Location = new System.Drawing.Point(44, 95);
			this.btn_Forward.Name = "btn_Forward";
			this.btn_Forward.Size = new System.Drawing.Size(130, 69);
			this.btn_Forward.TabIndex = 4;
			this.btn_Forward.Text = "Forward";
			this.btn_Forward.UseVisualStyleBackColor = true;
			this.btn_Forward.Click += new System.EventHandler(this.btn_Forward_Click);
			// 
			// btn_Backward
			// 
			this.btn_Backward.Location = new System.Drawing.Point(44, 191);
			this.btn_Backward.Name = "btn_Backward";
			this.btn_Backward.Size = new System.Drawing.Size(130, 69);
			this.btn_Backward.TabIndex = 5;
			this.btn_Backward.Text = "Backward";
			this.btn_Backward.UseVisualStyleBackColor = true;
			this.btn_Backward.Click += new System.EventHandler(this.btn_Backward_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btn_Backward);
			this.Controls.Add(this.btn_Forward);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label_response);
			this.Controls.Add(this.btn_Stop);
			this.Controls.Add(this.btn_Start);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_Start;
		private System.Windows.Forms.Button btn_Stop;
		private System.Windows.Forms.Label label_response;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_Forward;
		private System.Windows.Forms.Button btn_Backward;
	}
}

