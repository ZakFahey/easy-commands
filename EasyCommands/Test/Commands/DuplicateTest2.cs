using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class DuplicateTest2 : CommandCallbacks
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

        // Registration should succeed here because this is an override command
        [Command("test")]
        void Test2(User sender, int parameter)
        {
            Console.WriteLine("Something else");
            Console.WriteLine(parameter);
        }
    }
}
