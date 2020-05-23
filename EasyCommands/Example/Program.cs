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
            var commandHandler = new ExampleCommandHandler();
            commandHandler.RegisterCommands("Example.Commands");
            Console.WriteLine("Input a command. Type `help` to see a list of commands.");
            while(true)
            {
                 _ = commandHandler.RunCommandAsync(CurrentUser, Console.ReadLine());
            }
        }
    }
}
