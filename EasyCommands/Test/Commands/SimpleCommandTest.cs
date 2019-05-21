using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should succeed with this
    class SimpleCommandTest : CommandCallbacks<User>
    {
        [Command("test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }

        [Command("something")]
        public void Something()
        {
            Console.WriteLine("Hello!");
        }
    }
}
