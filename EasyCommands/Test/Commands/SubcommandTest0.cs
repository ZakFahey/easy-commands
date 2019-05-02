using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    [Command("do")]
    class SubcommandTest0 : CommandCallbacks
    {
        [SubCommand("test")]
        void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [SubCommand("something")]
        void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }

        // Registration should fail here because of the duplicate command
        [SubCommand("test")]
        void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
