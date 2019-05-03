using System;
using EasyCommands;
using Example;
using System.IO;

namespace EasyCommands.Test.Commands
{
    class CallbackSyntaxTest1 : CommandCallbacks
    {
        // Registration should fail because it doesn't include the sender parameter
        [Command("test")]
        public void Test(int num1)
        {
            Console.WriteLine("Hello!");
            Console.WriteLine(num1);
        }
    }
}
