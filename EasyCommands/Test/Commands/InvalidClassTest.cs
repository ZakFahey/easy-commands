using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    // Registration should fail because the class doesn't inherit from CommandCallbacks
    class InvalidClassTest
    {
        [Command("test")]
        public void Test(User sender)
        {
            Console.WriteLine("Something");
        }
    }
}
