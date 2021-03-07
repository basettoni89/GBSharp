﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class SubInstruction : Instruction
    {
        protected SubInstruction(Bus bus, byte opCode, string name, byte cycles) : base(bus, opCode, name, cycles)
        {
        }

        protected byte Sub(byte a, byte b, bool halfCarry, bool carry)
        {
            ushort r = (ushort)(a - b);

            bus.GetCPU().Flags.ZF = (byte)r == 0;
            bus.GetCPU().Flags.N = true;

            if (halfCarry)
                bus.GetCPU().Flags.H = (((a & 0b1111) - (b & 0b1111)) & (1 << 4)) != 0;

            if (carry)
                bus.GetCPU().Flags.CY = (r & (1 << 8)) != 0;

            return (byte)r;
        }
    }
}
