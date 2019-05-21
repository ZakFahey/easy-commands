using Example;

namespace EasyCommands.Test.ParsingRules
{
    [FlagsArgument]
    class InvalidFlags3
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        [FlagParams("-b")]
        public int B = 0;
        // Registration should fail here because using flags within flags is invaild
        [FlagParams("-c")]
        public ExampleFlags C = null;
    }
}
