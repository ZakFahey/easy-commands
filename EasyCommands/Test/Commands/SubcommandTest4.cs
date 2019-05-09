using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest4 : CommandCallbacks<User>
    {
        // Registration should fail due to invalid command-subcommand structure
        [Command("do")]
        public class DoCommand : CommandCallbacks<User>
        {
            [Command("test")]
            public void Test()
            {
                Console.WriteLine("Something");
            }
        }
    }
}
