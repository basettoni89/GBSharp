using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class XorInstruction : Instruction
    {
        public XorInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
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

        public XORARegA(Bus bus) : base(bus, "XOR A, A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().A);

            return usedCycles;
        }
    }

    public class XORARegB : XorInstruction
    {
        public static new byte OpCode => 0xA8;

        public XORARegB(Bus bus) : base(bus, "XOR A, B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().B);

            return usedCycles;
        }
    }

    public class XORARegC : XorInstruction
    {
        public static new byte OpCode => 0xA9;

        public XORARegC(Bus bus) : base(bus, "XOR A, C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().C);

            return usedCycles;
        }
    }

    public class XORARegD : XorInstruction
    {
        public static new byte OpCode => 0xAA;

        public XORARegD(Bus bus) : base(bus, "XOR A, D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().D);

            return usedCycles;
        }
    }

    public class XORARegE : XorInstruction
    {
        public static new byte OpCode => 0xAB;

        public XORARegE(Bus bus) : base(bus, "XOR A, E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().E);

            return usedCycles;
        }
    }

    public class XORARegH : XorInstruction
    {
        public static new byte OpCode => 0xAC;

        public XORARegH(Bus bus) : base(bus, "XOR A, H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().H);

            return usedCycles;
        }
    }

    public class XORARegL : XorInstruction
    {
        public static new byte OpCode => 0xAD;

        public XORARegL(Bus bus) : base(bus, "XOR A, L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.GetCPU().L);

            return usedCycles;
        }
    }

    public class XORAAddrHL : XorInstruction
    {
        public static new byte OpCode => 0xAE;

        public XORAAddrHL(Bus bus) : base(bus, "XOR A, (HL)", 2)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Xor(bus.GetCPU().A, bus.ReadMemory(address));

            usedCycles += 2;
            return usedCycles;
        }
    }

    public class XORAImpl : XorInstruction
    {
        public static new byte OpCode => 0xEE;

        protected byte value = 0;

        public XORAImpl(Bus bus) : base(bus, "XOR A", 2)
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;
            usedCycles++;

            bus.GetCPU().A = Xor(bus.GetCPU().A, data);
            usedCycles++;

            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
