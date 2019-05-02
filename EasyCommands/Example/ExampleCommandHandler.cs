using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCommands;

namespace Example
{
    public class ExampleCommandHandler : CommandHandler<User>
    {
        protected override void SendFailMessage(User sender, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public ExampleCommandHandler() : base()
        {
            AddParsingRules(typeof(ExampleParsingRules));
        }
    }
}
