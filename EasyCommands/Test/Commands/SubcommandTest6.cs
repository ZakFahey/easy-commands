using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest6 : CommandCallbacks<User>
    {
        // Registration should fail due to invalid command-subcommand structure
        [Command("do")]
        public class DoCommand : CommandCallbacks<User>
        {
            [Command("test")]
            public class TestSubcommand : CommandCallbacks<User>
            {
                [SubCommand("test")]
                public void Test()
                {
                    Console.WriteLine("Something");
                }
            }
        }
    }
}
