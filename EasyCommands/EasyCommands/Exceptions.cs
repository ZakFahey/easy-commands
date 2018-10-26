using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public class CommandRegistrationException : Exception
    {
        public CommandRegistrationException(string message) : base(message)
        {
        }
    }
}
