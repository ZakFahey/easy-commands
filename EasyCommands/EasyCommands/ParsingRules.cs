using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public abstract class ParsingRules
    {
        public string ParameterName;

        /// <summary>
        /// Fails the parsing and sends an error message
        /// </summary>
        public void Fail(string message)
        {
            throw new CommandParsingException(string.Format(message, ParameterName));
        }
    }
}
