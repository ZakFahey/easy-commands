using System;
using EasyCommands;

namespace Example
{
    /// <summary>
    /// Specifies the description for a command. Add to the top of a class to pass it to all delegates below.
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

    /// <summary>
    /// Specifies the description for a subcommand. Add to the top of a class to pass it to all delegates below.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SubCommandDocumentation : CustomAttribute
    {
        public string Documentation { get; private set; }

        public SubCommandDocumentation(string documentation)
        {
            Documentation = documentation;
        }
    }
}
