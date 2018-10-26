using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommands
{
    public abstract class ParsingRules<TSender>
    {
        public TSender Sender;
        public bool Failed = false;

        private string attributeName;
        private string validSyntax;
        private TextOptions textOptions;

        public void Fail(string message)
        {
            Failed = true;
            SendFailMessage(string.Format(message, attributeName));
            SendFailMessage(string.Format(textOptions.ProperSyntax, validSyntax));
        }

        protected abstract void SendFailMessage(string message);
    }
}
