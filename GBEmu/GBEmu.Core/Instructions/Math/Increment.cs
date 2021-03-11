using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class INCA : SumInstruction
    {
        public static new byte OpCode => 0x3C;

        public INCA(Bus bus) : base(bus, "INC A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, 1, true, false);

            return usedCycles;
        }
    }

    public class INCB : SumInstruction
    {
        public static new byte OpCode => 0x04;

        public INCB(Bus bus) : base(bus, "INC B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = Sum(bus.GetCPU().B, 1, true, false);
            return usedCycles;
        }
    }

    public class INCC : SumInstruction
    {
        public static new byte OpCode => 0x0C;

        public INCC(Bus bus) : base(bus, "INC C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = Sum(bus.GetCPU().C, 1, true, false);
            return usedCycles;
        }
    }

    public class INCD : SumInstruction
    {
        public static new byte OpCode => 0x14;

        public INCD(Bus bus) : base(bus, "INC D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = Sum(bus.GetCPU().D, 1, true, false);
            return usedCycles;
        }
    }

    public class INCE : SumInstruction
    {
        public static new byte OpCode => 0x1C;

        public INCE(Bus bus) : base(bus, "INC E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = Sum(bus.GetCPU().E, 1, true, false);
            return usedCycles;
        }
    }

    public class INCH : SumInstruction
    {
        public static new byte OpCode => 0x24;

        public INCH(Bus bus) : base(bus, "INC H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = Sum(bus.GetCPU().H, 1, true, false);
            return usedCycles;
        }
    }

    public class INCL : SumInstruction
    {
        public static new byte OpCode => 0x2C;

        public INCL(Bus bus) : base(bus, "INC L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = Sum(bus.GetCPU().L, 1, true, false);
            return usedCycles;
        }
    }

    public class INCAddrHL : SumInstruction
    {
        public static new byte OpCode => 0x34;

        public INCAddrHL(Bus bus) : base(bus, "INC (HL)", 3)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.GetCPU().A = Sum(bus.ReadMemory(address), 1, true, false);

            usedCycles += 2;
            return usedCycles;
        }
    }

    public class INCSP : Instruction
    {
        public static new byte OpCode => 0x33;

        public INCSP(Bus bus) : base(bus, "INC SP", 2)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().SP++;

            return usedCycles++;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCBC : Instruction
    {
        public static new byte OpCode => 0x03;

        public INCBC(Bus bus) : base(bus, "INC BC", 2)
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().B << 8) + bus.GetCPU().C) + 1);

            bus.GetCPU().B = (byte)(value >> 8);
            bus.GetCPU().C = (byte)value;

            return usedCycles++;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCDE : Instruction
    {
        public static new byte OpCode => 0x13;

        public INCDE(Bus bus) : base(bus, "INC DE", 2)
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().D << 8) + bus.GetCPU().E) + 1);

            bus.GetCPU().D = (byte)(value >> 8);
            bus.GetCPU().E = (byte)value;

            return usedCycles++;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCHL : Instruction
    {
        public static new byte OpCode => 0x23;

        public INCHL(Bus bus) : base(bus, "INC HL", 2)
        {
        }

        public override int Execute()
        {
            ushort value = (ushort)(((bus.GetCPU().H << 8) + bus.GetCPU().L) + 1);

            bus.GetCPU().H = (byte)(value >> 8);
            bus.GetCPU().L = (byte)value;

            return usedCycles++;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
