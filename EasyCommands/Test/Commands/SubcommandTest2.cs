using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest2 : CommandCallbacks
    {
        // Registration should succeed with this nested class
        [Command("do")]
        public class DoCommand : CommandCallbacks
        {
            [SubCommand("test")]
            public void Test(User sender)
            {
                Console.WriteLine("Something");
            }

            [SubCommand("something")]
            public void Something(User sender)
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
