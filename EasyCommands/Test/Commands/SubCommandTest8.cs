using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with these commands using isDefault for 1 SubCommand
    [Command("do")]
    class SubcommandTest8 : CommandCallbacks<User>
    {
        [SubCommand(isDefault: true, "test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}
