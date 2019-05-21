using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class FlagsTest2 : CommandCallbacks<User>
    {
        // Registration should fail because there is a flags parameter with an optional parameter 
        [Command("test")]
        public void Test(ExampleFlags flags, int optional = 0)
        {
            Console.WriteLine(flags.A);
        }
    }
}
