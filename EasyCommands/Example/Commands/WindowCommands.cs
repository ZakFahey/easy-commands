using System;
using EasyCommands;
using EasyCommands.Commands;

namespace Example.Commands
{
    [Command("window")]
    [CommandDocumentation("Manipulates the console window.")]
    public class WindowCommands : CommandCallbacks<User>
    {
        [SubCommand("resize")]
        [CommandDocumentation("Resizes the window.")]
        public void Resize(int width, int height)
        {
            var win = Window.GetConsoleWindow();
            Window.GetWindowRect(win, out Window.RECT rect);
            Window.MoveWindow(win, rect.Left, rect.Top, width, height, true);
            Console.WriteLine($"Window dimensions set to {width} x {height}.");
        }

        [SubCommand] // If we do not include the command name, it can be inferred from the method name.
        [CommandDocumentation("Moves the window to a certain position.")]
        public void Move(int left, int top)
        {
            var win = Window.GetConsoleWindow();
            Window.GetWindowRect(win, out Window.RECT rect);
            Window.MoveWindow(win, left, top, rect.Right - rect.Left, rect.Bottom - rect.Top, true);
            Console.WriteLine($"Window position set to ({left}, {top}).");
        }
    }
}
