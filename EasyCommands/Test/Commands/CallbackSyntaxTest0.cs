using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;
using System.IO;

namespace EasyCommands.Test.Commands
{
    class CallbackSyntaxTest0 : CommandCallbacks<User>
    {
        // Registration should fail because it uses a nonexistent parse rule
        [Command("test")]
        public void Test(DirectoryInfo directoryInfo)
        {
            Console.WriteLine("Hello!");
        }
    }
}
