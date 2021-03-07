using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions
{
    public abstract class Instruction
    {
        protected readonly Bus bus;
        protected int usedCycles;

        public byte OpCode { get; }

        public string Name { get; }

        public byte Cycles { get; }

        protected Instruction(Bus bus, byte opCode, string name, byte cycles)
        {
            this.bus = bus;

            OpCode = opCode;
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
        public NOP(Bus bus) : base(bus, 0x00, "NOP", 1)
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
