using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public abstract class CommandCallbacks<TSender>
    {
        public Context<TSender> Context;

        /// <summary>
        /// Sends an error message to the user and halts execution of the callback.
        /// </summary>
        protected void Fail(string message)
        {
            throw new CommandExecutionException(message);
        }
    }
}
