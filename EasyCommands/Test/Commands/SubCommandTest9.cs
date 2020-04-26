using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should fail with two default subcommands
    [Command("do")]
    class SubcommandTest9 : CommandCallbacks<User>
    {
        [SubCommand(SubCommandType.Default)]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand(SubCommandType.Default)]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}