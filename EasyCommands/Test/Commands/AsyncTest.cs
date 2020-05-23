using System;
using System.Threading.Tasks;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class AsyncTest : CommandCallbacks<User>
    {
        // Registration should succeed with an async return value
        [Command("test")]
        public async Task Test()
        {
            await Task.Delay(1000);
            Console.WriteLine("Message sent after 1 second!");
        }
    }
}
