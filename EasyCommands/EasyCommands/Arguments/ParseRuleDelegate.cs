using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands.Arguments
{
    abstract class ParseRuleDelegate<TSender>
    {
        protected Context<TSender> Context;

        public ParseRuleDelegate(Context<TSender> context)
        {
            Context = context;
        }

        public abstract object Invoke(string[] args, string parameterName, string properSyntax, object attributeOverride = null);
    }
}
