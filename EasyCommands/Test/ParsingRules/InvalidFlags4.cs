using Example;

namespace EasyCommands.Test.ParsingRules
{
    [FlagsArgument]
    class InvalidFlags4
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        // Registration should fail here because of the duplicate argument name
        [FlagParams("-a")]
        public int B = 0;
    }
}
