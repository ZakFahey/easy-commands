using System;
using System.Linq;
using EasyCommands;

namespace Example.Commands
{
    class HelpCommand : CommandCallbacks<User>
    {
        [Command("help")]
        [CommandDocumentation("Gives information about commands.")]
        public void Help(string command = null, string subcommand = null)
        {
            if(command == null)
            {
                Console.WriteLine("Available commands:");
                foreach(var cmd in Context.CommandHandler.CommandList)
                {
                    Console.WriteLine(cmd.SyntaxDocumentation());
                }
            }
            else
            {
                var cmdDelegate = Context.CommandRepository.GetDelegate(command, subcommand);
                if(cmdDelegate == null)
                {
                    string commandName = subcommand == null ? command : $"{command} {subcommand}";
                    Fail(string.Format(Context.TextOptions.CommandNotFound, commandName));
                }
                CommandDocumentation documentation = cmdDelegate.GetCustomAttribute<CommandDocumentation>();
                string helpText = documentation == null ? "This command does not have any documentation." : documentation.Documentation;
                if(cmdDelegate is CommandGroupDelegate<User> groupDelegate)
                {
                    Console.WriteLine($"{helpText} Subcommands:\n{groupDelegate.SubcommandList()}");
                }
                else
                {
                    Console.WriteLine($"{helpText} Syntax: {cmdDelegate.SyntaxDocumentation()}");
                }
            }
        }
    }
}