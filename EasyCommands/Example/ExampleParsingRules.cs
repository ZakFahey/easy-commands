using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCommands;

namespace Example
{
    public class ExampleParsingRules<TSender> : ParsingRules<TSender>
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
    }
}
