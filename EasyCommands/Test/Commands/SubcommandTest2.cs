using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest2 : CommandCallbacks
    {
        // Registration should succeed with this nested class
        [Command("do")]
        class DoCommand
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
}
