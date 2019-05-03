using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    class CommandRepository<TSender>
    {
        private Dictionary<string, CommandDelegate<TSender>> commands = new Dictionary<string, CommandDelegate<TSender>>();
        private TextOptions textOptions;

        public CommandRepository(TextOptions options)
        {
            textOptions = options;
        }

        public CommandDelegate<TSender> GetCallback(string name, IEnumerable<string> parameters)
        {
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException($"Command ");
            }
            return null;
        }
    }
}
