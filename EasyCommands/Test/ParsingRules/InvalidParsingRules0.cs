using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands.Test.ParsingRules
{
    // This should fail because it doesn't inherit from the ParsingRules class
    class InvalidParsingRules0
    {
        [ParseRule]
        public byte ParseByte(string arg)
        {
            return 0;
        }
    }
}
