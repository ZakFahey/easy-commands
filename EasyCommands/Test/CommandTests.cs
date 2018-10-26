using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyCommands;
using Example;
using System.IO;

namespace EasyCommands.Test
{
    [TestClass]
    public class CommandTests
    {
        User CurrentUser;
        CommandParser<User, ExampleParsingRules> CommandParser;
        StringWriter ConsoleOutput;
        
        public CommandTests()
        {
            CurrentUser = UserDatabase.GetUserByName("Admin");
            CommandParser = new CommandParser<User, ExampleParsingRules>();
            CommandParser.RegisterNamespace("Example.Commands");
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

        [TestMethod]
        [Description("The add command works.")]
        public void TestAdd()
        {
            CommandParser.RunCommand(CurrentUser, "add 1 2");
            Assert.AreEqual("1 + 2 = 3" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The subtract command works.")]
        public void TestSubtract()
        {
            CommandParser.RunCommand(CurrentUser, "subtract 1 2");
            Assert.AreEqual("1 + 2 = 3" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The divide command works.")]
        public void TestDivide()
        {
            CommandParser.RunCommand(CurrentUser, "divide 1 2");
            Assert.AreEqual("1 / 2 = 0.5" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The divide command detects a divide by zero error.")]
        public void TestDivideByZero()
        {
            CommandParser.RunCommand(CurrentUser, "divide 2 0");
            Assert.AreEqual("Divide by zero error!" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands throw an error when you input the wrong number of arguments.")]
        public void TestTooManyArguments()
        {
            CommandParser.RunCommand(CurrentUser, "add 1 2 3");
            Assert.AreEqual("Incorrect number of arguments! Proper syntax: add <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands throw an error when you input an invalid argument type.")]
        public void TestInvalidArgument()
        {
            CommandParser.RunCommand(CurrentUser, "add 1 bleh");
            Assert.AreEqual("Invalid syntax! @0 must be a whole number!" + Environment.NewLine
                + "Proper syntax: add <num1> <num2>" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with optional arguments work with the optional command omitted.")]
        public void TestOptionalArguments()
        {
            CommandParser.RunCommand(CurrentUser, "add3or4 1 2 3");
            Assert.AreEqual("sum = 6" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with optional arguments work with the optional command included.")]
        public void TestOptionalArguments2()
        {
            CommandParser.RunCommand(CurrentUser, "add3or4 1 2 3 4");
            Assert.AreEqual("sum = 10" + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The myname command works.")]
        public void TestMyName()
        {
            CommandParser.RunCommand(CurrentUser, "myname");
            Assert.AreEqual("Your name is Admin." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The favorite-food command to get a user's favorite food.")]
        public void TestFavoriteFoodGet()
        {
            CommandParser.RunCommand(CurrentUser, "favorite-food Jeff");
            Assert.AreEqual("Jeff's favorite food is steak." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("The favorite-food command to get a user's favorite food.")]
        public void TestFavoriteFoodSet()
        {
            CommandParser.RunCommand(CurrentUser, "favorite-food Blake bananas");
            User Blake = UserDatabase.GetUserByName("Blake");
            Assert.AreEqual(Blake.FavoriteFood, "bananas");
        }

        [TestMethod]
        [Description("Parameters are properly set when in quotes.")]
        public void TestParamsInQuotes()
        {
            CommandParser.RunCommand(CurrentUser, "favorite-food Henry \"creamed corn\"");
            User Henry = UserDatabase.GetUserByName("Henry");
            Assert.AreEqual(Henry.FavoriteFood, "creamed corn");
        }

        [TestMethod]
        [Description("When a user isn't found, the correct error message is shown.")]
        public void TestUserNotFound()
        {
            CommandParser.RunCommand(CurrentUser, "favorite-food Carl");
            Assert.AreEqual("User Carl not found." + Environment.NewLine, ConsoleOutput.ToString());
        }

        [TestMethod]
        [Description("Commands with the AllowSpaces attribute can include spaces without quotation marks.")]
        public void TestMultiWordArgument()
        {
            CommandParser.RunCommand(CurrentUser, "add-user Jimmy baked potatoes");
            User Jimmy = UserDatabase.GetUserByName("Jimmy");
            Assert.IsNotNull(Jimmy);
            Assert.AreEqual(Jimmy.FavoriteFood, "baked potatoes");
        }
    }
}
