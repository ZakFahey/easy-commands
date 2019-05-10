using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    /// <summary>
    /// Holds all command callback methods
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public abstract class CommandCallbacks<TSender>
    {
        //TODO: does this class really need everything in Context?
        //TODO: RawCommandText variable

        /// <summary> Maintains the various classes you'd want to reference for a given CommandHandler </summary>
        public Context<TSender> Context;
        /// <summary> Object containing the context of the user sending the command </summary>
        public TSender Sender;

        /// <summary>
        /// Sends an error message to the user and halts execution of the callback.
        /// </summary>
        protected void Fail(string message)
        {
            throw new CommandExecutionException(message);
        }
    }
}
