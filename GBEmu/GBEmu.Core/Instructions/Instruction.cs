using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions
{
    public abstract class Instruction
    {
        public static byte OpCode => 0xFF;

        protected readonly Bus bus;
        protected int usedCycles;

        public string Name { get; }

        public byte Cycles { get; }

        protected Instruction(Bus bus, string name, byte cycles)
        {
            this.bus = bus;

            Name = name;
            Cycles = cycles;

            usedCycles = 0;
        }

        public abstract override string ToString();

        public abstract int Execute();

        protected ushort CombineHILO(byte hi, byte lo)
        {
            return (ushort)((hi << 8) + lo);
        }
    }

    public class NOP : Instruction
    {
        public static new byte OpCode => 0x00;

        public NOP(Bus bus) : base(bus, "NOP", 1)
        {
        }

        public override int Execute()
        {
            return usedCycles;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
