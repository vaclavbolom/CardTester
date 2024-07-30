using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace CardTester
{
    public class SerialPortOperator : ISerialPortOperator
    {

      private const string FORWARD_STATE = "f";
      private const string BACKWARD_STATE = "b";
      private const string UP_STATE = "u";
      private const string DOWN_STATE = "d";
      private const string STOPPED_STATE = "s";

      private const string MOVE_FORWARD = "1";
      private const string MOVE_BACKWARD = "2";
      private const string STOP = "0";

      private bool _CancelOperation = false;
    
      private string _PortName = string.Empty;

      private string _LastState = string.Empty;

      private string _Command = string.Empty;
      public SerialPortOperator(string portName)
      {
        _PortName = portName;
      }

        public async Task Run()
      {
        while (!_CancelOperation)
        {
          //open port
          var port = OpenPort();

          //read state
          //var response = await port.ReadAsync(5);
          //_LastState = System.Text.Encoding.UTF8.GetString(response);

          var response = port.ReadLine();
        _LastState = response;

          // write command if there is command to write
          if (_Command != string.Empty)
          {
            port.WriteLine(_Command);
            _Command = string.Empty;
          }
          port.Close();
          await Task.Delay(1000);
        }
      }

      public void Stop()
      {
        _CancelOperation = true;
      }


		  public void RunCommand(string command)
      {

      }

      public string ReadState()
      {
        return string.Empty;
      }

    private SerialPort OpenPort()
    {
			var port = new SerialPort
			{
				PortName = _PortName,
				BaudRate = 9600,
				ReadTimeout = 10
			};

      port.Open();

      return port;
		}

	}
}
