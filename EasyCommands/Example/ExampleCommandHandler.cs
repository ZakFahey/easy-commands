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
            AddParsingRules(typeof(ExampleParsingRules));
        }
    }
}
