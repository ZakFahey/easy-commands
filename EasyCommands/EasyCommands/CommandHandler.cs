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
        private TextOptions textOptions = TextOptions.Default();
        private CommandRepository<TSender> commandRepository = new CommandRepository<TSender>();
        private ArgumentParser argumentParser = new ArgumentParser();

        protected abstract void SendFailMessage(TSender sender, string message);

        public void AddParsingRules(Type rules)
        {
            argumentParser.AddParsingRules(rules);
        }

        public void RegisterCustomAttribute(Type classToRegister)
        {

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
            //TODO: subcommands
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
            // TODO: subcommands need parameters to be off by 1
            MethodInfo callback = commandRepository.GetCallback(name, parameters);
            object instance = Activator.CreateInstance(callback.DeclaringType);

            var invocationParams = new List<object> { sender };
            foreach(ParameterInfo callbackParam in callback.GetParameters())
            {

                invocationParams.Add()
            }

            callback.Invoke(instance, invocationParams.ToArray());
        }
    }
}
