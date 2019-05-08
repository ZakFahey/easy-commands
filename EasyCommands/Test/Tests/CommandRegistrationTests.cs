using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using EasyCommands.Test.Commands;

namespace EasyCommands.Test.Tests
{
    [TestClass]
    public class CommandRegistrationTests
    {
        //TODO: tests for ambiguous optional parameters
        //TODO: tests for nonexistent custom attribute format
        //TODO: what happens when nulls are passed into things?

        [TestMethod]
        [Description("Command registration succeeds for a simple command callback class.")]
        public void TestSimpleCommands()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommands(typeof(SimpleCommandTest));
            Assert.AreEqual(2, handler.NumCommands);
        }

        [TestMethod]
        [Description("Command registration throws an error when a command name is invalid.")]
        public void TestErrorIfInvalidName()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(InvalidNameTest));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands.")]
        public void TestErrorIfDuplicateCommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(DuplicateTest0));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate commands that share an alias.")]
        public void TestErrorIfDuplicateCommandWithAlias()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(DuplicateTest1));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when there are 2 duplicate subcommands for a command.")]
        public void TestErrorIfDuplicateSubcommand()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(SubcommandTest0));
            });
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands.")]
        public void TestSubcommand()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommands(typeof(SubcommandTest1));
            Assert.AreEqual(1, handler.NumCommands);
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands nested in a class.")]
        public void TestSubcommandInNestedClass()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommands(typeof(SubcommandTest2));
            Assert.AreEqual(1, handler.NumCommands);
        }

        [TestMethod]
        [Description("Command registration succeeds for a command with subcommands nested in a class mixed with another command.")]
        public void TestSubcommandInNestedClassWithSingleCommand()
        {
            var handler = new ExampleCommandHandler();
            handler.RegisterCommands(typeof(SubcommandTest3));
            Assert.AreEqual(2, handler.NumCommands);
        }

        [TestMethod]
        [Description("Command registration throws an error for a command class within a command.")]
        public void TestErrorIfInvalidSubcommandStructure()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(SubcommandTest4));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error for a subcommand not encased in a command.")]
        public void TestErrorIfInvalidSubcommandStructure2()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(SubcommandTest5));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error for subcommands more than 1 layer deep.")]
        public void TestErrorIfInvalidSubcommandStructure3()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(SubcommandTest6));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error when a command class doesn't contain any subcommands.")]
        public void TestErrorIfInvalidSubcommandStructure4()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(SubcommandTest7));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if command has parameter with no corresponding parse rule.")]
        public void TestErrorForNonexistentParseRule()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(CallbackSyntaxTest0));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if there is no sender parameter.")]
        public void TestErrorIfNoSenderParameter()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(CallbackSyntaxTest1));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if the return type on a command callback isn't void.")]
        public void TestErrorIfInvalidReturnType()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(CallbackSyntaxTest2));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if the command class doesn't inherit from CommandCallbacks.")]
        public void TestErrorIfInvalidCallbackClass()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(InvalidClassTest0));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if the subcommand class doesn't inherit from CommandCallbacks.")]
        public void TestErrorIfInvalidCallbackClass2()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(InvalidClassTest1));
            });
        }

        [TestMethod]
        [Description("Command registration throws an error if a command uses multiple AllowSpaces attributes.")]
        public void TestErrorIfMultipleAllowSpaces()
        {
            Assert.ThrowsException<CommandRegistrationException>(() => {
                var handler = new ExampleCommandHandler();
                handler.RegisterCommands(typeof(AllowSpacesTest));
            });
        }
    }
}
