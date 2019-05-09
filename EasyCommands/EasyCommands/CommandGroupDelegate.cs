using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command with sub-commands
    /// </summary>
    public class CommandGroupDelegate<TSender> : CommandDelegate<TSender>
    {
        private Dictionary<string, BaseCommandDelegate<TSender>> subcommands = new Dictionary<string, BaseCommandDelegate<TSender>>();
        private List<BaseCommandDelegate<TSender>> subcommandList = new List<BaseCommandDelegate<TSender>>();

        public CommandGroupDelegate(Context<TSender> context, string name) : base(context, name) { }

        public override string SyntaxDocumentation()
        {
            return $"{Name} <{string.Join("|", subcommandList.Select(sub => sub.Name))}>";
        }

        public override void Invoke(TSender sender, string args)
        {
            if(args.Length == 0)
            {
                throw new CommandParsingException($"{string.Format(Context.TextOptions.ShowSubcommands, Name)}\n{SubcommandList()}");
            }
            (string subcommand, string subcommandArgs) = args.SplitAfterFirstSpace();
            if(!subcommands.ContainsKey(subcommand))
            {
                throw new CommandParsingException(
                    string.Format(Context.TextOptions.CommandNotFound, $"{Name} {subcommand}") + "\n"
                    + string.Format(Context.TextOptions.ShowSubcommands, Name) + "\n"
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
            subcommandList.Add(command);
        }

        public void AddSubcommand(MethodInfo command, string[] names)
        {
            var newSubcommand = new BaseCommandDelegate<TSender>(Context, $"{Name} {names[0]}", command);
            AddSubcommand(newSubcommand, names);
        }

        public string SubcommandList()
        {
            return string.Join("\n", subcommandList.Select(sub => sub.SyntaxDocumentation()));
        }
    }
}
