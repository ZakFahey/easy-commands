using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EasyCommands.Arguments
{
    class FlagParseRuleDelegate<TSender> : ParseRuleDelegate<TSender>
    {
        private Type flags;
        private Dictionary<string, FieldInfo> argumentMap;
        private string validFlags;

        public FlagParseRuleDelegate(Context<TSender> context, Type flags) : base(context)
        {
            this.flags = flags;
            argumentMap = new Dictionary<string, FieldInfo>();
            validFlags = "";

            foreach(FieldInfo field in flags.GetFields())
            {
                var flagParam = field.GetCustomAttribute<FlagParams>();
                if(flagParam != null)
                {
                    if(!Context.ArgumentParser.ParseRuleExists(field.FieldType))
                    {
                        throw new ParserInitializationException(
                            $"The field {field.Name} in the flag {flags.Name} does " +
                            $"not contain a corresponding parse rule for type {field.FieldType.Name}.");
                    }
                    if(Context.ArgumentParser.ParseRuleIsFlags(field.FieldType))
                    {
                        throw new ParserInitializationException(
                            $"The field {field.Name} in the flag {flags.Name} cannot be a flag itself.");
                    }

                    if(validFlags.Length > 0)
                    {
                        validFlags += ", ";
                    }
                    validFlags += flagParam.Names[0];

                    foreach(string name in flagParam.Names)
                    {
                        if(name.Contains(" "))
                        {
                            throw new ParserInitializationException(
                                $"Flags parameter {flags.Name} contains invalid argument name {name}. Argument names cannot contain spaces.");
                        }
                        if(argumentMap.ContainsKey(name))
                        {
                            throw new ParserInitializationException($"Flags parameter {flags.Name} contains ambiguous argument {name}.");
                        }
                        argumentMap[name] = field;
                    }
                }
            }
        }

        public override object Invoke(string[] args, string parameterName, string properSyntax, object attributeOverride)
        {
            object instance = Activator.CreateInstance(flags);

            for(int i = 0; i < args.Length; )
            {
                if(!argumentMap.ContainsKey(args[i]))
                {
                    throw new CommandParsingException(string.Format(Context.TextOptions.FlagNotFound, args[i], validFlags));
                }
                FieldInfo field = argumentMap[args[i]];
                if(field.FieldType == typeof(bool))
                {
                    field.SetValue(instance, true);
                    i++;
                }
                else
                {
                    if(i == args.Length - 1)
                    {
                        throw new CommandParsingException(string.Format(Context.TextOptions.FlagArgWithNoValue, args[i]));
                    }
                    object fieldValue = Context.ArgumentParser.ParseArgument
                        (field.FieldType, field.GetCustomAttributes(), parameterName, properSyntax, args[i + 1]);
                    field.SetValue(instance, fieldValue);
                    i += 2;
                }
            }

            return instance;
        }
    }
}
