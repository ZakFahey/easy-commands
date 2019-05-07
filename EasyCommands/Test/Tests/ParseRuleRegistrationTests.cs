using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using EasyCommands.Test.ParsingRules;

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
                handler.AddParsingRules(typeof(InvalidParsingRules1));
            });
        }
    }
}
