using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with these commands
    [Command("do")]
    class SubcommandTest1 : CommandCallbacks<User>
    {
        [SubCommand("test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}
