using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with one default subcommand
    [Command("do")]
    class SubcommandTest8 : CommandCallbacks<User>
    {
        [SubCommand(SubCommandType.Default)]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand(SubCommandType.Standard)]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}
