﻿using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest5 : CommandCallbacks<User>
    {
        // Registration should fail due to invalid command-subcommand structure
        [SubCommand("test")]
        public void Test()
        {
            Console.WriteLine("Something");
        }
    }
}
