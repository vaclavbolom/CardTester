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
        private readonly int _WriteDelay = 10;

        private bool _CancelOperation = false;

        private string _PortName = string.Empty;

        private string _Command = string.Empty;
        private string _PreviousCommand = string.Empty;
        private string _State = string.Empty;
        private string _PreviousState = string.Empty;

        private ProcessDataDelegate? _ProcessDataMethod;

        public bool IsDeviceConnected { get; private set; } = false;

        public int MyProperty { get; set; }

        public SerialPortOperator(string portName, ILogger logger)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PortName = portName;
        }



        public async Task Run(ProcessDataDelegate processDataMethod)
        {
            AddProcessDataMethod(processDataMethod);
            using (var port = CreatePort())
            {
                try
                {
                    port.Open();
                    var readTask = ReadFromSerialPort(port);
                    var writeTask = WriteToSerialPort(port);
                    IsDeviceConnected = true;

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
                    throw new SerialPortOperatorException("Unauthorized access - serial port is probably being used by another application");
                }
                catch (FileNotFoundException ex)
                {
                    var message = $"Cannot connect to serial port: {ex.Message}";
                    _Logger.Error(message);
                    throw new SerialPortOperatorException(message);
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex.Message, ex);
                }
            }
        }

        public void AddProcessDataMethod(ProcessDataDelegate processDataMethod)
        {
            _ProcessDataMethod += processDataMethod ?? throw new ArgumentNullException(nameof(processDataMethod));
        }

        public void RegisterStateChangeMethod(ProcessDataDelegate method) 
            => _ProcessDataMethod += method;

        public void Stop()
        {
            _CancelOperation = true;
        }


        public void RunCommand(string command)
        {
            _Command = command;
            _Logger.Debug($"Command set: {command}");
        }
        public async Task RunCommandAsync(string command)
        {
            _Command = command;            
            await Task.Delay(1);
            _Logger.Debug($"Run Command Async: {command}");
        }

        public string GetState()
        {
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
                        //var message = await serialPort.ReadLineAsync();
                        var message = await serialPort.ReadAsync(3);

                        _PreviousState = _State;
                        _State = System.Text.Encoding.UTF8.GetString(message);
                        _State = _State.Trim();
                        _Logger.Debug($"State changed: {_PreviousState} -> {_State}");
                        if (_ProcessDataMethod != null)
                            Task.Run(() => _ProcessDataMethod(_State));
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
