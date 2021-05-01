using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class CPA : SubInstruction
    {
        public static new byte OpCode => 0xBF;

        public CPA(Bus bus) : base(bus, "CP A")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().A, true, true);
            return 1;
        }
    }

    public class CPAInd : SubInstruction
    {
        public static new byte OpCode => 0xFE;

        public CPAInd(Bus bus) : base(bus, "CP A, (d8)")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().Fetch(), true, true);
            return 2;
        }
    }

    public class CPB : SubInstruction
    {
        public static new byte OpCode => 0xB8;

        public CPB(Bus bus) : base(bus, "CP B")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().B, true, true);
            return 1;
        }
    }
    public class CPC : SubInstruction
    {
        public static new byte OpCode => 0xB9;

        public CPC(Bus bus) : base(bus, "CP C")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().C, true, true);
            return 1;
        }
    }
    public class CPD : SubInstruction
    {
        public static new byte OpCode => 0xBA;

        public CPD(Bus bus) : base(bus, "CP D")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().D, true, true);
            return 1;
        }
    }
    public class CPE : SubInstruction
    {
        public static new byte OpCode => 0xBB;

        public CPE(Bus bus) : base(bus, "CP E")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().E, true, true);
            return 1;
        }
    }
    public class CPH : SubInstruction
    {
        public static new byte OpCode => 0xBC;

        public CPH(Bus bus) : base(bus, "CP H")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().H, true, true);
            return 1;
        }
    }
    public class CPL : SubInstruction
    {
        public static new byte OpCode => 0xBD;

        public CPL(Bus bus) : base(bus, "CP L")
        {
        }

        public override int Execute()
        {
            Sub(bus.GetCPU().A, bus.GetCPU().L, true, true);
            return 1;
        }
    }
    public class CPAddrHL : SubInstruction
    {
        public static new byte OpCode => 0xBE;

        public CPAddrHL(Bus bus) : base(bus, "CP (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            Sub(bus.GetCPU().A, bus.ReadMemory(address), true, true);
            return 2;
        }
    }
}
