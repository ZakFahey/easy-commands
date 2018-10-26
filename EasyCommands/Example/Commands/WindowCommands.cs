using System;
using EasyCommands;

namespace Example.Commands
{
    [Command("window")]
    [CommandDocumentation("Manipulates the console window.")]
    class WindowCommands
    {
        [SubCommand("resize")]
        [SubCommandDocumentation("Resizes the window.")]
        void Resize(User sender, int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.WriteLine($"Window dimensions set to {width} x {height}.");
        }

        [SubCommand("move")]
        [SubCommandDocumentation("Moves the window to a certain position.")]
        void Move(User sender, int left, int top)
        {
            Console.SetWindowPosition(left, top);
            Console.WriteLine($"Window position set to ({left}, {top}).");
        }
    }
}
