using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCommands;

namespace Example
{
    public class ExampleParsingRules : ParsingRules
    {
        [ParseRule]
        public string ParseString(string arg)
        {
            return arg;
        }

        [ParseRule]
        public int ParseInt(string arg)
        {
            int ret = 0;
            if(!int.TryParse(arg, out ret))
            {
                Fail("Invalid syntax! @0 must be a whole number!");
            }
            return ret;
        }

        [ParseRule]
        public float ParseFloat(string arg)
        {
            float ret = 0;
            if(!float.TryParse(arg, out ret))
            {
                Fail("Invalid syntax! @0 must be a number!");
            }
            return ret;
        }

        [ParseRule]
        public double ParseDouble(string arg)
        {
            double ret = 0;
            if(!double.TryParse(arg, out ret))
            {
                Fail("Invalid syntax! @0 must be a number!");
            }
            return ret;
        }

        [ParseRule]
        public User ParseUser(string arg)
        {
            User user = UserDatabase.GetUserByName(arg);
            if(user == null)
            {
                Fail($"User {arg} not found.");
            }
            return user;
        }
    }
}
