using System;
using EasyCommands;
using Example;
using System.IO;

namespace EasyCommands.Test.Commands
{
    class CallbackSyntaxTest2 : CommandCallbacks
    {
        // Registration should fail because the command has the wrong return value
        [Command("test")]
        public int Test(User sender, int num1)
        {
            Console.WriteLine("Hello!");
            Console.WriteLine(num1);
            return num1;
        }
    }
}
