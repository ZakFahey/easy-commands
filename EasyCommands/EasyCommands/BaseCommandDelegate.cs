using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command without sub-commands
    /// </summary>
    class BaseCommandDelegate<TSender> : CommandDelegate<TSender>
    {
        private MethodInfo callback;
        private ParameterInfo[] callbackParams;

        public BaseCommandDelegate(TextOptions options, ArgumentParser parser, string name, MethodInfo callback) : base(options, parser, name)
        {
            this.callback = callback;
            callbackParams = callback.GetParameters();
        }

        public override void Invoke(TSender sender, IEnumerable<string> args)
        {
            if(args.Count() != callbackParams.Count() - 1)
            {
                throw new CommandParsingException(string.Format(textOptions.WrongNumberOfArguments, "test"));
            }
            var invocationParams = new object[callbackParams.Count()];
            invocationParams[0] = sender;
            for(int i = 1; i < callbackParams.Count(); i++)
            {
                invocationParams[i] = parser.ParseArgument(callbackParams[i].ParameterType, args.ElementAt(i - 1));
            }
            object instance = Activator.CreateInstance(callback.DeclaringType);
            callback.Invoke(instance, invocationParams);
        }
    }
}
