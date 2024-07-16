using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace CardTester
{
	public partial class Form1 : Form
	{
		public SerialPort myport;
		public Form1()
		{
			InitializeComponent();
		}

		private string RunLoop(string command)
		{
			var response = string.Empty;
			var myport = new SerialPort();
			myport.BaudRate = 9600;
			myport.PortName = "COM4";
			myport.ReadTimeout = 10;
			myport.Open();

			myport.WriteLine(command);
			myport.Close();
			while (true) {
				myport.Open();
				try
				{
					response = myport.ReadLine().Trim();
				}
				catch (TimeoutException) { }
				myport.Close();
				
				if (!response.Equals(command) && !response.Equals(string.Empty))
					break;
			}

			return response;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			myport = new SerialPort();
			myport.BaudRate = 9600;
			myport.PortName = "COM4";
			myport.Open();
			myport.WriteLine("O");

			var response = myport.ReadLine();
			label_response.Text = response;
			myport.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var response = RunLoop("S");
			label_response.Text = response;

		}

		private void response_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{
			
		}

		private void btn_Forward_Click(object sender, EventArgs e)
		{
			var response = RunLoop("F");
			label_response.Text = response;
		}

		private void btn_Backward_Click(object sender, EventArgs e)
		{
			var response = RunLoop("B");
			label_response.Text = response;
		}
	}
}
