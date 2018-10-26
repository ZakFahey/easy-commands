using System;
using EasyCommands;

namespace Example.Commands
{
    class MathCommands
    {
        [ExampleCommand(
            CommandName = "add",
            Documentation = "Adds two integers together.")]
        void Add(User sender, int num1, int num2)
        {
            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
        }

        [ExampleCommand(CommandName = "subtract",
            Documentation = "Subtracts num2 from num1.")]
        void Subtract(User sender, int num1, int num2)
        {
            Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
        }

        [ExampleCommand(CommandName = "divide",
            Documentation = "Divides num1 by num2.")]
        void Divide(User sender, float num1, float num2)
        {
            try
            {
                Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
            }
            catch(ArithmeticException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Divide by zero error!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        [ExampleCommand(
            CommandName = "add3or4",
            Documentation = "Adds 3 or 4 integers together.")]
        void Add3or4(User sender, int num1, int num2, int num3, int num4 = 0)
        {
            Console.WriteLine($"sum = {num1 + num2 + num3 + num4}");
        }
    }
}
