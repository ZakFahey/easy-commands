﻿using System;
using EasyCommands;
using EasyCommands.Commands;
using Example;

namespace EasyCommands.Test.Commands
{
    class SubcommandTest7 : CommandCallbacks<User>
    {
        // Registration should fail because this command doesn't contain any subcommands
        [Command("test")]
        public class TestCommand : CommandCallbacks<User>
        {
        }
    }
}
