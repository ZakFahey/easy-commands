using System;
using EasyCommands;

namespace Example
{
    // These attributes extend from the CustomAttribute class, which lets the command handler keep track of them and lets you define custom behavior

    /// <summary>
    /// Specifies the description for a command.
    /// </summary>
    public class CommandDocumentation : CustomAttribute
    {
        public string Documentation { get; private set; }

        /// <summary> Specifies the description for a command. </summary>
        public CommandDocumentation(string documentation)
        {
            Documentation = documentation;
        }
    }

    /// <summary>
    /// Specifies the minimum permission level a user needs to run the command.
    /// </summary>
    public class AccessLevel : CustomAttribute
    {
        public PermissionLevel MinimumLevel { get; private set; }

        /// <summary> Specifies the minimum permission level a user needs to run the command. </summary>
        public AccessLevel(PermissionLevel minimumLevel)
        {
            MinimumLevel = minimumLevel;
        }
    }

    /// <summary>
    /// Specifies that an integer is read as a hexadecimal value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ReadAsHex : Attribute
    {
        /// <summary> Specifies that an integer is read as a hexadecimal value. </summary>
        public ReadAsHex() { }
    }
}
