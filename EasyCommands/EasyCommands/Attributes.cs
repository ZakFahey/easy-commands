using System;

namespace EasyCommands
{
    /// <summary>
    /// Attribute for a command delegate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Command : Attribute
    {
        //TODO: aliases
        public string CommandName;
        public string Subcommands;
    }

    /// <summary>
    /// Attribute for methods that parse an argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ParseRule : Attribute
    {
    }

    //TODO: validation property attributes. Greater than 0, etc.

    /// <summary>
    /// Overrides the default name of a parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParamName : Attribute
    {
        public string Name;

        public ParamName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Specifies that a parameter can be multiple words without the use of quotes. Can only be used once in a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class AllowSpaces : Attribute
    {
    }
}
