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
            commandRepository = new CommandRepository<TSender>(textOptions);
            Initialize();
        }

        public CommandHandler(TextOptions options)
        {
            textOptions = options;
            commandRepository = new CommandRepository<TSender>(textOptions);
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
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CustomAttribute));
            foreach(Type type in types)
            {
                RegisterCustomAttribute(type);
            }
        }

        public void RegisterCommandCallbacks(Type classToRegister)
        {
            if(classToRegister.BaseType != typeof(CommandCallbacks))
            {
                throw new ParserInitializationException("classToRegister must have the base class CommandCallbacks.");
            }
            //TODO
        }

        public void RegisterCommandCallbacks(string namespaceToRegister)
        {
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CommandCallbacks));
            foreach(Type type in types)
            {
                RegisterCommandCallbacks(type);
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
                // TODO: subcommands need parameters to be off by 1
                CommandDelegate<TSender> callback = commandRepository.GetCallback(name, parameters);
                var invocationParams = new object[parameters.Count()];
                var callbackParams = callback.GetParameters(parameters.Count());
                for(int i = 0; i < parameters.Count(); i++)
                {
                    invocationParams[i] = argumentParser.ParseArgument(callbackParams[i].ParameterType, parameters.ElementAt(i));
                }
                callback.Invoke(sender, invocationParams);
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
