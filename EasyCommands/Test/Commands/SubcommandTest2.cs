using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest2 : CommandCallbacks<User>
    {
        // Registration should succeed with this nested class
        [Command("do")]
        public class DoCommand : CommandCallbacks<User>
        {
            [SubCommand("test")]
            public void Test()
            {
                Console.WriteLine("Something");
            }

            [SubCommand("something")]
            public void Something()
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
