using System;
using EasyCommands;

namespace Example.Commands
{
    [Command("window")]
    [CommandDocumentation("Manipulates the console window.")]
    class WindowCommands : CommandCallbacks
    {
        [SubCommand("resize")]
        [SubCommandDocumentation("Resizes the window.")]
        public void Resize(User sender, int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.WriteLine($"Window dimensions set to {width} x {height}.");
        }

        [SubCommand] // If we do not include the command name, it can be inferred from the method name.
        [SubCommandDocumentation("Moves the window to a certain position.")]
        public void Move(User sender, int left, int top)
        {
            Console.SetWindowPosition(left, top);
            Console.WriteLine($"Window position set to ({left}, {top}).");
        }
    }
}
