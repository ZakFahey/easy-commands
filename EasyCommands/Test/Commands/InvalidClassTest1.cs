using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class InvalidClassTest1 : CommandCallbacks
    {
        // Registration should fail because the class doesn't inherit from CommandCallbacks
        [Command("test")]
        public class TestCommand
        {
            [SubCommand("test")]
            public void Test(User sender)
            {
                Console.WriteLine("Something");
            }
        }
    }
}
