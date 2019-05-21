using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class DuplicateTest0 : CommandCallbacks<User>
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

        // Registration should fail here because of the duplicate command
        [Command("test")]
        public void Test2()
        {
            Console.WriteLine("Something else");
        }
    }
}
