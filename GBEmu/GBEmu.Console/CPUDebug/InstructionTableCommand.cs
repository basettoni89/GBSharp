using GBEmu.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.CLI.CPUDebug
{
    public class InstructionTableCommand : Command
    {
        public static new string Name => "lookup";

        public override string Description => "Print the actual implemented instruction lookup table";

        private const int CELL_WIDTH = 13;
        private const int CELL_HEIGHT = 4;

        private const int CELL_IN_ROW = 16;

        private readonly CPU cpu;

        public InstructionTableCommand(CPU cpu, ConsoleHandler handler) : base(handler)
        {
            this.cpu = cpu;
        }

        public override void Execute()
        {
            StringBuilder sb = new StringBuilder();
            var lookup = cpu.GetInstructionLookup();

            string instructionName = string.Empty;

            for (long i = 0; i < 0x100 * CELL_WIDTH * CELL_HEIGHT; i++)
            {
                long charRow = i / (CELL_IN_ROW * CELL_WIDTH);
                long charCol = i % (CELL_IN_ROW * CELL_WIDTH);
                long row = i / (CELL_IN_ROW * CELL_WIDTH * CELL_HEIGHT);
                long col = (i / CELL_WIDTH) % CELL_IN_ROW;

                long cellRow = charRow % CELL_HEIGHT;
                long cellCol = charCol % CELL_WIDTH;
                int nameOffset = (int)cellCol - 2;

                if (charRow % CELL_HEIGHT == CELL_HEIGHT / 2 && cellCol == 0)
                {
                    byte opcode = (byte)((row * CELL_IN_ROW) + col);
                    if (lookup.ContainsKey(opcode))
                    {
                        Instruction instruction = lookup[opcode];
                        instructionName = instruction?.ToString() ?? string.Empty;
                    }
                    else
                    {
                        instructionName = string.Empty;
                    }
                }

                if (i % (CELL_IN_ROW * CELL_WIDTH) == 0)
                {
                    sb.Append("|");
                    sb.Append(Environment.NewLine);
                }

                if (cellRow == 0)
                {
                    sb.Append("-");
                }
                else if (cellCol == 0)
                {
                    sb.Append("|");
                }
                else if (charRow % CELL_HEIGHT == CELL_HEIGHT / 2 && nameOffset < instructionName.Length && nameOffset >= 0)
                {
                    sb.Append(instructionName.Substring(nameOffset, 1));
                }
                else
                {
                    sb.Append(" ");
                }
            }

            handler.NewLine();
            
            handler.Write(sb.ToString());

            handler.NewLine();
            handler.NewLine();
        }
    }
}
