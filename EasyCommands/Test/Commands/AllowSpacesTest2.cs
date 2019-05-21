using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class AllowSpacesTest2 : CommandCallbacks<User>
    {
        // Registration should succeed here with the optional parameter that allows spaces
        [Command("test")]
        public void Test([AllowSpaces]string msg = null)
        {
            if(msg == null)
            {
                Console.WriteLine("No message");
            }
            else
            {
                Console.WriteLine(msg);
            }
        }
    }
}
