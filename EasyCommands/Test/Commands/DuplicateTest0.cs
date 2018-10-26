using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class DuplicateTest0
    {
        [Command("test")]
        void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [Command("something")]
        void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }

        [Command("test")]
        void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
