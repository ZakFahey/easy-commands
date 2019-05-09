using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example;
using System.IO;
using System.Collections.Generic;

namespace EasyCommands.Test.Tests
{
    [TestClass]
    public class ExampleCommandTests
    {
        User CurrentUser;
        ExampleCommandHandler CommandHandler;
        StringWriter ConsoleOutput;
        
        public ExampleCommandTests()
        {
            CurrentUser = UserDatabase.GetUserByName("Admin");
            CommandHandler = new ExampleCommandHandler();
            CommandHandler.RegisterCommands("Example.Commands");
            ConsoleOutput = new StringWriter();
        }

        [TestInitialize]
        public void SetConsoleOutput()
        {
            ConsoleOutput.Flush();
            Console.SetOut(ConsoleOutput);
        }

        [TestCleanup]
        public void ResetConsoleOutput()
        {
            StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
        }

        //TODO: test documentation/help command
        //TODO: documentation for aliases

        [TestMethod]
        [Description("Empty commands throw an error.")]
        public void TestEmptyCommand()
        {
            CommandHandler.RunCommand(CurrentUser, "");
            Assert.AreEqual("Please enter a command." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Nonexistent commands throw an error.")]
        public void TestInvalidCommand()
        {
            CommandHandler.RunCommand(CurrentUser, "asdf 1 2 3");
            Assert.AreEqual("Command \"asdf\" does not exist." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Nonexistent subcommands throw an error.")]
        public void TestInvalidSubcommand()
        {
            CommandHandler.RunCommand(CurrentUser, "window asdf 1 2 3");
            Assert.AreEqual("Command \"window asdf\" does not exist." + Environment.NewLine
                + "window contains these subcommands:" + Environment.NewLine
                + "window resize <width> <height>" + Environment.NewLine
                + "window move <left> <top>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The add command works.")]
        public void TestAdd()
        {
            CommandHandler.RunCommand(CurrentUser, "add 1 2");
            Assert.AreEqual("1 + 2 = 3" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The subtract command works, inferring the command name from the method name.")]
        public void TestSubtract()
        {
            CommandHandler.RunCommand(CurrentUser, "subtract 10 5");
            Assert.AreEqual("10 - 5 = 5" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The divide command works.")]
        public void TestDivide()
        {
            CommandHandler.RunCommand(CurrentUser, "divide 1 2");
            Assert.AreEqual("1 / 2 = 0.5" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The divide command works with its alias.")]
        public void TestDiv()
        {
            CommandHandler.RunCommand(CurrentUser, "div 1 2");
            Assert.AreEqual("1 / 2 = 0.5" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands throw an error when you input the wrong number of arguments.")]
        public void TestTooManyArguments()
        {
            CommandHandler.RunCommand(CurrentUser, "add 1 2 3");
            Assert.AreEqual("Incorrect number of arguments! Proper syntax: add <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands throw an error when you input an invalid argument type.")]
        public void TestInvalidArgument()
        {
            CommandHandler.RunCommand(CurrentUser, "add 1 bleh");
            Assert.AreEqual("Invalid syntax! num2 must be a whole number!" + Environment.NewLine
                + "Proper syntax: add <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands show the correct parameter name when overridden.")]
        public void TestOverriddenParamName()
        {
            CommandHandler.RunCommand(CurrentUser, "subtract");
            Assert.AreEqual("Incorrect number of arguments! Proper syntax: subtract <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with optional arguments work with the optional command omitted.")]
        public void TestOptionalArguments()
        {
            CommandHandler.RunCommand(CurrentUser, "add3or4 1 2 3");
            Assert.AreEqual("sum = 6" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with optional arguments work with the optional command included.")]
        public void TestOptionalArguments2()
        {
            CommandHandler.RunCommand(CurrentUser, "add3or4 1 2 3 4");
            Assert.AreEqual("sum = 10" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with optional arguments throw an error when given the wrong number of arguments and show the correct proper syntax.")]
        public void TestOptionalArgumentsProperSyntax()
        {
            CommandHandler.RunCommand(CurrentUser, "add3or4");
            Assert.AreEqual("Incorrect number of arguments! Proper syntax: add3or4 <num1> <num2> <num3> [num4]" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The myname command works.")]
        public void TestMyName()
        {
            CommandHandler.RunCommand(CurrentUser, "myname");
            Assert.AreEqual("Your name is Admin." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The favorite-food command to get a user's favorite food.")]
        public void TestFavoriteFoodGet()
        {
            CommandHandler.RunCommand(CurrentUser, "favorite-food Jeff");
            Assert.AreEqual("Jeff's favorite food is steak." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The favorite-food command to set a user's favorite food.")]
        public void TestFavoriteFoodSet()
        {
            CommandHandler.RunCommand(CurrentUser, "favorite-food Blake bananas");
            User Blake = UserDatabase.GetUserByName("Blake");
            Assert.AreEqual("bananas", Blake.FavoriteFood);
        }

        [TestMethod]
        [Description("Parameters are properly set when in quotes.")]
        public void TestParamsInQuotes()
        {
            CommandHandler.RunCommand(CurrentUser, "favorite-food Henry \"creamed corn\"");
            User Henry = UserDatabase.GetUserByName("Henry");
            Assert.AreEqual("creamed corn", Henry.FavoriteFood);
        }

        [TestMethod]
        [Description("Parameters are properly set with escaped quotes.")]
        public void TestParamsInEscapedQuotes()
        {
            CommandHandler.RunCommand(CurrentUser, "favorite-food Jessica \\\"tacos\\\"");
            User Jessica = UserDatabase.GetUserByName("Jessica");
            Assert.AreEqual("\"tacos\"", Jessica.FavoriteFood);
        }

        [TestMethod]
        [Description("When a user isn't found, the correct error message is shown.")]
        public void TestUserNotFound()
        {
            CommandHandler.RunCommand(CurrentUser, "favorite-food Carl");
            Assert.AreEqual("User Carl not found." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with the AllowSpaces attribute can include spaces without quotation marks.")]
        public void TestMultiWordArgument()
        {
            CommandHandler.RunCommand(CurrentUser, "add-user Jimmy baked potatoes");
            User Jimmy = UserDatabase.GetUserByName("Jimmy");
            Assert.IsNotNull(Jimmy);
            Assert.AreEqual("baked potatoes", Jimmy.FavoriteFood);
        }

        [TestMethod]
        [Description("Blank commands with subcommands show the available subcommands.")]
        public void TestSubCommand()
        {
            CommandHandler.RunCommand(CurrentUser, "window");
            Assert.AreEqual("window contains these subcommands:" + Environment.NewLine
                + "window resize <width> <height>" + Environment.NewLine
                + "window move <left> <top>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The window resize subcommand works.")]
        public void TestWindowResize()
        {
            CommandHandler.RunCommand(CurrentUser, "window resize 800 600");
            Assert.AreEqual("Window dimensions set to 800 x 600." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The window move subcommand works, inferring the command name from the method name.")]
        public void TestWindowMove()
        {
            CommandHandler.RunCommand(CurrentUser, "window move 100 100");
            Assert.AreEqual("Window position set to (100, 100)." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The help command shows all available commands.")]
        public void TestHelp()
        {
            CommandHandler.RunCommand(CurrentUser, "help");
            string[] lines = ConsoleOutput.ToString().Split('\n', Environment.NewLine[0]);
            Assert.AreEqual("Available commands:", lines[0]);
            
            // Verify that the commands are there, regardless of order
            var actual = new HashSet<string>();
            for(int i = 1; i < lines.Length; i++)
            {
                actual.Add(lines[i]);
            }
            var expected = new HashSet<string>()
            {
                "add <num1> <num2>",
                "subtract <num1> <num2>",
                "divide <num1> <num2>",
                "add3or4 <num1> <num2> <num3> [num4]",
                "myname",
                "favorite-food <querying> [food]",
                "add-user <name> <favoriteFood>",
                "window <resize|move>",
                "help [command] [subcommand]"
            };
            Assert.IsTrue(expected.SetEquals(actual));
        }

        [TestMethod]
        [Description("The help command shows the description for a command.")]
        public void TestHelpWithCommand()
        {
            CommandHandler.RunCommand(CurrentUser, "help add");
            Assert.AreEqual("Adds two integers together. Syntax: add <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The help command shows the description for a subcommand.")]
        public void TestHelpWithSubcommand()
        {
            CommandHandler.RunCommand(CurrentUser, "help window move");
            Assert.AreEqual("Moves the window to a certain position. Syntax: window move <left> <top>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The help command shows the description for a command with subcommands.")]
        public void TestHelpWithCommandWithSubcommands()
        {
            CommandHandler.RunCommand(CurrentUser, "help window");
            Assert.AreEqual("Manipulates the console window. Subcommands:" + Environment.NewLine
                + "window resize <width> <height>" + Environment.NewLine
                + "window move <left> <top>" + Environment.NewLine, ConsoleOutput.ToString());
        }
    }
}
