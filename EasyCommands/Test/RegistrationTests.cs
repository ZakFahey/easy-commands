using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyCommands;
using Example;
using EasyCommands.Test.Commands;

namespace EasyCommands.Test
{
    [TestClass]
    class RegistrationTests
    {
        [TestMethod]
        [Description("Attributes are passed from classes to methods.")]
        public void Test()
        {
            //TODO
            Assert.Fail();
        }

        //TODO: Test that commands are successfully added

        //TODO: ensure errors are thrown for things like duplicate commands (accounting for different arguments and subcommands),
        //ambiguous optional parameters, and ambiguous phrases
        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands.")]
        public void TestErrorIfDuplicateCommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var parser = new CommandParser<User, ExampleParsingRules>();
                parser.RegisterClass(typeof(DuplicateTest0));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands that share an alias.")]
        public void TestErrorIfDuplicateCommandWithAlias()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var parser = new CommandParser<User, ExampleParsingRules>();
                parser.RegisterClass(typeof(DuplicateTest1));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate subcommands for a command.")]
        public void TestErrorIfDuplicateSubcommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var parser = new CommandParser<User, ExampleParsingRules>();
                parser.RegisterClass(typeof(DuplicateTest3));
            });
        }

        [TestMethod]
        [Description("Command registration doesn't throw an error if a command with the same name has a different number of arguments.")]
        public void TestDuplicateCommandWithDifferentArguments()
        {
            var parser = new CommandParser<User, ExampleParsingRules>();
            parser.RegisterClass(typeof(DuplicateTest2));
            //No error
        }
    }
}
