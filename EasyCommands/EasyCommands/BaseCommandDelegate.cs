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
        /// <summary> Callbacks with the corresponding argument count </summary>
        private Dictionary<int, MethodInfo> callbacks = new Dictionary<int, MethodInfo>();

        public BaseCommandDelegate(TextOptions options) : base(options) { }

        public override List<ParameterInfo> GetParameters(int parameterCount)
        {
            throw new NotImplementedException();
        }

        public override void Invoke(TSender sender, IEnumerable<object> args)
        {
            throw new NotImplementedException();
        }
    }
}
