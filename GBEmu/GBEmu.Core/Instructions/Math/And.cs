using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class AndInstruction : Instruction
    {
        public AndInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        protected byte And(byte a, byte b)
        {
            byte r = (byte)(a & b);

            bus.GetCPU().Flags.ZF = r == 0;
            bus.GetCPU().Flags.N = false;

            bus.GetCPU().Flags.H = true;
            bus.GetCPU().Flags.CY = false;

            return r;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class ANDARegA : AndInstruction
    {
        public static new byte OpCode => 0xA7;

        public ANDARegA(Bus bus) : base(bus, "AND A, A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().A);

            return 1;
        }
    }

    public class ANDARegB : AndInstruction
    {
        public static new byte OpCode => 0xA0;

        public ANDARegB(Bus bus) : base(bus, "AND A, B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().B);

            return 1;
        }
    }

    public class ANDARegC : AndInstruction
    {
        public static new byte OpCode => 0xA1;

        public ANDARegC(Bus bus) : base(bus, "AND A, C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().C);

            return 1;
        }
    }

    public class ANDARegD : AndInstruction
    {
        public static new byte OpCode => 0xA2;

        public ANDARegD(Bus bus) : base(bus, "AND A, D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().D);

            return 1;
        }
    }

    public class ANDARegE : AndInstruction
    {
        public static new byte OpCode => 0xA3;

        public ANDARegE(Bus bus) : base(bus, "AND A, E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().E);

            return 1;
        }
    }

    public class ANDARegH : AndInstruction
    {
        public static new byte OpCode => 0xA4;

        public ANDARegH(Bus bus) : base(bus, "AND A, H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().H);

            return 1;
        }
    }

    public class ANDARegL : AndInstruction
    {
        public static new byte OpCode => 0xA5;

        public ANDARegL(Bus bus) : base(bus, "AND A, L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().L);

            return 1;
        }
    }

    public class ANDAAddrHL : AndInstruction
    {
        public static new byte OpCode => 0xA6;

        public ANDAAddrHL(Bus bus) : base(bus, "AND A, (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = And(bus.GetCPU().A, bus.ReadMemory(address));

            return 2;
        }
    }

    public class ANDAImpl : AndInstruction
    {
        public static new byte OpCode => 0xE6;

        protected byte value = 0;

        public ANDAImpl(Bus bus) : base(bus, "AND A")
        {
        }

        public override int Execute()
        {
            byte data = bus.GetCPU().Fetch();

            bus.GetCPU().A = And(bus.GetCPU().A, data);

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
