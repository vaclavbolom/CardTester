using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTester
{
	/// <summary>
	/// Sending and receiving commands to/from serial port.
	/// </summary>
	public interface ISerialPortOperator
	{
		/// <summary>
		/// Run loop for reading/writting from/to serial port
		/// </summary>
		/// <returns></returns>
		Task<int> Run();

		/// <summary>
		/// Stops loop
		/// </summary>
		void Stop();

		/// <summary>
		/// Send command to serial port
		/// </summary>
		void RunCommand(string command);


		/// <summary>
		/// Gets the last state received from serial port
		/// </summary>
		/// <returns></returns>
		string ReadState();

		
	}
}
