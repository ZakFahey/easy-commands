﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command without sub-commands
    /// </summary>
    class BaseCommandDelegate<TSender> : CommandDelegate<TSender>
    {
        private MethodInfo callback;
        private ParameterInfo[] callbackParams;
        private int phraseIndex;
        private int minLength;
        private int maxLength;

        public BaseCommandDelegate(TextOptions options, ArgumentParser parser, string name, MethodInfo callback) : base(options, parser, name)
        {
            this.callback = callback;
            callbackParams = callback.GetParameters();
            maxLength = callbackParams.Length - 1;
            minLength = Array.FindIndex(callbackParams, p => p.HasDefaultValue) - 1;
            if(minLength == -2)
            {
                minLength = maxLength;
            }
            phraseIndex = Array.FindIndex(callbackParams, p => p.GetCustomAttribute<AllowSpaces>() != null);

            if(minLength != maxLength && phraseIndex >= 0)
            {
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} cannot contain a parameter with the AllowSpaces attribute along with any optional parameters.");
            }
            if(callback.ReturnType != typeof(void))
            {
                throw new CommandRegistrationException($"{callback.DeclaringType.Name}.{callback.Name} must return void.");
            }
            if(callbackParams.Length < 1 || callbackParams[0].ParameterType != typeof(TSender))
            {
                throw new CommandRegistrationException($"{callback.DeclaringType.Name}.{callback.Name} must start with a sender parameter.");
            }
            if(callbackParams.Count(p => p.GetCustomAttribute<AllowSpaces>() != null) > 1)
            {
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} cannot contain more than one parameter with the AllowSpaces attribute.");
            }
            ParameterInfo undefinedParam = callbackParams.FirstOrDefault(p => !parser.ParseRuleExists(p.ParameterType));
            if(undefinedParam != null)
            {
                throw new CommandRegistrationException(
                    $"The parameter {undefinedParam.Name} in the command {callback.DeclaringType.Name}.{callback.Name} does " +
                    $"not contain a corresponding parse rule for type {undefinedParam.ParameterType.Name}.");
            }
        }

        public override void Invoke(TSender sender, string args)
        {
            var argList = new List<string>();
            bool inQuotes = false;
            // Build up space-separated list with quotes accounted for
            for(int i = 0; i < args.Length; i++)
            {
                if(argList.Count == 0)
                {
                    argList.Add("");
                }
                char c = args[i];
                if(i < args.Length - 1 && c == '\\' && args[i + 1] == '"')
                {
                    // Add escaped quotes
                    argList[argList.Count - 1] += '"';
                    i++;
                }
                else if(c == '"')
                {
                    // Enter or exit a quote
                    inQuotes = !inQuotes;
                }
                else if(c == ' ' && !inQuotes)
                {
                    argList.Add("");
                }
                else
                {
                    argList[argList.Count - 1] += c;
                }
            }
            // Combine arguments if there is a phrase
            if(argList.Count > maxLength && phraseIndex >= 0)
            {
                int phraseLength = argList.Count - maxLength + 1;
                argList[phraseIndex - 1] = string.Join(" ", argList.GetRange(phraseIndex - 1, phraseLength));
                if(phraseIndex < argList.Count)
                {
                    argList.RemoveRange(phraseIndex, phraseLength - 1);
                }
            }
            Invoke(sender, argList);
        }

        void Invoke(TSender sender, IEnumerable<string> args)
        {
            if(args.Count() < minLength || args.Count() > maxLength)
            {
                throw new CommandParsingException(string.Format(textOptions.WrongNumberOfArguments, "test"));
            }
            var invocationParams = new object[callbackParams.Length];
            invocationParams[0] = sender;
            for(int i = 1; i < invocationParams.Length; i++)
            {
                if(i > args.Count())
                {
                    invocationParams[i] = callbackParams[i].DefaultValue;
                }
                else
                {
                    invocationParams[i] = parser.ParseArgument(callbackParams[i].ParameterType, args.ElementAt(i - 1), callbackParams[i].Name);
                }
            }
            object instance = Activator.CreateInstance(callback.DeclaringType);
            try
            {
                callback.Invoke(instance, invocationParams);
            }
            catch(TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
