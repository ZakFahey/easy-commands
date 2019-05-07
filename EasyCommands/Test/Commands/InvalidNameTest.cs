using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class InvalidNameTest : CommandCallbacks
    {
        // Registration should fail with this because the name is invalid
        [Command("test test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }
    }
}
