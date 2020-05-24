using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography.X509Certificates;

namespace EasyCommands.Test
{
    /// <summary>
    /// Utility to read console input one line at a time
    /// </summary>
    class ConsoleReader
    {
        StringWriter consoleOutput;
        string[] lines;
        int lineIndex = -1;

        public ConsoleReader()
        {
            consoleOutput = new StringWriter();
            consoleOutput.Flush();
            Console.SetOut(consoleOutput);
        }

        /// <summary>
        /// Returns the text of a single line of the console output
        /// </summary>
        public string ReadLine()
        {
            if(lineIndex < 0)
            {
                lines = consoleOutput.ToString().Split(new char[] { '\n', Environment.NewLine[0] }, StringSplitOptions.RemoveEmptyEntries);
            }
            lineIndex++;
            if(lineIndex >= lines.Length)
            {
                Assert.Fail("Tried to read beyond the end of the console output.");
            }
            return lineIndex < lines.Length ? lines[lineIndex] : "";
        }

        /// <summary>
        /// Tests whether the remainder of the console output contains all of and only the specified lines, regardless of order
        /// </summary>
        public void AssertOutputContains(params string[] expected)
        {
            lineIndex++;
            if(lineIndex >= lines.Length)
            {
                Assert.Fail("Tried to read beyond the end of the console output.");
            }
            var actual = new HashSet<string>();
            while(lineIndex < lines.Length)
            {
                actual.Add(lines[lineIndex]);
                lineIndex++;
            }
            var expectedSet = new HashSet<string>(expected);
            if(!expectedSet.SetEquals(actual))
            {
                Assert.Fail(
                    "Console output not equal to expected output." +
                    "\nExpected:\n" + string.Join("\n", expected) +
                    "\nActual:\n" + string.Join("\n", actual));
            }
        }

        public void AssertEndOfOutput()
        {
            if (lineIndex < lines.Length - 1)
            {
                string failureMessage =
                    "Expected end of console output, but found the following extra lines:";
                while (lineIndex < lines.Length - 1)
                {
                    failureMessage += "\n" + ReadLine();
                }
                Assert.Fail(failureMessage);
            }
        }

        public void Close()
        {
            StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
            consoleOutput.Close();
        }
    }
}
