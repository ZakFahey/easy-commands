using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    abstract class CommandDelegate<TSender>
    {
        protected TextOptions textOptions;

        public abstract void Invoke(TSender sender, IEnumerable<object> args);
        public abstract List<ParameterInfo> GetParameters(int parameterCount);

        public CommandDelegate(TextOptions options)
        {
            textOptions = options;
        }
    }
}
