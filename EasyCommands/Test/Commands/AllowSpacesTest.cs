﻿using System;
using EasyCommands;
using Example;

namespace EasyCommands.Test.Commands
{
    class AllowSpacesTest : CommandCallbacks
    {
        // Registration should fail because there are multiple parameters with the AllowSpaces attribute
        [Command("test")]
        public void Test(
            User sender,
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
