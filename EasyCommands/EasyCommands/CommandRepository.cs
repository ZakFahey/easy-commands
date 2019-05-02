using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    public class CommandRepository<TSender>
    {
        private Dictionary<string, CommandDelegate<TSender>> commands = new Dictionary<string, CommandDelegate<TSender>>();

        public void RunCommand(ArgumentParser parser, string name, IEnumerable<string> parameters)
        {
            if(!commands.ContainsKey(name))
            {
                throw new CommandParsingException($"Command ")
            }
        }
    }
}
