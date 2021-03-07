using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Math
{
    public class INCA : SumInstruction
    {
        public INCA(Bus bus) : base(bus, 0x3C, "INC A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sum(bus.GetCPU().A, 1, true, false);

            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCB : SumInstruction
    {
        public INCB(Bus bus) : base(bus, 0x04, "INC B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = Sum(bus.GetCPU().B, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCC : SumInstruction
    {
        public INCC(Bus bus) : base(bus, 0x0C, "INC C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = Sum(bus.GetCPU().C, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCD : SumInstruction
    {
        public INCD(Bus bus) : base(bus, 0x14, "INC D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = Sum(bus.GetCPU().D, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCE : SumInstruction
    {
        public INCE(Bus bus) : base(bus, 0x1C, "INC E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = Sum(bus.GetCPU().E, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCH : SumInstruction
    {
        public INCH(Bus bus) : base(bus, 0x24, "INC H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = Sum(bus.GetCPU().H, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCL : SumInstruction
    {
        public INCL(Bus bus) : base(bus, 0x2C, "INC L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = Sum(bus.GetCPU().L, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCAddrHL : SumInstruction
    {
        public INCAddrHL(Bus bus) : base(bus, 0x34, "INC (HL)", 3)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.WriteMemory(Sum(bus.ReadMemory(address), 1, true, false), address);

            usedCycles += 2;
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class INCSP : Instruction
    {
        public INCSP(Bus bus) : base(bus, 0x33, "INC SP", 2)
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
        public INCBC(Bus bus) : base(bus, 0x03, "INC BC", 2)
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
        public INCDE(Bus bus) : base(bus, 0x13, "INC DE", 2)
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
        public INCHL(Bus bus) : base(bus, 0x23, "INC HL", 2)
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
