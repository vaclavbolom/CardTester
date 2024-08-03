using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace CardTesterLibrary
{
    public class SerialPortOperator : ISerialPortOperator
    {

        

        private readonly ILogger _Logger;
        private readonly int _WriteDelay = 1;

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
                    throw;
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
            _Logger.Debug($"Command set: {command}");
        }

        public string GetState()
        {
            _Logger.Debug($"Get state: {_State}");
            return _State;
        }

        private SerialPort CreatePort()
        {
            var port = new SerialPort
            {
                PortName = _PortName,
                BaudRate = 9600
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
                        if (_ProcessDataMethod != null)
                            Task.Run(() => _ProcessDataMethod(message));
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
                            _Logger.Debug($"Try to write command: {_Command}");
                            await serialPort.WriteLineAsync(_Command);
                            _Logger.Debug($"Written to serial port: {_Command}");
                            _PreviousCommand = _Command;
                            _Command = string.Empty;
                        }
                        else
                            await Task.Delay(_WriteDelay);
                    }
                    catch (Exception ex)
                    {
                        _Logger.Error(ex.Message, ex);
                    }
                }
                else
                {
                    _Logger.Warning("Port is NOT opened.");
                }
            }

        }
    }
}
