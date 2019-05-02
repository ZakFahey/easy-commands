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
        void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }
    }
}
