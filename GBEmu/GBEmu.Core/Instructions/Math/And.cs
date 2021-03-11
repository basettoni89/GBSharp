using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class AndInstruction : Instruction
    {
        public AndInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
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

        public ANDARegA(Bus bus) : base(bus, "AND A, A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().A);

            return usedCycles;
        }
    }

    public class ANDARegB : AndInstruction
    {
        public static new byte OpCode => 0xA0;

        public ANDARegB(Bus bus) : base(bus, "AND A, B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().B);

            return usedCycles;
        }
    }

    public class ANDARegC : AndInstruction
    {
        public static new byte OpCode => 0xA1;

        public ANDARegC(Bus bus) : base(bus, "AND A, C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().C);

            return usedCycles;
        }
    }

    public class ANDARegD : AndInstruction
    {
        public static new byte OpCode => 0xA2;

        public ANDARegD(Bus bus) : base(bus, "AND A, D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().D);

            return usedCycles;
        }
    }

    public class ANDARegE : AndInstruction
    {
        public static new byte OpCode => 0xA3;

        public ANDARegE(Bus bus) : base(bus, "AND A, E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().E);

            return usedCycles;
        }
    }

    public class ANDARegH : AndInstruction
    {
        public static new byte OpCode => 0xA4;

        public ANDARegH(Bus bus) : base(bus, "AND A, H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().H);

            return usedCycles;
        }
    }

    public class ANDARegL : AndInstruction
    {
        public static new byte OpCode => 0xA5;

        public ANDARegL(Bus bus) : base(bus, "AND A, L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = And(bus.GetCPU().A, bus.GetCPU().L);

            return usedCycles;
        }
    }

    public class ANDAAddrHL : AndInstruction
    {
        public static new byte OpCode => 0xA6;

        public ANDAAddrHL(Bus bus) : base(bus, "AND A, (HL)", 2)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = And(bus.GetCPU().A, bus.ReadMemory(address));

            usedCycles += 2;
            return usedCycles;
        }
    }

    public class ANDAImpl : AndInstruction
    {
        public static new byte OpCode => 0xE6;

        protected byte value = 0;

        public ANDAImpl(Bus bus) : base(bus, "AND A", 2)
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;
            usedCycles++;

            bus.GetCPU().A = And(bus.GetCPU().A, data);
            usedCycles++;

            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
