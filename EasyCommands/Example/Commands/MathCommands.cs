using System;
using EasyCommands;
using EasyCommands.Commands;

namespace Example.Commands
{
    class MathCommands : CommandCallbacks<User>
    {
        [Command("add")]
        [CommandDocumentation("Adds two integers together.")]
        public void Add(int num1, int num2)
        {
            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
        }

        [Command] // If we do not include the command name, it can be inferred from the method name.
        [CommandDocumentation("Subtracts num2 from num1.")]
        public void Subtract(
            [ParamName("num1")]
            int num_1,
            [ParamName("num2")]
            int num_2)
        {
            Console.WriteLine($"{num_1} - {num_2} = {num_1 - num_2}");
        }

        [Command("divide", "div")]
        [CommandDocumentation("Divides num1 by num2.")]
        [SyntaxOverride("<num1> <num2>")] // Syntax override overrides the parameter names
        public void Divide(float num_1, float num_2)
        {
            Console.WriteLine($"{num_1} / {num_2} = {num_1 / num_2}");
        }

        [Command("add3or4")]
        [CommandDocumentation("Adds 3 or 4 integers together.")]
        public void Add3or4(int num1, int num2, int num3, int num4 = 0)
        {
            Console.WriteLine($"sum = {num1 + num2 + num3 + num4}");
        }

        [Command("hextodec")]
        [CommandDocumentation("Takes a number in hexadecimal and outputs it in decimal format.")]
        public void HexToDec([ReadAsHex] int num)
        {
            Console.WriteLine($"Decimal: {num}");
        }
    }
}
