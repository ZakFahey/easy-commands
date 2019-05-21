using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class FlagsTest1 : CommandCallbacks<User>
    {
        // Registration should fail because there is a flags parameter with a parameter with AllowSpaces 
        [Command("test")]
        public void Test([AllowSpaces]string test, ExampleFlags flags)
        {
            Console.WriteLine(flags.A);
        }
    }
}
