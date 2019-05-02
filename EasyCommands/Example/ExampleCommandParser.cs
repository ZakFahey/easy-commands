using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCommands;

namespace Example
{
    public class ExampleCommandParser : CommandParser<User>
    {
        protected override void SendFailMessage(User sender, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public ExampleCommandParser() : base()
        {
            AddParsingRules(new ExampleParsingRules());
        }
    }
}
