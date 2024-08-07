using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public static class CardTesterConstants
    {
        public const string STATE_FORWARD = "f";
        public const string STATE_BACKWARD = "b";
        public const string STATE_UP = "u";
        public const string STATE_DOWN = "d";
        public const string STATE_STOPPED = "s";
        public const string STATE_UNKNOWN = "x";

        public const string COMMAND_MOVE_FORWARD = "1";
        public const string COMMAND_MOVE_BACKWARD = "2";
        public const string COMMAND_STOP = "0";
        public const string COMMAND_RESET = "3";
        public const string COMMAND_GET_STATE = "4";
    }
}
