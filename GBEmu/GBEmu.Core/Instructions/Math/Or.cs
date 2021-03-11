using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public abstract class OrInstruction : Instruction
    {
        public OrInstruction(Bus bus, string name, byte cycles) : base(bus, name, cycles)
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

        public ORARegA(Bus bus) : base(bus, "OR A, A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().A);

            return usedCycles;
        }
    }

    public class ORARegB : OrInstruction
    {
        public static new byte OpCode => 0xB0;

        public ORARegB(Bus bus) : base(bus, "OR A, B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().B);

            return usedCycles;
        }
    }

    public class ORARegC : OrInstruction
    {
        public static new byte OpCode => 0xB1;

        public ORARegC(Bus bus) : base(bus, "OR A, C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().C);

            return usedCycles;
        }
    }

    public class ORARegD : OrInstruction
    {
        public static new byte OpCode => 0xB2;

        public ORARegD(Bus bus) : base(bus, "OR A, D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().D);

            return usedCycles;
        }
    }

    public class ORARegE : OrInstruction
    {
        public static new byte OpCode => 0xB3;

        public ORARegE(Bus bus) : base(bus, "OR A, E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().E);

            return usedCycles;
        }
    }

    public class ORARegH : OrInstruction
    {
        public static new byte OpCode => 0xB4;

        public ORARegH(Bus bus) : base(bus, "OR A, H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().H);

            return usedCycles;
        }
    }

    public class ORARegL : OrInstruction
    {
        public static new byte OpCode => 0xB5;

        public ORARegL(Bus bus) : base(bus, "OR A, L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Or(bus.GetCPU().A, bus.GetCPU().L);

            return usedCycles;
        }
    }

    public class ORAAddrHL : OrInstruction
    {
        public static new byte OpCode => 0xB6;

        public ORAAddrHL(Bus bus) : base(bus, "OR A, (HL)", 2)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Or(bus.GetCPU().A, bus.ReadMemory(address));

            usedCycles += 2;
            return usedCycles;
        }
    }

    public class ORAImpl : OrInstruction
    {
        public static new byte OpCode => 0xF6;

        protected byte value = 0;

        public ORAImpl(Bus bus) : base(bus, "OR A", 2)
        {
        }

        public override int Execute()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;
            usedCycles++;

            bus.GetCPU().A = Or(bus.GetCPU().A, data);
            usedCycles++;

            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
