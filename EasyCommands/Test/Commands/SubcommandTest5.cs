using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest5 : CommandCallbacks
    {
        // Registration should fail due to invalid command-subcommand structure
        [SubCommand("test")]
        void Test(User sender)
        {
            Console.WriteLine("Something");
        }
    }
}
