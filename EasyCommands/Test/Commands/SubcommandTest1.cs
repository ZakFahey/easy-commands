using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with these commands
    [Command("do")]
    class SubcommandTest1 : CommandCallbacks
    {
        [SubCommand("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        public void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }
    }
}
