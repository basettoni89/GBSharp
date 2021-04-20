using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class XorInstruction : Instruction
    {
        public XorInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        protected byte Xor(byte a, byte b)
        {
            byte r = (byte)(a ^ b);

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

    public class XORARegA : XorInstruction
    {
        public static new byte OpCode => 0xAF;

        public XORARegA(Bus bus) : base(bus, "XOR A, A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().A);

            return 1;
        }
    }

    public class XORARegB : XorInstruction
    {
        public static new byte OpCode => 0xA8;

        public XORARegB(Bus bus) : base(bus, "XOR A, B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().B);

            return 1;
        }
    }

    public class XORARegC : XorInstruction
    {
        public static new byte OpCode => 0xA9;

        public XORARegC(Bus bus) : base(bus, "XOR A, C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().C);

            return 1;
        }
    }

    public class XORARegD : XorInstruction
    {
        public static new byte OpCode => 0xAA;

        public XORARegD(Bus bus) : base(bus, "XOR A, D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().D);

            return 1;
        }
    }

    public class XORARegE : XorInstruction
    {
        public static new byte OpCode => 0xAB;

        public XORARegE(Bus bus) : base(bus, "XOR A, E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().E);

            return 1;
        }
    }

    public class XORARegH : XorInstruction
    {
        public static new byte OpCode => 0xAC;

        public XORARegH(Bus bus) : base(bus, "XOR A, H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().H);

            return 1;
        }
    }

    public class XORARegL : XorInstruction
    {
        public static new byte OpCode => 0xAD;

        public XORARegL(Bus bus) : base(bus, "XOR A, L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().L);

            return 1;
        }
    }

    public class XORAAddrHL : XorInstruction
    {
        public static new byte OpCode => 0xAE;

        public XORAAddrHL(Bus bus) : base(bus, "XOR A, (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.ReadMemory(address));

            return 2;
        }
    }

    public class XORAImpl : XorInstruction
    {
        public static new byte OpCode => 0xEE;

        protected byte value = 0;

        public XORAImpl(Bus bus) : base(bus, "XOR A")
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;

            bus.GetCPU().A = Xor(bus.GetCPU().A, data);

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
