using System.IO;

namespace EasyCommands.Test.ParsingRules
{
    [FlagsArgument]
    class InvalidFlags2
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        // Registration should fail here because the parameter includes a space
        [FlagParams("- b")]
        public int B = 0;
    }
}
