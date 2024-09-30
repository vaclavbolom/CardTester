using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{

    public delegate void ProcessDataDelegate(string message);
    /// <summary>
    /// Sending and receiving commands to/from serial port.
    /// </summary>
    public interface ISerialPortOperator
	{
		/// <summary>
		/// Run loop for reading/writting from/to serial port
		/// </summary>
		/// <returns></returns>
		Task Run(ProcessDataDelegate processData);
		
		/// <summary>
		/// Registers method for processing of state update
		/// </summary>
		void AddProcessDataMethod(ProcessDataDelegate processData);

		/// <summary>
		/// Stops loop
		/// </summary>
		void Stop();

		/// <summary>
		/// Send command to serial port
		/// </summary>
		Task RunCommandAsync(string command);

		/// <summary>
		/// Send comand to serial port
		/// </summary>
		/// <param name="command"></param>
		void RunCommand(string command);


		/// <summary>
		/// Gets the last state received from serial port
		/// </summary>
		/// <returns></returns>
		string GetState();

		/// <summary>
		/// Register method for processing of changed state
		/// </summary>
		/// <param name="method"></param>
		public void RegisterStateChangeMethod(ProcessDataDelegate method);


		bool IsDeviceConnected { get; }
    }
}
