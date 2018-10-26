using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    /// <summary>
    /// Used to mark a command name, subcommand, and argument count as reserved to prevent duplicates.
    /// Add the cross product of all command and subcommand aliases to the set of used commands.
    /// </summary>
    struct UniqueCommandKey
    {
        string BaseCommandName;
        string SubcommandName;
        int NumArgs;

        public UniqueCommandKey(string baseCommandName, string subcommandName, int numArgs)
        {
            BaseCommandName = baseCommandName;
            SubcommandName = subcommandName;
            NumArgs = numArgs;
        }
    }
}
