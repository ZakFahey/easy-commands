using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands.Test.ParsingRules
{
    public class InvalidParsingRules1 : EasyCommands.ParsingRules
    {
        // This should fail because the input parameter isn't a string
        [ParseRule]
        byte ParseByte(byte arg)
        {
            return 0;
        }
    }
}
