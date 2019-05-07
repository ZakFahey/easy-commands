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
        protected ArgumentParser parser;

        public CommandRepository(TextOptions options, ArgumentParser parser)
        {
            textOptions = options;
            this.parser = parser;
        }

        public void Invoke(TSender sender, string name, IEnumerable<string> parameters)
        {
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException(string.Format(textOptions.CommandNotFound, name));
            }
            commands[name].Invoke(sender, parameters);
        }
    }
}
