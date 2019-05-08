using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest6 : CommandCallbacks
    {
        // Registration should fail due to invalid command-subcommand structure
        [Command("do")]
        public class DoCommand : CommandCallbacks
        {
            [Command("test")]
            public class TestSubcommand : CommandCallbacks
            {
                [SubCommand("test")]
                public void Test(User sender)
                {
                    Console.WriteLine("Something");
                }
            }
        }
    }
}
