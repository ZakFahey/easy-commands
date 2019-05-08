using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands
{
    class ArgumentParser
    {
        private Dictionary<Type, MethodInfo> parsingRules = new Dictionary<Type, MethodInfo>();

        public void AddParsingRules(Type rules)
        {
            if(rules.BaseType != typeof(ParsingRules))
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
            ParsingRules instance = (ParsingRules)Activator.CreateInstance(rule.DeclaringType);
            instance.ParameterName = parameterName;
            instance.ProperSyntax = properSyntax;
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
