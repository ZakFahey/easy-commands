using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    class CommandRepository<TSender>
    {
        private Dictionary<string, CommandDelegate<TSender>> commands = new Dictionary<string, CommandDelegate<TSender>>();
        private TextOptions textOptions;
        protected ArgumentParser parser;

        public CommandRepository(TextOptions options, ArgumentParser parser)
        {
            textOptions = options;
            this.parser = parser;
        }

        public void RegisterCommand(string[] names, MethodInfo command)
        {
            AddCommand(new BaseCommandDelegate<TSender>(textOptions, parser, names[0], command), names);
        }

        public void RegisterCommandWithSubcommands(string[] names, Type command)
        {
            if(command.BaseType != typeof(CommandCallbacks))
            {
                throw new CommandRegistrationException($"{command.Name} must have the base class CommandCallbacks.");
            }
            if(command.GetNestedTypes().Any(t => command.GetCommandNames<Command>() != null))
            {
                throw new CommandRegistrationException($"Unexpected nested subcommand in {command.Name}.");
            }

            var newCommand = new CommandGroupDelegate<TSender>(textOptions, parser, names[0]);

            foreach(MethodInfo subcommand in command.GetMethods())
            {
                if(subcommand.GetCommandNames<Command>() != null)
                {
                    throw new CommandRegistrationException($"Unexpected Command attribute in {command.Name}.{subcommand.Name}.");
                }
                string[] subcommandNames = command.GetCommandNames<SubCommand>();
                if(subcommandNames != null)
                {
                    newCommand.AddSubcommand(subcommand, subcommandNames);
                }
            }

            AddCommand(newCommand, names);
        }

        public void Invoke(TSender sender, string name, IEnumerable<string> parameters)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new CommandParsingException(string.Format(textOptions.EmptyCommand, name));
            }
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException(string.Format(textOptions.CommandNotFound, name));
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
                commands[name] = command;
            }
        }
    }
}
