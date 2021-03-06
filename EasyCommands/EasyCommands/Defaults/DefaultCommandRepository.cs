﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyCommands.Commands;

namespace EasyCommands.Defaults
{
    /// <summary>
    /// Stores and indexes all commands
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public class DefaultCommandRepository<TSender> : CommandRepository<TSender>
    {
        private Dictionary<string, CommandDelegate<TSender>> commands = new Dictionary<string, CommandDelegate<TSender>>();

        public DefaultCommandRepository(Context<TSender> context) : base(context) { }
        
        public override async Task Invoke(TSender sender, string command)
        {
            if(string.IsNullOrEmpty(command))
            {
                throw new CommandParsingException(Context.TextOptions.EmptyCommand);
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
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException(string.Format(Context.TextOptions.CommandNotFound, name));
            }
            await commands[name].Invoke(sender, parameters);
        }

        protected override void AddCommand(CommandDelegate<TSender> command, string[] names)
        {
            foreach(string name in names)
            {
                if(commands.ContainsKey(name))
                {
                    throw new CommandRegistrationException($"Failed to register command \"{name}\" because it is a duplicate.");
                }
                if(!Regex.IsMatch(name, Context.TextOptions.CommandNameValidationRegex))
                {
                    throw new CommandRegistrationException(
                        string.Format(Context.TextOptions.InvalidCommandNameErrorMessage, name));
                }
                commands[name] = command;
            }
            commandList.Add(command);
        }

        public CommandDelegate<TSender> GetDelegate(string command, string subcommand = null)
        {
            if(!commands.ContainsKey(command))
            {
                return null;
            }
            var cmdDelegate = commands[command];
            if(subcommand == null)
            {
                return cmdDelegate;
            }
            if(cmdDelegate is CommandGroupDelegate<TSender> groupDelegate)
            {
                return groupDelegate.GetSubcommandDelegate(subcommand);
            }
            return null;
        }
    }
}
