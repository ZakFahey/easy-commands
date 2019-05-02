using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using EasyCommands.Test.Commands;

namespace EasyCommands.Test.Tests
{
    [TestClass]
    public class RegistrationTests
    {
        //TODO: ensure errors are thrown for things like ambiguous optional parameters and ambiguous phrases
        //TODO: tests for nonexistent parse rule and custom attribute format
        //TODO: tests for invalid command-subcommand class structure

        [TestMethod]
        [Description("Attributes are passed from classes to methods.")]
        public void Test()
        {
            //TODO
            Assert.Fail();
        }

        [TestMethod]
        [Description("Command registration succeeds for a simple command callback class.")]
        public void TestSimpleCommands()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommandCallbacks(typeof(SimpleCommandTest));
            // No error
            //TODO: Test that commands are successfully added
        }

        [TestMethod]
        [Description("Command registration throws an error when a command name is invalid.")]
        public void TestErrorIfInvalidName()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommandCallbacks(typeof(InvalidNameTest));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands.")]
        public void TestErrorIfDuplicateCommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommandCallbacks(typeof(DuplicateTest0));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands that share an alias.")]
        public void TestErrorIfDuplicateCommandWithAlias()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommandCallbacks(typeof(DuplicateTest1));
            });
        }

        [TestMethod]
        [Description("Command registration doesn't throw an error if a command with the same name has a different number of arguments.")]
        public void TestDuplicateCommandWithDifferentArguments()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommandCallbacks(typeof(DuplicateTest2));
            // No error
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate subcommands for a command.")]
        public void TestErrorIfDuplicateSubcommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommandCallbacks(typeof(SubcommandTest0));
            });
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands.")]
        public void TestSubcommand()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommandCallbacks(typeof(SubcommandTest1));
            // No error
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands nested in a class.")]
        public void TestSubcommandInNestedClass()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommandCallbacks(typeof(SubcommandTest2));
            // No error
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands nested in a class mixed with another command.")]
        public void TestSubcommandInNestedClassWithSingleCommand()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommandCallbacks(typeof(SubcommandTest2));
            // No error
        }
    }
}
