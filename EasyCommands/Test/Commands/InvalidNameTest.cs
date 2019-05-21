using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class InvalidNameTest : CommandCallbacks<User>
    {
        // Registration should fail with this because the name is invalid
        [Command("test test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }
    }
}
