﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands.Commands
{
    /// <summary>
    /// Command delegate for command with sub-commands
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public class CommandGroupDelegate<TSender> : CommandDelegate<TSender>
    {
        private Dictionary<string, BaseCommandDelegate<TSender>> subcommands = new Dictionary<string, BaseCommandDelegate<TSender>>();
        private List<BaseCommandDelegate<TSender>> subcommandList = new List<BaseCommandDelegate<TSender>>();
        private BaseCommandDelegate<TSender> defaultCommand = null;

        public CommandGroupDelegate(Context<TSender> context, string mainName, string[] allNames, Type command) : base(context, mainName, allNames)
        {
            foreach(CustomAttribute attribute in command.GetCustomAttributes<CustomAttribute>(true))
            {
                customAttributes[attribute.GetType()] = attribute;
            }

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
                    var newSubcommand = new BaseCommandDelegate<TSender>(Context, $"{Name} {subcommandNames[0]}", subcommandNames, subcommandNames[0], subcommand);
                    AddSubcommand(newSubcommand, subcommandNames);

                    // Check if it's the default command
                    if (subcommand.GetSubCommandIsDefault())
                    {
                        if (defaultCommand != null)
                        {
                            throw new CommandRegistrationException($"There is two or more default sub commands in {command.Name}.");
                        }
                        defaultCommand = newSubcommand;
                    }
                }
            }
            if(!anySubcommands)
            {
                throw new CommandRegistrationException($"{command.Name} must contain at least one subcommand.");
            }
        }

        public override string SyntaxDocumentation()
        {
            return $"{Context.TextOptions.CommandPrefix}{Name} <{string.Join("|", subcommandList.Select(sub => sub.ShortName))}>";
        }

        public override void Invoke(TSender sender, string args)
        {
            if(args.Length == 0)
            {
                // Check for default command
                if (defaultCommand != null)
                {
                    Context.CommandHandler.PreCheck(sender, this);
                    defaultCommand.Invoke(sender, "");
                }
                else
                {
                    throw new CommandParsingException($"{string.Format(Context.TextOptions.ShowSubcommands, Name)}\n{SubcommandList()}");
                }
            }
            else
            {
                (string subcommand, string subcommandArgs) = args.SplitAfterFirstSpace();
                if(!subcommands.ContainsKey(subcommand))
                {
                    // Check for default command
                    if (defaultCommand != null)
                    {
                        Context.CommandHandler.PreCheck(sender, this);
                        defaultCommand.Invoke(sender, args);
                    }
                    else
                    {
                        throw new CommandParsingException(
                            string.Format(Context.TextOptions.CommandNotFound, $"{Name} {subcommand}") + "\n"
                            + string.Format(Context.TextOptions.ShowSubcommands, Name) + "\n"
                            + SubcommandList());
                    }
                }
                else
                {
                    Context.CommandHandler.PreCheck(sender, this);
                    subcommands[subcommand].Invoke(sender, subcommandArgs);
                }
            }
        }

        public string SubcommandList()
        {
            return string.Join("\n", subcommandList.Select(sub => sub.SyntaxDocumentation()));
        }

        public BaseCommandDelegate<TSender> GetSubcommandDelegate(string subcommand)
        {
            if(!subcommands.ContainsKey(subcommand))
            {
                return null;
            }
            return subcommands[subcommand];
        }

        private void AddSubcommand(BaseCommandDelegate<TSender> command, string[] names)
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
    }
}
