using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class SumInstruction : Instruction
    {
        protected SumInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
        {
        }

        protected byte Sum(byte a, byte b, bool halfCarry, bool carry)
        {
            ushort r = (ushort)(a + b);

            bus.GetCPU().Flags.ZF = (byte)r == 0;
            bus.GetCPU().Flags.N = false;

            if (halfCarry)
                bus.GetCPU().Flags.H = (((a & 0b1111) + (b & 0b1111)) & (1 << 4)) != 0;

            if (carry)
                bus.GetCPU().Flags.CY = (r & (1 << 8)) != 0;

            return (byte)r;
        }
    }
}
