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
    }
}
