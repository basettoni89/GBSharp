using GBEmu.CLI.CPUDebug;
using GBEmu.Core;
using System;
using System.Collections.Generic;

namespace GBEmu.CLI
{
    class Program
    {
        private static Bus bus = new Bus();

        private static ConsoleHandler handler = new ConsoleHandler();
        private static Dictionary<string, Command> commands = new Dictionary<string, Command>()
        {
            {InstructionTableCommand.Name, new InstructionTableCommand(bus.GetCPU(), handler)}
        };


        static void Main(string[] args)
        {
            PrintHelp();

            while(true)
            {
                string command = handler.ReadLine("Command: ").Trim();

                if(commands.ContainsKey(command))
                {
                    commands[command].Execute();
                }
                else if (command == "help")
                {
                    PrintHelp();
                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    handler.WriteLine("Command not found");
                }
            }
        }

        private static void PrintHelp()
        {
            handler.NewLine();

            foreach (string key in commands.Keys)
            {
                handler.WriteLine($" * {key} - {commands[key].Description}");
            }

            handler.WriteLine(" * help - Print the command list");
            handler.WriteLine(" * exit - Quit the app");

            handler.NewLine();
        }
    }
}
