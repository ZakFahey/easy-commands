using System;
using EasyCommands.Commands;

namespace Example.Commands
{
    class FlagCommand : CommandCallbacks<User>
    {
        //[Command("flag-test")]
        [CommandDocumentation("Demonstrates the flags argument.")]
        public void FlagTest(ExampleFlags flags)
        {
            Console.WriteLine($"A: {flags.A}");
            Console.WriteLine($"B: {flags.B}");
            Console.WriteLine($"C: {flags.C?.Name}");
        }
    }
}
