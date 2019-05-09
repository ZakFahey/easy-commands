using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public abstract class ParsingRules<TSender>
    {
        public string ParameterName;
        public string ProperSyntax;
        public Context<TSender> Context;

        /// <summary>
        /// Fails the parsing and sends an error message
        /// </summary>
        /// <param name="message">The error message to show</param>
        /// <param name="showProperSyntax">Whether to prompt the user with the proper syntax for the command</param>
        public void Fail(string message, bool showProperSyntax = true)
        {
            string msg = string.Format(message, ParameterName);
            if(showProperSyntax)
            {
                msg += "\n" + string.Format(Context.TextOptions.ProperSyntax, ProperSyntax);
            }
            throw new CommandParsingException(msg);
        }
    }
}
