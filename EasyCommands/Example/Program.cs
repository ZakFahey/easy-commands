using System;
using EasyCommands;

namespace Example
{
    class Program
    {
        public static User CurrentUser;

        static void Main(string[] args)
        {
            CurrentUser = UserDatabase.GetUserByName("Admin");
            var commandParser = new CommandParser<User, ExampleParsingRules>();
            commandParser.RegisterNamespace("Example.Commands");
            Console.WriteLine("Input a command. Type `help` to see a list of commands.");
            while(true)
            {
                commandParser.RunCommand(CurrentUser, Console.ReadLine());
            }
        }
    }
}
