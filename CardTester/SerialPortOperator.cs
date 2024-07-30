using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
   
      public async Task Run()
      {
        while (!_CancelOperation)
        {
          //open port
          //read state
          // write command if there is command to write
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

	}
}
