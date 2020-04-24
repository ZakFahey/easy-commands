using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should fail with these commands using isDefault for 2 SubCommand
    [Command("do")]
    class SubcommandTest10 : CommandCallbacks<User>
    {
        [SubCommand(isDefault: true, "test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand(isDefault: true, "something")]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}