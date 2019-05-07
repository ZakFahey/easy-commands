using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest3 : CommandCallbacks
    {
        // Registration should succeed with a combination of commands and subcommands
        [Command("do")]
        class DoCommand : CommandCallbacks
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

        [Command("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
