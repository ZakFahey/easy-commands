using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        private ArgumentParser argumentParser = new ArgumentParser();

        protected abstract void SendFailMessage(TSender sender, string message);
        protected abstract void Initialize();

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

        public void RegisterCustomAttribute(Type classToRegister)
        {
            //TODO
        }

        public void RegisterCustomAttributes(string namespaceToRegister)
        {
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CustomAttribute) && !t.IsNested);
            foreach(Type type in types)
            {
                RegisterCustomAttribute(type);
            }
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
            if(classToRegister.BaseType != typeof(CommandCallbacks))
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
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CommandCallbacks) && !t.IsNested);
            foreach(Type type in types)
            {
                RegisterCommands(type);
            }
        }

        public void RunCommand(TSender sender, string input)
        {
            //TODO: quotes and phrases
            var prms = input.Split(' ').ToList();
            RunCommand(sender, prms[0], prms.GetRange(1, prms.Count - 1));
        }

        public void RunCommand(TSender sender, string name, IEnumerable<string> parameters)
        {
            try
            {
                commandRepository.Invoke(sender, name, parameters);
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
                SendFailMessage(sender, $"The {name} command threw an error:\n{e}");
            }
        }
    }
}
