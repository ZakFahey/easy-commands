using System.IO;

namespace EasyCommands.Test.ParsingRules
{
    [FlagsArgument]
    class InvalidFlags1
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        [FlagParams("-b")]
        public int B = 0;
        // Registration should fail here because there is no parse rule for Stream
        [FlagParams("-c")]
        public Stream C = null;
    }
}
