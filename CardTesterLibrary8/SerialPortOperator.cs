using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Serilog;
using System.Runtime.CompilerServices;

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

    private readonly ILogger _Logger;
    private bool _CancelOperation = false;
    
    private string _PortName = string.Empty;

    private string _LastState = string.Empty;

    private string _Command = string.Empty;
    public SerialPortOperator(string portName, ILogger logger)
    {
      _Logger = logger ?? throw new ArgumentNullException(nameof( logger));
      _PortName = portName;
    }

    public async Task<int> Run()
    {
      while (!_CancelOperation)
      {
        var port = CreatePort();
        try
        {
          //open port
          port.Open();

          //read state
          var response = await port.ReadAsync(20);
          _LastState = System.Text.Encoding.UTF8.GetString(response);
          _Logger.Debug($"Read state: {_LastState}");

          //var response = port.ReadLine();
          
          //_LastState = response;

          // write command if there is command to write
          if (_Command != string.Empty)
          {
            port.WriteLine(_Command);
            _Command = string.Empty;
          }
        }
        catch(TimeoutException ex)
        {
          var message = ex.Message;
          _Logger.Error("Open port: TimeoutException");
        }
        finally
        {
          if (port.IsOpen)
          {
            port.Close();
          }
        }
        //await Task.Delay(10);
      }
      return 1;
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

    private SerialPort CreatePort()
    {
      
			var port = new SerialPort
			{
				PortName = _PortName,
				BaudRate = 9600,
				ReadTimeout = 10
			};

      return port;
		}

	}
}
