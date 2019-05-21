using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands.Arguments
{
    /// <summary>
    /// Service to convert a string argument from a user's input into its respective parameter type
    /// </summary>
    /// <typeparam name="TSender">Object containing the context of the user sending the command</typeparam>
    public class ArgumentParser<TSender>
    {
        /// <summary> Class used to handle a handler with no attribute override </summary>
        private class NullAttribute
        {
        }

        private Dictionary<Type, Dictionary<Type, ParseRuleDelegate<TSender>>> parsingRules = new Dictionary<Type, Dictionary<Type, ParseRuleDelegate<TSender>>>();
        /// <summary> Maintains the various classes you'd want to reference for a given CommandHandler </summary>
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
                    if(parameters.Length < 1 || parameters.Length > 2 || parameters[0].ParameterType != typeof(string))
                    {
                        throw new ParserInitializationException(
                            $"Parse rule {rules.Name}.{rule.Name} has invalid arguments. It should have a single string parameter plus an optional attribute override parameter.");
                    }
                    Type attributeOverride = parameters.Length == 2 ? parameters[1].ParameterType : typeof(NullAttribute);
                    if(attributeOverride != typeof(NullAttribute) && attributeOverride.BaseType != typeof(Attribute))
                    {
                        throw new ParserInitializationException($"Attribute override parameter in {rules.Name}.{rule.Name} must inherit from Attribute.");
                    }
                    Dictionary<Type, ParseRuleDelegate<TSender>> rulesForThisType;
                    if(parsingRules.ContainsKey(rule.ReturnType))
                    {
                        rulesForThisType = parsingRules[rule.ReturnType];
                    }
                    else
                    {
                        rulesForThisType = new Dictionary<Type, ParseRuleDelegate<TSender>>();
                        parsingRules[rule.ReturnType] = rulesForThisType;
                    }
                    if(rulesForThisType.ContainsKey(attributeOverride))
                    {
                        Console.WriteLine($"WARNING: parse rule {rules.Name}.{rule.Name} is overriding an existing parse rule for type {rule.ReturnType.Name}.");
                    }
                    rulesForThisType[attributeOverride] = new NormalParseRuleDelegate<TSender>(Context, rule);
                }
            }
        }

        public void AddFlagRule(Type flags)
        {

        }

        public object ParseArgument(Type t, IEnumerable<object> parameterAttributes, string parameterName, string properSyntax, params string[] args)
        {
            var rulesForType = parsingRules[t];
            object attributeOverride = parameterAttributes.FirstOrDefault(a => rulesForType.ContainsKey(a.GetType()));
            ParseRuleDelegate<TSender> rule = rulesForType[attributeOverride == null ? typeof(NullAttribute) : attributeOverride.GetType()];
            return rule.Invoke(args, parameterName, properSyntax, attributeOverride);
        }

        public bool ParseRuleExists(Type t)
        {
            return parsingRules.ContainsKey(t);
        }
    }
}
