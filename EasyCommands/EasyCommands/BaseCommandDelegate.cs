using System;
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

        public BaseCommandDelegate(TextOptions options, ArgumentParser parser, string name, MethodInfo callback) : base(options, parser, name)
        {
            this.callback = callback;
            callbackParams = callback.GetParameters();
            if(callback.ReturnType != typeof(void))
            {
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} must return void.");
            }
            if(callbackParams.Length < 1 || callbackParams[0].ParameterType != typeof(TSender))
            {
                throw new CommandRegistrationException(
                    $"{callback.DeclaringType.Name}.{callback.Name} must start with a sender parameter.");
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

        public override void Invoke(TSender sender, IEnumerable<string> args)
        {
            if(args.Count() != callbackParams.Length - 1)
            {
                throw new CommandParsingException(string.Format(textOptions.WrongNumberOfArguments, "test"));
            }
            var invocationParams = new object[callbackParams.Length];
            invocationParams[0] = sender;
            for(int i = 1; i < callbackParams.Length; i++)
            {
                invocationParams[i] = parser.ParseArgument(callbackParams[i].ParameterType, args.ElementAt(i - 1), callbackParams[i].Name);
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
