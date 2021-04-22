using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class SubInstruction : Instruction
    {
        protected SubInstruction(Bus bus, string name) : base(bus, name)
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

        public SUBARegA(Bus bus) : base(bus, "SUB A, A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().A, true, true);

            return 1;
        }
    }

    public class SUBARegB : SubInstruction
    {
        public static new byte OpCode => 0x90;

        public SUBARegB(Bus bus) : base(bus, "SUB A, B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().B, true, true);

            return 1;
        }
    }

    public class SUBARegC : SubInstruction
    {
        public static new byte OpCode => 0x91;

        public SUBARegC(Bus bus) : base(bus, "SUB A, C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().C, true, true);

            return 1;
        }
    }

    public class SUBARegD : SubInstruction
    {
        public static new byte OpCode => 0x92;

        public SUBARegD(Bus bus) : base(bus, "SUB A, D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().D, true, true);

            return 1;
        }
    }

    public class SUBARegE : SubInstruction
    {
        public static new byte OpCode => 0x93;

        public SUBARegE(Bus bus) : base(bus, "SUB A, E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().E, true, true);

            return 1;
        }
    }

    public class SUBARegH : SubInstruction
    {
        public static new byte OpCode => 0x94;

        public SUBARegH(Bus bus) : base(bus, "SUB A, H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().H, true, true);

            return 1;
        }
    }

    public class SUBARegL : SubInstruction
    {
        public static new byte OpCode => 0x95;

        public SUBARegL(Bus bus) : base(bus, "SUB A, L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.GetCPU().L, true, true);

            return 1;
        }
    }

    public class SUBAAddrHL : SubInstruction
    {
        public static new byte OpCode => 0x96;

        public SUBAAddrHL(Bus bus) : base(bus, "SUB A, (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Sub(bus.GetCPU().A, bus.ReadMemory(address), true, true);

            return 2;
        }
    }

    public class SUBAImpl : SubInstruction
    {
        public static new byte OpCode => 0xD6;

        protected byte value = 0;

        public SUBAImpl(Bus bus) : base(bus, "SUB A")
        {
        }

        public override int Execute()
        {
            byte data = bus.GetCPU().Fetch();

            bus.GetCPU().A = Sub(bus.GetCPU().A, data, true, true);

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
