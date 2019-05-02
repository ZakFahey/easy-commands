using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class DuplicateTest1 : CommandCallbacks
    {
        [Command("test", "write-something")]
        void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [Command("something")]
        void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }

        // Registration should fail here because of the duplicate command
        [Command("write-something-else", "test")]
        void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
