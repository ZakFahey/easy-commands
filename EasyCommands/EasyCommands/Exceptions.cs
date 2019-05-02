using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public class ParserInitializationException : Exception
    {
        public ParserInitializationException(string message) : base(message)
        {
        }
    }

    public class CommandRegistrationException : Exception
    {
        public CommandRegistrationException(string message) : base(message)
        {
        }
    }

    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(string message) : base(message)
        {
        }
    }

    public class CommandParsingException : Exception
    {
        public CommandParsingException(string message) : base(message)
        {
        }
    }
}
