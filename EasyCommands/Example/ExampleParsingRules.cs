using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCommands;
using System.Globalization;

namespace Example
{
    class ExampleParsingRules<TSender> : ParsingRules<TSender>
    {
        [ParseRule]
        public User ParseUser(string arg)
        {
            User user = UserDatabase.GetUserByName(arg);
            if(user == null)
            {
                Fail($"User {arg} not found.", false);
            }
            return user;
        }

        [ParseRule]
        public PermissionLevel ParsePermissionLevel(string arg)
        {
            PermissionLevel level;
            if(!Enum.TryParse(arg, out level))
            {
                Fail($"{arg} is not a permission level. Valid values: {string.Join(", ", Enum.GetNames(typeof(PermissionLevel)))}", false);
            }
            return level;
        }

        [ParseRule]
        public int ParseIntAsHex(string arg, ReadAsHex attributeOverride)
        {
            int ret = 0;
            if(!int.TryParse(arg, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ret))
            {
                Fail("Invalid syntax! {0} must be a whole number!");
            }
            return ret;
        }
    }
}
