using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    [Command("do")]
    class SubcommandTest0 : CommandCallbacks<User>
    {
        [SubCommand("test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }

        // Registration should fail here because of the duplicate command
        [SubCommand("test")]
        public void Test2()
        {
            Console.WriteLine("Something else");
        }
    }
}
