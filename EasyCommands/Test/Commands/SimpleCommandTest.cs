using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with this
    class SimpleCommandTest : CommandCallbacks
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
    }
}
