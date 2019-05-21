using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using EasyCommands.Test.ParsingRules;
using System;

namespace EasyCommands.Test.Tests
{
    [TestClass]
    public class ParseRuleRegistrationTests
    {
        [TestMethod]
        [Description("Example command handler succeeds in initializing and registering the example parsing rules.")]
        public void TestCommandHandlerInstantiation()
        {
            var handler = new ExampleCommandHandler();
            // No error
        }

        [TestMethod]
        [Description("Command handler fails if it registers a parsing rule class that doesn't inherit from ParsingRules.")]
        public void TestErrorIfInvalidParsingRule0()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddParsingRules(typeof(InvalidParsingRules0));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a parsing rule with invalid method syntax.")]
        public void TestErrorIfInvalidParsingRule1()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddParsingRules(typeof(InvalidParsingRules1<User>));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a parsing rule with a non-attribute type in ParseRule's argument.")]
        public void TestErrorIfInvalidParsingRule2()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddParsingRules(typeof(InvalidParsingRules2<User>));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a flag that doesn't have the FlagsArgument attribute.")]
        public void TestErrorIfInvalidFlagRule0()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(typeof(InvalidFlags0));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a null flag.")]
        public void TestErrorIfInvalidFlagRule1()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(null);
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a flag with an argument of an unregistered type.")]
        public void TestErrorIfInvalidFlagRule2()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(typeof(InvalidFlags1));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a flag with an argument that contains a space.")]
        public void TestErrorIfInvalidFlagRule3()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(typeof(InvalidFlags2));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a flag with an argument that is also flags.")]
        public void TestErrorIfInvalidFlagRule4()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(typeof(InvalidFlags3));
            });
        }

        [TestMethod]
        [Description("Command handler fails if it registers a flag with duplicate argument names.")]
        public void TestErrorIfInvalidFlagRule5()
        {
            Assert.ThrowsException<ParserInitializationException>(() =>
            {
                var handler = new ExampleCommandHandler();
                handler.AddFlagRule(typeof(InvalidFlags4));
            });
        }
    }
}
