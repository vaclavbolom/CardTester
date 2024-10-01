using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public enum CodingResult
    {
        CODING_RESULT_SYSTEM_ERROR = -2,    //coding result was bad, reject chip. Meaning: This process is not operational (any more). Stop using this process.
        CODING_RESULT_REJECT = -1,    //coding result was bad, reject chip.
        CODING_RESULT_UNINITIALIZED = 0,     //default value
        CODING_RESULT_OK= 1
    }
}
