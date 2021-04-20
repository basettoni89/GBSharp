using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class OrInstruction : Instruction
    {
        public OrInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        protected byte Or(byte a, byte b)
        {
            byte r = (byte)(a | b);

            bus.GetCPU().Flags.ZF = r == 0;
            bus.GetCPU().Flags.N = false;

            bus.GetCPU().Flags.H = false;
            bus.GetCPU().Flags.CY = false;

            return r;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class ORARegA : OrInstruction
    {
        public static new byte OpCode => 0xB7;

        public ORARegA(Bus bus) : base(bus, "OR A, A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().A);

            return 1;
        }
    }

    public class ORARegB : OrInstruction
    {
        public static new byte OpCode => 0xB0;

        public ORARegB(Bus bus) : base(bus, "OR A, B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().B);

            return 1;
        }
    }

    public class ORARegC : OrInstruction
    {
        public static new byte OpCode => 0xB1;

        public ORARegC(Bus bus) : base(bus, "OR A, C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().C);

            return 1;
        }
    }

    public class ORARegD : OrInstruction
    {
        public static new byte OpCode => 0xB2;

        public ORARegD(Bus bus) : base(bus, "OR A, D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().D);

            return 1;
        }
    }

    public class ORARegE : OrInstruction
    {
        public static new byte OpCode => 0xB3;

        public ORARegE(Bus bus) : base(bus, "OR A, E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().E);

            return 1;
        }
    }

    public class ORARegH : OrInstruction
    {
        public static new byte OpCode => 0xB4;

        public ORARegH(Bus bus) : base(bus, "OR A, H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().H);

            return 1;
        }
    }

    public class ORARegL : OrInstruction
    {
        public static new byte OpCode => 0xB5;

        public ORARegL(Bus bus) : base(bus, "OR A, L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().L);

            return 1;
        }
    }

    public class ORAAddrHL : OrInstruction
    {
        public static new byte OpCode => 0xB6;

        public ORAAddrHL(Bus bus) : base(bus, "OR A, (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Or(bus.GetCPU().A, bus.ReadMemory(address));

            return 2;
        }
    }

    public class ORAImpl : OrInstruction
    {
        public static new byte OpCode => 0xF6;

        protected byte value = 0;

        public ORAImpl(Bus bus) : base(bus, "OR A")
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;

            bus.GetCPU().A = Or(bus.GetCPU().A, data);

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
