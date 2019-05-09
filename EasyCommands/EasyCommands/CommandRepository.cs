using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EasyCommands
{
    public class CommandRepository<TSender>
    {
        private Dictionary<string, CommandDelegate<TSender>> commands = new Dictionary<string, CommandDelegate<TSender>>();
        private List<CommandDelegate<TSender>> commandList = new List<CommandDelegate<TSender>>();
        private Context<TSender> Context;

        public ReadOnlyCollection<CommandDelegate<TSender>> CommandList
        {
            get => commandList.AsReadOnly();
        }

        public CommandRepository(Context<TSender> context)
        {
            Context = context;
        }

        public void RegisterCommand(string[] names, MethodInfo command)
        {
            var newCommand = new BaseCommandDelegate<TSender>(Context, names[0], command);
            AddCommand(newCommand, names);
        }

        public void RegisterCommandWithSubcommands(string[] names, Type command)
        {
            if(command.BaseType != typeof(CommandCallbacks<TSender>))
            {
                throw new CommandRegistrationException($"{command.Name} must have the base class CommandCallbacks.");
            }
            if(command.GetNestedTypes().Any(t => command.GetCommandNames<Command>() != null))
            {
                throw new CommandRegistrationException($"Unexpected nested subcommand in {command.Name}.");
            }

            CommandGroupDelegate<TSender> newCommand = new CommandGroupDelegate<TSender>(Context, names[0]);
            bool anySubcommands = false;

            foreach(MethodInfo subcommand in command.GetMethods())
            {
                if(subcommand.GetCommandNames<Command>() != null)
                {
                    throw new CommandRegistrationException($"Unexpected Command attribute in {command.Name}.{subcommand.Name}.");
                }
                string[] subcommandNames = subcommand.GetCommandNames<SubCommand>();
                if(subcommandNames != null)
                {
                    anySubcommands = true;
                    newCommand.AddSubcommand(subcommand, subcommandNames);
                }
            }
            
            if(!anySubcommands)
            {
                throw new CommandRegistrationException($"{command.Name} must contain at least one subcommand.");
            }

            AddCommand(newCommand, names);
        }

        public void Invoke(TSender sender, string command)
        {
            if(command == null)
            {
                throw new ArgumentNullException();
            }
            command = command.Trim(' ');
            int firstSpace = command.IndexOf(' ');
            (string name, string parameters) = command.SplitAfterFirstSpace();
            if(firstSpace == -1)
            {
                name = command;
                parameters = "";
            }
            else
            {
                name = command.Substring(0, firstSpace);
                parameters = command.Substring(firstSpace + 1);
            }
            if(string.IsNullOrEmpty(name))
            {
                throw new CommandParsingException(string.Format(Context.TextOptions.EmptyCommand, name));
            }
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException(string.Format(Context.TextOptions.CommandNotFound, name));
            }
            commands[name].Invoke(sender, parameters);
        }

        private void AddCommand(CommandDelegate<TSender> command, string[] names)
        {
            foreach(string name in names)
            {
                if(commands.ContainsKey(name))
                {
                    throw new CommandRegistrationException($"Failed to register command \"{name}\" because it is a duplicate.");
                }
                if(!Regex.IsMatch(name, @"^[a-z0-9\-]*$"))
                {
                    throw new CommandRegistrationException($"Failed to register command \"{name}\". " +
                        $"Commands may only contain lowercase letters, numbers, and the dash symbol.");
                }
                commands[name] = command;
            }
            commandList.Add(command);
        }
    }
}
