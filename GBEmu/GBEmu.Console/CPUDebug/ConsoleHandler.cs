using System;

namespace GBEmu.CLI.CPUDebug
{
    public class ConsoleHandler
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void NewLine()
        {
            Console.WriteLine();
        }

        public string ReadLine(string prompt = null)
        {
            if(!string.IsNullOrEmpty(prompt))
            {
                Write(prompt);
            }

            return Console.ReadLine();
        }
    }
}