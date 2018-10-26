using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    [Command("do")]
    class DuplicateTest3
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

        [SubCommand("test")]
        void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
