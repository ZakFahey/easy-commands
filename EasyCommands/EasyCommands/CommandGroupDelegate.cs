using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command with sub-commands
    /// </summary>
    class CommandGroupDelegate<TSender> : CommandDelegate<TSender>
    {
        public CommandGroupDelegate(TextOptions options) : base(options) { }

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
