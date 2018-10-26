using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    public class CommandParser<TSender, TParsingRules> where TParsingRules : ParsingRules<TSender>, new()
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

        public CommandParser()
        {
            textOptions = TextOptions.Default();
        }

        private CommandParser(TextOptions textOptions)
        {
            this.textOptions = textOptions;
        }

        public void RegisterClass(Type classToRegister)
        {

        }

        public void RegisterNamespace(string namespaceToRegister)
        {
            IEnumerable<Type> types = allTypes.Where(t => t.IsClass && t.Namespace == namespaceToRegister);
            foreach(Type type in types)
            {
                RegisterClass(type);
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
