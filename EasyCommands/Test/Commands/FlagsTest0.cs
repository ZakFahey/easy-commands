using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class FlagsTest0 : CommandCallbacks<User>
    {
        // Registration should fail because there are multiple flags parameters
        [Command("test")]
        public void Test(ExampleFlags flags, ExampleFlags flags2)
        {
            Console.WriteLine(flags.A);
        }
    }
}
