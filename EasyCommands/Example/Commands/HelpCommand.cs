using System;
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
                    var permLevel = cmd.GetCustomAttribute<AccessLevel>();
                    if(permLevel == null || Sender.PermissionLevel >= permLevel.MinimumLevel)
                    {
                        Console.WriteLine(cmd.SyntaxDocumentation());
                    }
                }
            }
            else
            {
                string commandName = subcommand == null ? command : $"{command} {subcommand}";
                var cmdDelegate = Context.CommandRepository.GetDelegate(command, subcommand);
                if(cmdDelegate == null)
                {
                    Fail(string.Format(Context.TextOptions.CommandNotFound, commandName));
                }
                // If the user doesn't have permission to run this command, don't show it
                var baseCmdDelegate = Context.CommandRepository.GetDelegate(command);
                AccessLevel permLevel = baseCmdDelegate.GetCustomAttribute<AccessLevel>();
                if(permLevel != null && permLevel.MinimumLevel > Sender.PermissionLevel)
                {
                    Fail(string.Format(Context.TextOptions.CommandNotFound, commandName));
                }
                CommandDocumentation documentation = cmdDelegate.GetCustomAttribute<CommandDocumentation>();
                string helpText = documentation == null ? "This command does not have any documentation." : documentation.Documentation;
                string aliases = string.Join(", ", cmdDelegate.Aliases);
                if(cmdDelegate is CommandGroupDelegate<User> groupDelegate)
                {
                    if(aliases.Length > 0)
                    {
                        aliases = " Aliases: " + aliases + ".";
                    }
                    Console.WriteLine($"{helpText}{aliases} Subcommands:\n{groupDelegate.SubcommandList()}");
                }
                else
                {
                    if(aliases.Length > 0)
                    {
                        aliases = ". Aliases: " + aliases;
                    }
                    Console.WriteLine($"{helpText} Syntax: {cmdDelegate.SyntaxDocumentation()}{aliases}");
                }
            }
        }
    }
}