using System;
using EasyCommands;
using Example;
using System.IO;

namespace EasyCommands.Test.Commands
{
    class CallbackSyntaxTest0 : CommandCallbacks
    {
        // Registration should fail because it uses a nonexistent parse rule
        [Command("test")]
        public void Test(User sender, DirectoryInfo directoryInfo)
        {
            Console.WriteLine("Hello!");
        }
    }
}
