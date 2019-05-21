using System;
using EasyCommands;
using System.Globalization;
using EasyCommands.Arguments;
using System.Linq;

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
        public User[] ParseUsers(string[] args)
        {
            return args.Select(ParseUser).ToArray();
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
