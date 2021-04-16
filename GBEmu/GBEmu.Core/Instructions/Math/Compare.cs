using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class CPA : SubInstruction
    {
        public static new byte OpCode => 0xBF;

        public CPA(Bus bus) : base(bus, "CP A", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().A, true, true);
            return usedCycles;
        }
    }
    public class CPB : SubInstruction
    {
        public static new byte OpCode => 0xB8;

        public CPB(Bus bus) : base(bus, "CP B", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().B, true, true);
            return usedCycles;
        }
    }
    public class CPC : SubInstruction
    {
        public static new byte OpCode => 0xB9;

        public CPC(Bus bus) : base(bus, "CP C", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().C, true, true);
            return usedCycles;
        }
    }
    public class CPD : SubInstruction
    {
        public static new byte OpCode => 0xBA;

        public CPD(Bus bus) : base(bus, "CP D", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().D, true, true);
            return usedCycles;
        }
    }
    public class CPE : SubInstruction
    {
        public static new byte OpCode => 0xBB;

        public CPE(Bus bus) : base(bus, "CP E", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().E, true, true);
            return usedCycles;
        }
    }
    public class CPF : SubInstruction
    {
        public static new byte OpCode => 0xBC;

        public CPF(Bus bus) : base(bus, "CP F", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().F, true, true);
            return usedCycles;
        }
    }
    public class CPH : SubInstruction
    {
        public static new byte OpCode => 0xBD;

        public CPH(Bus bus) : base(bus, "CP B", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().H, true, true);
            return usedCycles;
        }
    }
    public class CPL : SubInstruction
    {
        public static new byte OpCode => 0xBE;

        public CPL(Bus bus) : base(bus, "CP L", 1)
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().L, true, true);
            return usedCycles;
        }
    }
}
