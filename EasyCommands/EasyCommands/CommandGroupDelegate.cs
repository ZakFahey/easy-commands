using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command with sub-commands
    /// </summary>
    class CommandGroupDelegate<TSender> : CommandDelegate<TSender>
    {
        private Dictionary<string, BaseCommandDelegate<TSender>> subcommands = new Dictionary<string, BaseCommandDelegate<TSender>>();

        public CommandGroupDelegate(TextOptions options, ArgumentParser parser, string name) : base(options, parser, name) { }

        public override void Invoke(TSender sender, string args)
        {
            if(args.Length == 0)
            {
                //TODO: proper list
                throw new CommandParsingException($"{string.Format(textOptions.ShowSubcommands, Name)}\n{SubcommandList()}");
            }
            (string subcommand, string subcommandArgs) = args.SplitAfterFirstSpace();
            if(!subcommands.ContainsKey(subcommand))
            {
                throw new CommandParsingException(
                    string.Format(textOptions.CommandNotFound, $"{Name} {subcommand}") + "\n"
                    + string.Format(textOptions.ShowSubcommands, Name) + "\n"
                    + SubcommandList());
            }
            subcommands[subcommand].Invoke(sender, subcommandArgs);
        }

        public void AddSubcommand(BaseCommandDelegate<TSender> command, string[] names)
        {
            foreach(string name in names)
            {
                if(subcommands.ContainsKey(name))
                {
                    throw new CommandRegistrationException($"Failed to register command \"{Name} {name}\" because it is a duplicate.");
                }
                subcommands[name] = command;
            }
        }

        public void AddSubcommand(MethodInfo command, string[] names)
        {
            AddSubcommand(new BaseCommandDelegate<TSender>(textOptions, parser, $"{Name} {names[0]}", command), names);
        }

        public string SubcommandList()
        {
            return string.Join("\n", subcommands.Values.Select(sub => sub.SyntaxDocumentation));
        }
    }
}
