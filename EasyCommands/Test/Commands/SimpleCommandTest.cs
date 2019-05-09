using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with this
    class SimpleCommandTest : CommandCallbacks<User>
    {
        [Command("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }

        [Command("something")]
        public void Something(User sender)
        {
            Console.WriteLine("Hello!");
        }
    }
}
