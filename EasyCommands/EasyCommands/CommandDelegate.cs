using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    public abstract class CommandDelegate<TSender>
    {
        public string Name { get; private set; }

        protected Context<TSender> Context;
        protected Dictionary<Type, CustomAttribute> customAttributes = new Dictionary<Type, CustomAttribute>();

        public abstract void Invoke(TSender sender, string args);
        public abstract string SyntaxDocumentation();

        public CommandDelegate(Context<TSender> context, string name)
        {
            Context = context;
            Name = name;
        }

        public T GetCustomAttribute<T>() where T : CustomAttribute
        {
            if(!customAttributes.ContainsKey(typeof(T)))
            {
                return null;
            }
            return (T)customAttributes[typeof(T)];
        }
    }
}
