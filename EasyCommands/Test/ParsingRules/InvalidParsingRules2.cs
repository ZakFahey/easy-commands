using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands.Test.ParsingRules
{
    class InvalidParsingRules2<TSender> : ParsingRules<TSender>
    {
        // This should fail because the argument in ParseRule isn't an attribute
        [ParseRule]
        public byte ParseByte(string arg, string attributeOverride)
        {
            return 0;
        }
    }
}
