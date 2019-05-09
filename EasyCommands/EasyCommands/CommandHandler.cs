using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;

namespace EasyCommands
{
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

        private TextOptions textOptions;
        private CommandRepository<TSender> commandRepository;
        private ArgumentParser<TSender> argumentParser = new ArgumentParser<TSender>();

        protected abstract void SendFailMessage(TSender sender, string message);
        protected abstract void Initialize();
        protected virtual void BeforeAll() { }
        protected virtual void AfterAll() { }

        public ReadOnlyCollection<CommandDelegate<TSender>> CommandList
        {
            get => commandRepository.CommandList;
        }

        public CommandHandler()
        {
            textOptions = TextOptions.Default();
            commandRepository = new CommandRepository<TSender>(textOptions, argumentParser);
            Initialize();
        }

        public CommandHandler(TextOptions options)
        {
            textOptions = options;
            commandRepository = new CommandRepository<TSender>(textOptions, argumentParser);
            Initialize();
        }

        public void AddParsingRules(Type rules)
        {
            argumentParser.AddParsingRules(rules);
        }

        public void RegisterCommands(Type classToRegister)
        {
            // Register the base class as a command with subcommands if it is one
            string[] classCommandNames = classToRegister.GetCommandNames<Command>();
            if(classCommandNames != null)
            {
                commandRepository.RegisterCommandWithSubcommands(classCommandNames, classToRegister);
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
                    commandRepository.RegisterCommand(commandNames, command);
                }
            }

            // Register any commands with subcommands
            foreach(Type command in classToRegister.GetNestedTypes())
            {
                string[] commandNames = command.GetCommandNames<Command>();
                if(commandNames != null)
                {
                    commandRepository.RegisterCommandWithSubcommands(commandNames, command);
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
                commandRepository.Invoke(sender, command);
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
