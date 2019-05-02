using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    public abstract class CommandParser<TSender>
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
        private List<ParsingRules> parsingRules = new List<ParsingRules>();
        private TextOptions textOptions = TextOptions.Default();

        protected abstract void SendFailMessage(TSender sender, string message);

        public void AddParsingRules(ParsingRules rules)
        {
            parsingRules.Add(rules);
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

        }

        public void RegisterCommandCallbacks(string namespaceToRegister)
        {
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister && t.BaseType == typeof(CommandCallbacks));
            foreach(Type type in types)
            {
                RegisterCommandCallbacks(type);
            }
        }

        public void RunCommand(TSender helperClass, string input)
        {

        }

        public void RunCommand(TSender helperClass, string name, IEnumerable<string> parameters)
        {

        }
    }
}
