using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions
{
    public abstract class Instruction
    {
        public static byte OpCode => 0xFF;

        protected readonly Bus bus;

        public string Name { get; }

        protected Instruction(Bus bus, string name)
        {
            this.bus = bus;
            Name = name;
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

        public NOP(Bus bus) : base(bus, "NOP")
        {
        }

        public override int Execute()
        {
            return 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
