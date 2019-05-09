using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Wrapper for a command callback method
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public abstract class CommandDelegate<TSender>
    {
        public string Name { get; private set; }
        public string[] Aliases { get; private set; }

        protected Context<TSender> Context;
        protected Dictionary<Type, CustomAttribute> customAttributes = new Dictionary<Type, CustomAttribute>();

        public abstract void Invoke(TSender sender, string args);
        public abstract string SyntaxDocumentation();

        public CommandDelegate(Context<TSender> context, string mainName, string[] allNames)
        {
            Context = context;
            Name = mainName;
            Aliases = allNames.ToList().GetRange(1, allNames.Length - 1).ToArray();
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
