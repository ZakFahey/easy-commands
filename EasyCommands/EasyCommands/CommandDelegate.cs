using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    abstract class CommandDelegate<TSender>
    {
        public abstract void Invoke(TSender sender, string[] args);
    }
}
