using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class AllowSpacesTest1 : CommandCallbacks<User>
    {
        // Registration should fail because there is an AllowSpaces attribute along with an optional parameter
        [Command("test")]
        public void Test(
            [AllowSpaces]
            string val1,
            int val2 = 10)
        {
            Console.WriteLine(val1);
            Console.WriteLine(val2);
        }
    }
}
