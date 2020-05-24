using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public class TextOptions
    {
        public string ProperSyntax;
        public string WrongNumberOfArguments;
        public string CommandPrefix;
        public string ShowSubcommands;
        public string SubcommandsShort;
        public string CommandNotFound;
        public string EmptyCommand;
        public string FlagArgWithNoValue;
        public string FlagNotFound;
        public string CommandThrewException;
        /// <summary> The regex pattern that all command names must follow </summary>
        public string CommandNameValidationRegex;
        public string InvalidCommandNameErrorMessage;

        public static TextOptions Default()
        {
            return new TextOptions
            {
                ProperSyntax = "Proper syntax: {0}",
                WrongNumberOfArguments = "Incorrect number of arguments! Proper syntax: {0}",
                CommandPrefix = "",
                ShowSubcommands = "{0} contains these subcommands:",
                CommandNotFound = "Command \"{0}\" does not exist.",
                EmptyCommand = "Please enter a command.",
                FlagArgWithNoValue = "Invalid syntax! {0} must have a corresponding value.",
                FlagNotFound = "Invalid syntax! {0} is not a valid flag. Valid flags: {1}",
                CommandThrewException = "The command threw an error.",
                CommandNameValidationRegex = @"^[a-z0-9\-]*$",
                InvalidCommandNameErrorMessage =
                    "Failed to register command \"{0}\". Commands may only contain lowercase letters, " +
                    "numbers, and the dash symbol."
            };
        }
    }
}
