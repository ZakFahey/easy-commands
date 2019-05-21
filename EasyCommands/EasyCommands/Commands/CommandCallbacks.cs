using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands.Commands
{
    /// <summary>
    /// Holds all command callback methods
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public abstract class CommandCallbacks<TSender>
    {
        /// <summary> Stores and indexes all commands </summary>
        public CommandRepository<TSender> CommandRepository;
        public TextOptions TextOptions;
        /// <summary> Object containing the context of the user sending the command </summary>
        public TSender Sender;
        /// <summary> The unprocessed text used in the command </summary>
        public string RawCommandText;

        /// <summary>
        /// Sends an error message to the user and halts execution of the callback.
        /// </summary>
        protected void Fail(string message)
        {
            throw new CommandExecutionException(message);
        }
    }
}
