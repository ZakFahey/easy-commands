using EasyCommands;
using EasyCommands.Commands;
using System;
using System.Threading.Tasks;

namespace Example.Commands
{
    class DelayCommands : CommandCallbacks<User>
    {
        [Command("delay")]
        [CommandDocumentation("Show a message after the specified number of seconds.")]
        public async Task Delay(int delay)
        {
            if (delay < 0)
            {
                Fail("delay cannot be negative.");
            }
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Console.WriteLine($"A message appeared after {delay} second(s)!");
        }

        [Command("delay-error")]
        [CommandDocumentation("Show a message after the specified number of seconds.")]
        public async Task DelayWithError(int delay)
        {
            if (delay < 0)
            {
                Fail("delay cannot be negative.");
            }
            await Task.Delay(TimeSpan.FromSeconds(delay));
            Fail($"The command has failed after {delay} second(s).");
        }
    }
}
