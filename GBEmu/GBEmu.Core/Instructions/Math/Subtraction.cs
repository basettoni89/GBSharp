using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class SubInstruction : Instruction
    {
        protected SubInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
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

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class SUBARegA : SubInstruction
    {
        public static new byte OpCode => 0x97;

        public SUBARegA(Bus bus) : base(bus, "SUB A, A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().A, true, true);

            return usedCycles;
        }
    }

    public class SUBARegB : SubInstruction
    {
        public static new byte OpCode => 0x90;

        public SUBARegB(Bus bus) : base(bus, "SUB A, B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().B, true, true);

            return usedCycles;
        }
    }

    public class SUBARegC : SubInstruction
    {
        public static new byte OpCode => 0x91;

        public SUBARegC(Bus bus) : base(bus, "SUB A, C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().C, true, true);

            return usedCycles;
        }
    }

    public class SUBARegD : SubInstruction
    {
        public static new byte OpCode => 0x92;

        public SUBARegD(Bus bus) : base(bus, "SUB A, D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().D, true, true);

            return usedCycles;
        }
    }

    public class SUBARegE : SubInstruction
    {
        public static new byte OpCode => 0x93;

        public SUBARegE(Bus bus) : base(bus, "SUB A, E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().E, true, true);

            return usedCycles;
        }
    }

    public class SUBARegH : SubInstruction
    {
        public static new byte OpCode => 0x94;

        public SUBARegH(Bus bus) : base(bus, "SUB A, H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().H, true, true);

            return usedCycles;
        }
    }

    public class SUBARegL : SubInstruction
    {
        public static new byte OpCode => 0x95;

        public SUBARegL(Bus bus) : base(bus, "SUB A, L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().L, true, true);

            return usedCycles;
        }
    }

    public class SUBAAddrHL : SubInstruction
    {
        public static new byte OpCode => 0x96;

        public SUBAAddrHL(Bus bus) : base(bus, "SUB A, (HL)", 2)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.WriteMemory(Sub(bus.GetCPU().A, bus.ReadMemory(address), true, true), address);

            usedCycles += 2;
            return usedCycles;
        }
    }
}
