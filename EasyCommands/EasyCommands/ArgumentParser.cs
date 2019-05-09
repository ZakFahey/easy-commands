using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Service to convert a string argument from a user's input into its respective parameter type
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public class ArgumentParser<TSender>
    {
        private Dictionary<Type, MethodInfo> parsingRules = new Dictionary<Type, MethodInfo>();
        private Context<TSender> Context;

        public ArgumentParser(Context<TSender> context)
        {
            Context = context;
        }

        public void AddParsingRules(Type rules)
        {
            if(rules.BaseType != typeof(ParsingRules<TSender>))
            {
                throw new ParserInitializationException($"{rules.Name} must have the base class ParsingRules.");
            }
            foreach(MethodInfo rule in rules.GetMethods())
            {
                if(rule.GetCustomAttribute(typeof(ParseRule)) != null)
                {
                    var parameters = rule.GetParameters();
                    if(parameters.Length != 1 || parameters[0].ParameterType != typeof(string))
                    {
                        throw new ParserInitializationException($"Parse rule {rules.Name}.{rule.Name} has invalid arguments. It should have a single string parameter.");
                    }
                    if(parsingRules.ContainsKey(rule.ReturnType))
                    {
                        Console.WriteLine($"WARNING: parse rule {rules.Name}.{rule.Name} is overriding an existing parse rule for type {rule.ReturnType.Name}.");
                    }
                    parsingRules[rule.ReturnType] = rule;
                }
            }
        }

        public object ParseArgument(Type t, string arg, string parameterName, string properSyntax)
        {
            var rule = parsingRules[t];
            ParsingRules<TSender> instance = (ParsingRules<TSender>)Activator.CreateInstance(rule.DeclaringType);
            instance.ParameterName = parameterName;
            instance.ProperSyntax = properSyntax;
            instance.Context = Context;
            try
            {
                return rule.Invoke(instance, new object[] { arg });
            }
            catch(TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public bool ParseRuleExists(Type t)
        {
            return parsingRules.ContainsKey(t);
        }
    }
}
