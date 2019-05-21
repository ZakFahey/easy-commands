namespace EasyCommands.Test.ParsingRules
{
    // Registration should fail here because this class doesn't have the [FlagsArgument] attribute
    class InvalidFlags0
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        [FlagParams("-b")]
        public int B = 0;
    }
}
