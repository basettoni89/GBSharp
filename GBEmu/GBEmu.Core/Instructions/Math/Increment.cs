using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class INCA : SumInstruction
    {
        public static new byte OpCode => 0x3C;

        public INCA(Bus bus) : base(bus, "INC A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, 1, true, false);

            return 1;
        }
    }

    public class INCB : SumInstruction
    {
        public static new byte OpCode => 0x04;

        public INCB(Bus bus) : base(bus, "INC B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = Sum(bus.GetCPU().B, 1, true, false);
            return 1;
        }
    }

    public class INCC : SumInstruction
    {
        public static new byte OpCode => 0x0C;

        public INCC(Bus bus) : base(bus, "INC C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = Sum(bus.GetCPU().C, 1, true, false);
            return 1;
        }
    }

    public class INCD : SumInstruction
    {
        public static new byte OpCode => 0x14;

        public INCD(Bus bus) : base(bus, "INC D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = Sum(bus.GetCPU().D, 1, true, false);
            return 1;
        }
    }

    public class INCE : SumInstruction
    {
        public static new byte OpCode => 0x1C;

        public INCE(Bus bus) : base(bus, "INC E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = Sum(bus.GetCPU().E, 1, true, false);
            return 1;
        }
    }

    public class INCH : SumInstruction
    {
        public static new byte OpCode => 0x24;

        public INCH(Bus bus) : base(bus, "INC H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = Sum(bus.GetCPU().H, 1, true, false);
            return 1;
        }
    }

    public class INCL : SumInstruction
    {
        public static new byte OpCode => 0x2C;

        public INCL(Bus bus) : base(bus, "INC L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = Sum(bus.GetCPU().L, 1, true, false);
            return 1;
        }
    }

    public class INCAddrHL : SumInstruction
    {
        public static new byte OpCode => 0x34;

        public INCAddrHL(Bus bus) : base(bus, "INC (HL)")
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Sum(bus.ReadMemory(address), 1, true, false);

            return 3;
        }
    }

    public class INCSP : Instruction
    {
        public static new byte OpCode => 0x33;

        public INCSP(Bus bus) : base(bus, "INC SP")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().SP++;

            return 2;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCBC : Instruction
    {
        public static new byte OpCode => 0x03;

        public INCBC(Bus bus) : base(bus, "INC BC")
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().B << 8) + bus.GetCPU().C) + 1);

            bus.GetCPU().B = (byte)(value >> 8);
            bus.GetCPU().C = (byte)value;

            return 2;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCDE : Instruction
    {
        public static new byte OpCode => 0x13;

        public INCDE(Bus bus) : base(bus, "INC DE")
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().D << 8) + bus.GetCPU().E) + 1);

            bus.GetCPU().D = (byte)(value >> 8);
            bus.GetCPU().E = (byte)value;

            return 2;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCHL : Instruction
    {
        public static new byte OpCode => 0x23;

        public INCHL(Bus bus) : base(bus, "INC HL")
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().H << 8) + bus.GetCPU().L) + 1);

            bus.GetCPU().H = (byte)(value >> 8);
            bus.GetCPU().L = (byte)value;

            return 2;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
