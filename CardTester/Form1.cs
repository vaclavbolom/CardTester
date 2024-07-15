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

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			myport = new SerialPort();
			myport.BaudRate = 9600;
			myport.PortName = "COM5";
			myport.Open();
			myport.WriteLine("O");
			myport.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			myport = new SerialPort();
			myport.BaudRate = 9600;
			myport.PortName = "COM5";
			myport.Open();
			myport.WriteLine("O");
			myport.Close();
		}
	}
}
