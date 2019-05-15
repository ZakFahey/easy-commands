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

        /// <summary> Attribute for a command delegate. </summary>
        /// <param name="names">The aliases used to run the command</param>
        public Command(params string[] names)
        {
            Names = names;
        }
    }

    /// <summary>
    /// Attribute for a subcommand.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SubCommand : Command
    {
        /// <summary> Attribute for a subcommand. </summary>
        /// <param name="names">The aliases used to run the command</param>
        public SubCommand(params string[] names) : base(names) { }
    }

    /// <summary>
    /// Attribute for methods that parse an argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ParseRule : Attribute
    {
        /// <summary> Attribute for methods that parse an argument. </summary>
        public ParseRule() { }
    }
    
    /// <summary>
    /// Overrides the default name of a parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParamName : Attribute
    {
        public string Name { get; private set; }

        /// <summary> Overrides the default name of a parameter. </summary>
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
        /// <summary> Specifies that a parameter can be multiple words without the use of quotes. Can only be used once in a command. </summary>
        public AllowSpaces() { }
    }

    /// <summary>
    /// Custom behavior you can add to a command or parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class CustomAttribute : Attribute
    {
    }
}
