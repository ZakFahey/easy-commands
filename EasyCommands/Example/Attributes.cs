using System;
using EasyCommands;

namespace Example
{
    /// <summary>
    /// Specifies the description for a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CommandDocumentation : CustomAttribute
    {
        public string Documentation { get; private set; }

        public CommandDocumentation(string documentation)
        {
            Documentation = documentation;
        }
    }
}
