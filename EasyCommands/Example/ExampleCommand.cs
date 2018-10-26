using System;
using EasyCommands;

namespace Example
{
    /// <summary>
    /// Attribute for a command delegate. Extended to provide extra functionality.
    /// </summary>
    public class ExampleCommand : Command
    {
        public string Documentation;
    }
}
