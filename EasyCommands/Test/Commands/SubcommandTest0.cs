using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    [Command("do")]
    class SubcommandTest0 : CommandCallbacks
    {
        [SubCommand("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        public void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }

        // Registration should fail here because of the duplicate command
        [SubCommand("test")]
        public void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
