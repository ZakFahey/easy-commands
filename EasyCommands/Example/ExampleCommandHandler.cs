using System;
using EasyCommands;
using EasyCommands.Defaults;
using EasyCommands.Commands;

namespace Example
{
    public class ExampleCommandHandler : CommandHandler<User>
    {
        public ExampleCommandHandler() : base() { }
        public ExampleCommandHandler(TextOptions options) : base(options) { }

        protected override void SendFailMessage(User sender, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach(string line in message.Split('\n'))
            {
                Console.WriteLine(line);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Initialize()
        {
            AddParsingRules(typeof(DefaultParsingRules<User>));
            AddParsingRules(typeof(ExampleParsingRules<User>));
            AddFlagRule(typeof(ExampleFlags));
        }

        public override void PreCheck(User sender, CommandDelegate<User> command)
        {
            AccessLevel permLevel = command.GetCustomAttribute<AccessLevel>();
            if(permLevel != null && sender.PermissionLevel < permLevel.MinimumLevel)
            {
                Fail($"Command \"{command.Name}\" does not exist.");
            }
        }

        public override bool CanSeeCommand(User sender, CommandDelegate<User> command)
        {
            AccessLevel permLevel = command.GetCustomAttribute<AccessLevel>();
            return permLevel == null || sender.PermissionLevel >= permLevel.MinimumLevel;
        }
    }
}
