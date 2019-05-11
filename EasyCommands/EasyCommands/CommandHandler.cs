using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using EasyCommands.Defaults;

namespace EasyCommands
{
    /// <summary>
    /// The base class to build off of to create a command parser for your application
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public abstract class CommandHandler<TSender>
    {
        private IEnumerable<Type> _allTypes = null;
        /// <summary> A list of all types in the assembly. </summary>
        private IEnumerable<Type> allTypes
        {
            get
            {
                if(_allTypes == null)
                {
                    _allTypes = AppDomain
                        .CurrentDomain
                        .GetAssemblies()
                        .SelectMany(t => t.GetTypes());
                }
                return _allTypes;
            }
        }

        protected abstract void SendFailMessage(TSender sender, string message);
        protected abstract void Initialize();
        protected Context<TSender> Context = new Context<TSender>();

        protected void Fail(string message)
        {
            throw new CommandExecutionException(message);
        }

        public ReadOnlyCollection<CommandDelegate<TSender>> CommandList
        {
            get => Context.CommandRepository.CommandList;
        }

        public CommandHandler()
        {
            Context.CommandHandler = this;
            Context.TextOptions = TextOptions.Default();
            Context.ArgumentParser = new ArgumentParser<TSender>(Context);
            Context.CommandRepository = new DefaultCommandRepository<TSender>(Context);
            Initialize();
        }

        /// <summary> Runs before all commands. Can be used to run additional checks on the command. </summary>
        public virtual void PreCheck(TSender sender, CommandDelegate<TSender> command) { }
        /// <summary> The command repository to use. </summary>
        protected virtual Type CommandRepositoryToUse() => typeof(DefaultCommandRepository<TSender>);

        private CommandRepository<TSender> GetRepository()
        {
            Type repositoryType = CommandRepositoryToUse();
            if(repositoryType.BaseType != typeof(CommandRepository<TSender>))
            {
                throw new ArgumentException($"The input in CommandRepositoryToUse must have the parent class CommandRepository<{typeof(TSender).Name}>.");
            }
            return (CommandRepository<TSender>)Activator.CreateInstance(repositoryType, Context);
        }

        public CommandHandler(TextOptions options)
        {
            Context.CommandHandler = this;
            Context.TextOptions = options;
            Context.ArgumentParser = new ArgumentParser<TSender>(Context);
            Context.CommandRepository = new DefaultCommandRepository<TSender>(Context);
            Initialize();
        }

        public void AddParsingRules(Type rules)
        {
            Context.ArgumentParser.AddParsingRules(rules);
        }

        public void RegisterCommands(Type classToRegister)
        {
            // Register the base class as a command with subcommands if it is one
            string[] classCommandNames = classToRegister.GetCommandNames<Command>();
            if(classCommandNames != null)
            {
                Context.CommandRepository.RegisterCommandWithSubcommands(classCommandNames, classToRegister);
                return;
            }

            // Enforce type
            if(classToRegister.BaseType != typeof(CommandCallbacks<TSender>))
            {
                throw new CommandRegistrationException($"{classToRegister.Name} must have the base class CommandCallbacks.");
            }

            // Register all commands in this class
            foreach(MethodInfo command in classToRegister.GetMethods())
            {
                if(command.GetCommandNames<SubCommand>() != null)
                {
                    throw new CommandRegistrationException($"Unexpected SubCommand attribute in {classToRegister.Name}.{command.Name}.");
                }
                string[] commandNames = command.GetCommandNames<Command>();
                if(commandNames != null)
                {
                    Context.CommandRepository.RegisterCommand(commandNames, command);
                }
            }

            // Register any commands with subcommands
            foreach(Type command in classToRegister.GetNestedTypes())
            {
                string[] commandNames = command.GetCommandNames<Command>();
                if(commandNames != null)
                {
                    Context.CommandRepository.RegisterCommandWithSubcommands(commandNames, command);
                }
            }
        }

        public void RegisterCommands(string namespaceToRegister)
        {
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CommandCallbacks<TSender>) && !t.IsNested);
            foreach(Type type in types)
            {
                RegisterCommands(type);
            }
        }

        public void RunCommand(TSender sender, string command)
        {
            command = command.Trim(' ');
            try
            {
                Context.CommandRepository.Invoke(sender, command);
            }
            catch(CommandParsingException e)
            {
                SendFailMessage(sender, e.Message);
            }
            catch(CommandExecutionException e)
            {
                SendFailMessage(sender, e.Message);
            }
            catch(Exception e)
            {
                SendFailMessage(sender, $"The command threw an error:\n{e}");
            }
        }
    }
}
