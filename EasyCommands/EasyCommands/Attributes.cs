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
    public class SubCommand : Command
    {
        public bool IsDefault { get; private set; }

        /// <param name="names">The aliases used to run the command</param>
        public SubCommand(params string[] names) : base(names)
        {
            IsDefault = false;
        }
        /// <param name="subCommandType">Determine whether this subcommand is a default subcommand or not</param>
        public SubCommand(SubCommandType subCommandType) : base(subCommandType == SubCommandType.Default ? new string[] { "" } : Array.Empty<string>())
        {
            IsDefault = subCommandType == SubCommandType.Default;
        }
    }

    /// <summary> Overrides the syntax documentation for a command. </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SyntaxOverride : Attribute
    {
        public string Syntax { get; private set; }

        public SyntaxOverride(string syntax)
        {
            Syntax = syntax;
        }
    }

    /// <summary>
    /// Attribute for methods that parse an argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ParseRule : Attribute
    {
    }

    /// <summary>
    /// Specifies that a class is parsed as a series of flags.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FlagsArgument : Attribute
    {
    }

    /// <summary>
    /// A parameter in a flag object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FlagParams : Attribute
    {
        public string[] Names { get; private set; }

        /// <param name="names">The names you can use to specify this parameter</param>
        public FlagParams(params string[] names)
        {
            if(names.Length < 1)
            {
                throw new ArgumentException("FlagParams must have at least 1 name.");
            }
            Names = names;
        }
    }

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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Field)]
    public abstract class CustomAttribute : Attribute
    {
    }
}
