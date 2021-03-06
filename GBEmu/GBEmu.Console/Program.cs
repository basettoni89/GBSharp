using GBEmu.CLI.CPUDebug;
using GBEmu.Core;
using System;

namespace GBEmu.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus bus = new Bus();

            InstructionTable instructionTable = new InstructionTable(bus.GetCPU());
            Console.Write(instructionTable.ToString());
        }
    }
}
