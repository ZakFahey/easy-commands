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

        protected void Fail(string message)
        {
            throw new CommandExecutionException(message);
        }
    }
}
