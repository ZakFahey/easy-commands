using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    /// <summary>
    /// Change how a <see cref="SubCommand"/> is executed
    /// </summary>
    public enum SubCommandType
    {
        /// <summary>
        /// This does nothing special, it just execute the command when it's called
        /// </summary>
        Standard,
        /// <summary>
        /// Mark the command as the default one, it will be executed whenever an invalid command is executed
        /// </summary>
        Default
    }
}
