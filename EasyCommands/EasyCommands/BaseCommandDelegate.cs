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
        public override void Invoke(TSender sender, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
