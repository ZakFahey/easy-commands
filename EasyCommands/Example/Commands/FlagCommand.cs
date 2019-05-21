using System;
using EasyCommands;
using EasyCommands.Commands;

namespace Example.Commands
{
    class FlagCommand : CommandCallbacks<User>
    {
        [Command("flag-test")]
        [CommandDocumentation("Demonstrates the flags argument.")]
        public void FlagTest(string test, ExampleFlags flags)
        {
            string c = flags.C?.Name;

            Console.WriteLine($"test: {test}");
            Console.WriteLine($"A: {flags.A}");
            Console.WriteLine($"B: {flags.B}");
            Console.WriteLine($"C: {(c == null ? "null" : c)}");
            Console.WriteLine($"D: {flags.D}");
        }
    }
}
