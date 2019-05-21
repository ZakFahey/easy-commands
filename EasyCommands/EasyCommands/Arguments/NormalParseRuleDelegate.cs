using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands.Arguments
{
    class NormalParseRuleDelegate<TSender> : ParseRuleDelegate<TSender>
    {
        private MethodInfo rule;

        public NormalParseRuleDelegate(Context<TSender> context, MethodInfo rule) : base(context)
        {
            this.rule = rule;
        }

        public override object Invoke(string[] args, string parameterName, string properSyntax, object attributeOverride)
        {
            ParsingRules<TSender> instance = (ParsingRules<TSender>)Activator.CreateInstance(rule.DeclaringType);
            instance.ParameterName = parameterName;
            instance.ProperSyntax = properSyntax;
            instance.CommandRepository = Context.CommandRepository;
            instance.TextOptions = Context.TextOptions;
            try
            {
                object firstArg;
                if(rule.GetParameters()[0].ParameterType == typeof(string))
                {
                    firstArg = string.Join(" ", args);
                }
                else
                {
                    firstArg = args;
                }
                var invocationArgs = attributeOverride == null ? new object[] { firstArg } : new object[] { firstArg, attributeOverride };
                return rule.Invoke(instance, invocationArgs);
            }
            catch(TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
