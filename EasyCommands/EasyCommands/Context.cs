using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    /// <summary>
    /// Maintains the various classes you'd want to reference for a given CommandHandler
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public class Context<TSender>
    {
        public CommandHandler<TSender> CommandHandler;
        public ArgumentParser<TSender> ArgumentParser;
        public CommandRepository<TSender> CommandRepository;
        public TextOptions TextOptions;
    }
}
