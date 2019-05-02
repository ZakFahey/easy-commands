using System;
using System.Collections.Generic;

namespace EasyCommands
{
    /// <summary>
    /// Attribute for a command delegate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class Command : Attribute
    {
        public string[] Names { get; private set; }
        
        /// <param name="names">The aliases used to run the command</param>
        public Command(params string[] names)
        {
            Names = names;
        }
    }

    /// <summary>
    /// Attribute a subcommand.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SubCommand : Attribute
    {
        public string[] Names { get; private set; }

        /// <param name="names">The aliases used to run the command</param>
        public SubCommand(params string[] names)
        {
            Names = names;
        }
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
        public string Name { get; private set; }

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

    /// <summary>
    /// Custom behavior you can add to a command or parameter.
    /// </summary>
    public abstract class CustomAttribute : Attribute
    {
        public virtual void Before() { }
    }
}
