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
        [SubCommand("hello")]
        [CommandDocumentation("Say hello.")]
        public void Hello([AllowSpaces] string name)
        {
            Console.WriteLine($"Hello {name}!");
        }

        [SubCommand("bye")]
        [CommandDocumentation("Say bye.")]
        public void Bye([AllowSpaces] string name)
        {
            Console.WriteLine($"Bye {name}!");
        }

        // It is executed whenever an invalid request is sent, for example: "default Eveldee" will execute the default subcommand with args "Eveldee"
        [SubCommand(SubCommandType.Default)]
        [CommandDocumentation("This is the default subcommand.")]
        public void IAmDefault(string name)
        {
            Console.WriteLine($"Have a nice day {name}!");
        }
    }
}
