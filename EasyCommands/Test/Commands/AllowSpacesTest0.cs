using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class AllowSpacesTest0 : CommandCallbacks<User>
    {
        // Registration should fail because there are multiple parameters with the AllowSpaces attribute
        [Command("test")]
        public void Test(
            [AllowSpaces]
            string val1,
            [AllowSpaces]
            string val2)
        {
            Console.WriteLine(val1);
            Console.WriteLine(val2);
        }
    }
}
