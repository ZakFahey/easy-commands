using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest4 : CommandCallbacks
    {
        // Registration should fail due to invalid command-subcommand structure
        [Command("do")]
        class DoCommand
        {
            [Command("test")]
            void Test(User sender)
            {
                Console.WriteLine("Something");
            }
        }
    }
}
