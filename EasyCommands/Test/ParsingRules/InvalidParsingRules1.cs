using EasyCommands.Arguments;

namespace EasyCommands.Test.ParsingRules
{
    class InvalidParsingRules1<TSender> : ParsingRules<TSender>
    {
        // This should fail because the input parameter isn't a string
        [ParseRule]
        public byte ParseByte(byte arg)
        {
            return 0;
        }
    }
}
