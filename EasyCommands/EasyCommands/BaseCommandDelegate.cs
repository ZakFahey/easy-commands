using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyCommands
{
    /// <summary>
    /// Command delegate for command without sub-commands
    /// </summary>
    public class BaseCommandDelegate<TSender> : CommandDelegate<TSender>
    {
        private MethodInfo callback;
        private ParameterInfo[] callbackParams;
        private string[] paramNames;
        private int phraseIndex;
        private int minLength;
        private int maxLength;
        private string syntaxDocumentation;

        public string ShortName { get; private set; }

        public BaseCommandDelegate(Context<TSender> context, string name, string shortName, MethodInfo callback) : base(context, name)
        {
            ShortName = shortName;
            this.callback = callback;
            callbackParams = callback.GetParameters();
            paramNames = callbackParams.Select(p => {
                ParamName nameOverride = p.GetCustomAttribute<ParamName>();
                return nameOverride == null ? p.Name : nameOverride.Name;
            }).ToArray();
            maxLength = callbackParams.Length;
            minLength = Array.FindIndex(callbackParams, p => p.HasDefaultValue);
            if(minLength == -1)
            {
                minLength = maxLength;
            }
            phraseIndex = Array.FindIndex(callbackParams, p => p.GetCustomAttribute<AllowSpaces>() != null);
            syntaxDocumentation = Name + " " + string.Join(" ", paramNames.Select((nm, i) => i >= minLength ? $"[{nm}]" : $"<{nm}>"));
            foreach(CustomAttribute attribute in callback.GetCustomAttributes<CustomAttribute>(true))
            {
                customAttributes[attribute.GetType()] = attribute;
            }
            
            if(minLength != maxLength && phraseIndex >= 0)
            {
                //TODO: allow phrase and optional parameter if they are both the same thing
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} cannot contain a parameter with the AllowSpaces attribute along with any optional parameters.");
            }
            if(callback.ReturnType != typeof(void))
            {
                throw new CommandRegistrationException($"{callback.DeclaringType.Name}.{callback.Name} must return void.");
            }
            if(callbackParams.Count(p => p.GetCustomAttribute<AllowSpaces>() != null) > 1)
            {
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} cannot contain more than one parameter with the AllowSpaces attribute.");
            }
            ParameterInfo undefinedParam = callbackParams.FirstOrDefault(p => !Context.ArgumentParser.ParseRuleExists(p.ParameterType));
            if(undefinedParam != null)
            {
                throw new CommandRegistrationException(
                    $"The parameter {undefinedParam.Name} in the command {callback.DeclaringType.Name}.{callback.Name} does " +
                    $"not contain a corresponding parse rule for type {undefinedParam.ParameterType.Name}.");
            }
        }

        public override string SyntaxDocumentation()
        {
            return syntaxDocumentation;
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
                argList[phraseIndex] = string.Join(" ", argList.GetRange(phraseIndex, phraseLength));
                if(phraseIndex + 1 < argList.Count)
                {
                    argList.RemoveRange(phraseIndex + 1, phraseLength - 1);
                }
            }
            Invoke(sender, argList);
        }

        void Invoke(TSender sender, IEnumerable<string> args)
        {
            if(args.Count() < minLength || args.Count() > maxLength)
            {
                throw new CommandParsingException(string.Format(Context.TextOptions.WrongNumberOfArguments, SyntaxDocumentation()));
            }
            var invocationParams = new object[callbackParams.Length];
            for(int i = 0; i < invocationParams.Length; i++)
            {
                if(i >= args.Count())
                {
                    invocationParams[i] = callbackParams[i].DefaultValue;
                }
                else
                {
                    //TODO: textOptions formatting should be elsewhere
                    invocationParams[i] = Context.ArgumentParser.ParseArgument(
                        callbackParams[i].ParameterType, args.ElementAt(i), paramNames[i], SyntaxDocumentation());
                }
            }
            CommandCallbacks<TSender> instance = (CommandCallbacks<TSender>)Activator.CreateInstance(callback.DeclaringType);
            instance.Context = Context;
            instance.Sender = sender;
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