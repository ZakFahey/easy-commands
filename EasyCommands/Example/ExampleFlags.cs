using System;
using EasyCommands;

namespace Example
{
    [FlagsArgument]
    public class ExampleFlags
    {
        [FlagParams("-a")]
        public string A = "DEFAULT";
        [FlagParams("-b")]
        public int B = 0;
        [FlagParams("-c", "-C")]
        public User C = null;
    }
}
