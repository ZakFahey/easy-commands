using System;
using System.Linq;
using EasyCommands;

namespace Example.Commands
{
    class HelpCommand : CommandCallbacks<User>
    {
        [Command("help")]
        [CommandDocumentation("Gives information about commands.")]
        public void Help(User sender, string command = null, string subcommand = null)
        {
            if(command == null)
            {
                Console.WriteLine("Available commands:");
                foreach(var cmd in CommandHandler.CommandList)
                {
                    Console.WriteLine(cmd.SyntaxDocumentation());
                }
            }
            else
            {

            }
        }
    }
}
