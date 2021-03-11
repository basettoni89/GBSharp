using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class SumInstruction : Instruction
    {
        public SumInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
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

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class SUMARegA : SumInstruction
    {
        public static new byte OpCode => 0x87;

        public SUMARegA(Bus bus) : base(bus, "SUM A, A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().A, true, true);

            return usedCycles;
        }
    }

    public class SUMARegB : SumInstruction
    {
        public static new byte OpCode => 0x80;

        public SUMARegB(Bus bus) : base(bus, "SUM A, B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().B, true, true);

            return usedCycles;
        }
    }

    public class SUMARegC : SumInstruction
    {
        public static new byte OpCode => 0x81;

        public SUMARegC(Bus bus) : base(bus, "SUM A, C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().C, true, true);

            return usedCycles;
        }
    }

    public class SUMARegD : SumInstruction
    {
        public static new byte OpCode => 0x82;

        public SUMARegD(Bus bus) : base(bus, "SUM A, D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().D, true, true);

            return usedCycles;
        }
    }

    public class SUMARegE : SumInstruction
    {
        public static new byte OpCode => 0x83;

        public SUMARegE(Bus bus) : base(bus, "SUM A, E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().E, true, true);

            return usedCycles;
        }
    }

    public class SUMARegH : SumInstruction
    {
        public static new byte OpCode => 0x84;

        public SUMARegH(Bus bus) : base(bus, "SUM A, H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().H, true, true);

            return usedCycles;
        }
    }

    public class SUMARegL : SumInstruction
    {
        public static new byte OpCode => 0x85;

        public SUMARegL(Bus bus) : base(bus, "SUM A, L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.GetCPU().L, true, true);

            return usedCycles;
        }
    }

    public class SUMAAddrHL : SumInstruction
    {
        public static new byte OpCode => 0x86;

        public SUMAAddrHL(Bus bus) : base(bus, "SUM A, (HL)", 2)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Sum(bus.GetCPU().A, bus.ReadMemory(address), true, true);

            usedCycles += 2;
            return usedCycles;
        }
    }

    public class SUMAImpl : SumInstruction
    {
        public static new byte OpCode => 0xC6;

        protected byte value = 0;

        public SUMAImpl(Bus bus) : base(bus, "SUM A", 2)
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;
            usedCycles++;

            bus.GetCPU().A = Sum(bus.GetCPU().A, data, true, true);
            usedCycles ++;

            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
