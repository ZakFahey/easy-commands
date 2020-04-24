using EasyCommands;
using EasyCommands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Commands
{
    [Command("default")]
    [CommandDocumentation("Show how to use a default sub command.")]
    class DefaultExampleCommands : CommandCallbacks<User>
    {
        // This command will be executed whenever a non-existant command is called,
        // you can name it "" (empty string) if you want to create an "invisible" sub command
        [SubCommand(isDefault: true, "hello")]
        [CommandDocumentation("Say hello.")]
        public void Hello([AllowSpaces] string name)
        {
            Console.WriteLine($"Hello {name}!");
        }

        [SubCommand("bye")] // If nothing is passed to isDefault, it's false
        [CommandDocumentation("Say bye.")]
        public void Bye([AllowSpaces] string name)
        {
            Console.WriteLine($"Bye {name}!");
        }
    }
}
