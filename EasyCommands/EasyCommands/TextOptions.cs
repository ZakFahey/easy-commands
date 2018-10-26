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

        public static TextOptions Default()
        {
            return new TextOptions
            {
                ProperSyntax = "Proper syntax: @0",
                WrongNumberOfArguments = "Incorrect number of arguments! Proper syntax: @0",
                CommandPrefix = "",
                ShowSubcommands = "@0 contains these subcommands:",
                SubcommandsShort = " Subcommands:"
            };
        }
    }
}
