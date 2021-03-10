using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class DECA : SubInstruction
    {
        public static new byte OpCode => 0x3D;

        public DECA(Bus bus) : base(bus, "DEC A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, 1, true, false);
            return usedCycles;
        }
    }

    public class DECB : SubInstruction
    {
        public static new byte OpCode => 0x05;

        public DECB(Bus bus) : base(bus, "DEC B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = Sub(bus.GetCPU().B, 1, true, false);
            return usedCycles;
        }
    }

    public class DECC : SubInstruction
    {
        public static new byte OpCode => 0x0D;

        public DECC(Bus bus) : base(bus, "DEC C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = Sub(bus.GetCPU().C, 1, true, false);
            return usedCycles;
        }
    }

    public class DECD : SubInstruction
    {
        public static new byte OpCode => 0x15;

        public DECD(Bus bus) : base(bus, "DEC D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = Sub(bus.GetCPU().D, 1, true, false);
            return usedCycles;
        }
    }

    public class DECE : SubInstruction
    {
        public static new byte OpCode => 0x1D;

        public DECE(Bus bus) : base(bus, "DEC E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = Sub(bus.GetCPU().E, 1, true, false);
            return usedCycles;
        }
    }

    public class DECH : SubInstruction
    {
        public static new byte OpCode => 0x25;

        public DECH(Bus bus) : base(bus, "DEC H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = Sub(bus.GetCPU().H, 1, true, false);
            return usedCycles;
        }
    }

    public class DECL : SubInstruction
    {
        public static new byte OpCode => 0x2D;

        public DECL(Bus bus) : base(bus, "DEC L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = Sub(bus.GetCPU().L, 1, true, false);
            return usedCycles;
        }
    }

    public class DECAddrHL : SubInstruction
    {
        public static new byte OpCode => 0x35;

        public DECAddrHL(Bus bus) : base(bus, "DEC (HL)", 3)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.WriteMemory(Sub(bus.ReadMemory(address), 1, true, false), address);

            usedCycles += 2;
            return usedCycles;
        }
    }
}
