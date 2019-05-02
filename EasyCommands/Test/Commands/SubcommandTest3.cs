using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest3 : CommandCallbacks
    {
        // Registration should succeed with a combination of commands and subcommands
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

        [Command("test")]
        void Test(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
