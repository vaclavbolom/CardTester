using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

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
        private const string RESET = "3";

        private readonly ILogger _Logger;
        private readonly int _WriteDelay = 10;

        private bool _CancelOperation = false;

        private string _PortName = string.Empty;

        private string _Command = string.Empty;
        private string _PreviousCommand = string.Empty;
        private string _State = string.Empty;
        private string _PreviousState = string.Empty;

        

        private ProcessDataDelegate? _ProcessDataMethod;

        public SerialPortOperator(string portName, ILogger logger)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PortName = portName;
        }



        public async Task Run(ProcessDataDelegate processDataMethod)
        {
            _ProcessDataMethod = processDataMethod ?? throw new ArgumentNullException(nameof(processDataMethod));
            using (var port = CreatePort())
            {
                try
                {
                    port.Open();
                    var readTask = ReadFromSerialPort(port);
                    var writeTask = WriteToSerialPort(port);

                    await Task.WhenAll(readTask, writeTask);
                }
                catch (TimeoutException ex)
                {
                    var message = ex.Message;
                    _Logger.Error("Open port: TimeoutException");
                }
                catch (UnauthorizedAccessException ex)
                {
                    _Logger.Error($"{ex.Message} ({ex.GetType().Name})");
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                }
            }
        }

        public void Stop()
        {
            _CancelOperation = true;
        }


        public void RunCommand(string command)
        {
            _Command = command;
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
                ReadTimeout = 10000
            };

            return port;
        }

        private async Task ReadFromSerialPort(SerialPort serialPort)
        {
            while (!_CancelOperation)
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        var message = await serialPort.ReadLineAsync();
                        
                        _PreviousState = _State;
                        //_State = System.Text.Encoding.UTF8.GetString( message);
                        _State = message.Trim();
                        _Logger.Debug($"State changed: {_PreviousState} -> {_State}");  
                        _ProcessDataMethod(message);
                    }                    
                }
                catch (Exception ex)
                {
                    _Logger.Error($"{ex.Message}");
                }
            }
        }

        private async Task WriteToSerialPort(SerialPort serialPort)
        {
            while (!_CancelOperation)
            {
                if (serialPort.IsOpen)
                {
                    try
                    {
                        if (_Command != string.Empty)
                        {
                            await serialPort.WriteLineAsync(_Command);
                            _Logger.Debug($"Written to serial port: {_Command}");
                            _PreviousCommand = _Command;
                            _Command = string.Empty;
                        }
                        await Task.Delay(_WriteDelay);
                    }
                    catch (Exception ex)
                    {
                        _Logger.Error(ex.Message, ex);
                    }
                }
            }

        }
    }
}
