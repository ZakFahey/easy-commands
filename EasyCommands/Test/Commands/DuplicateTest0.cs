using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class DuplicateTest0 : CommandCallbacks<User>
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

        // Registration should fail here because of the duplicate command
        [Command("test")]
        public void Test2(User sender)
        {
            Console.WriteLine("Something else");
        }
    }
}
