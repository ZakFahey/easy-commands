using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest5 : CommandCallbacks<User>
    {
        // Registration should fail due to invalid command-subcommand structure
        [SubCommand("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }
    }
}
