using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public class LDARegA : Instruction
    {


        public LDARegA(Bus bus) : base(bus, 0x7F, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDARegB : Instruction
    {


        public LDARegB(Bus bus) : base(bus, 0x78, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDARegC : Instruction
    {


        public LDARegC(Bus bus) : base(bus, 0x79, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDARegD : Instruction
    {


        public LDARegD(Bus bus) : base(bus, 0x7A, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDARegE : Instruction
    {


        public LDARegE(Bus bus) : base(bus, 0x7B, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDARegH : Instruction
    {


        public LDARegH(Bus bus) : base(bus, 0x7C, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDARegL : Instruction
    {


        public LDARegL(Bus bus) : base(bus, 0x7D, "LD A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDBRegA : Instruction
    {


        public LDBRegA(Bus bus) : base(bus, 0x47, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDBRegB : Instruction
    {


        public LDBRegB(Bus bus) : base(bus, 0x40, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDBRegC : Instruction
    {


        public LDBRegC(Bus bus) : base(bus, 0x41, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDBRegD : Instruction
    {


        public LDBRegD(Bus bus) : base(bus, 0x41, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDBRegE : Instruction
    {


        public LDBRegE(Bus bus) : base(bus, 0x43, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDBRegH : Instruction
    {


        public LDBRegH(Bus bus) : base(bus, 0x44, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDBRegL : Instruction
    {


        public LDBRegL(Bus bus) : base(bus, 0x45, "LD B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDCRegA : Instruction
    {


        public LDCRegA(Bus bus) : base(bus, 0x4F, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDCRegB : Instruction
    {


        public LDCRegB(Bus bus) : base(bus, 0x48, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDCRegC : Instruction
    {


        public LDCRegC(Bus bus) : base(bus, 0x49, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDCRegD : Instruction
    {


        public LDCRegD(Bus bus) : base(bus, 0x4A, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDCRegE : Instruction
    {


        public LDCRegE(Bus bus) : base(bus, 0x4B, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDCRegH : Instruction
    {


        public LDCRegH(Bus bus) : base(bus, 0x4C, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDCRegL : Instruction
    {


        public LDCRegL(Bus bus) : base(bus, 0x4D, "LD C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDDRegA : Instruction
    {


        public LDDRegA(Bus bus) : base(bus, 0x57, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDDRegB : Instruction
    {


        public LDDRegB(Bus bus) : base(bus, 0x50, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDDRegC : Instruction
    {


        public LDDRegC(Bus bus) : base(bus, 0x51, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDDRegD : Instruction
    {


        public LDDRegD(Bus bus) : base(bus, 0x51, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDDRegE : Instruction
    {


        public LDDRegE(Bus bus) : base(bus, 0x53, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDDRegH : Instruction
    {


        public LDDRegH(Bus bus) : base(bus, 0x54, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDDRegL : Instruction
    {


        public LDDRegL(Bus bus) : base(bus, 0x55, "LD D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDERegA : Instruction
    {


        public LDERegA(Bus bus) : base(bus, 0x5F, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDERegB : Instruction
    {


        public LDERegB(Bus bus) : base(bus, 0x58, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDERegC : Instruction
    {


        public LDERegC(Bus bus) : base(bus, 0x59, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDERegD : Instruction
    {


        public LDERegD(Bus bus) : base(bus, 0x5A, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDERegE : Instruction
    {


        public LDERegE(Bus bus) : base(bus, 0x5B, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDERegH : Instruction
    {


        public LDERegH(Bus bus) : base(bus, 0x5C, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDERegL : Instruction
    {


        public LDERegL(Bus bus) : base(bus, 0x5D, "LD E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDHRegA : Instruction
    {


        public LDHRegA(Bus bus) : base(bus, 0x67, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDHRegB : Instruction
    {


        public LDHRegB(Bus bus) : base(bus, 0x60, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDHRegC : Instruction
    {


        public LDHRegC(Bus bus) : base(bus, 0x61, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDHRegD : Instruction
    {


        public LDHRegD(Bus bus) : base(bus, 0x61, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDHRegE : Instruction
    {


        public LDHRegE(Bus bus) : base(bus, 0x63, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDHRegH : Instruction
    {


        public LDHRegH(Bus bus) : base(bus, 0x64, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDHRegL : Instruction
    {


        public LDHRegL(Bus bus) : base(bus, 0x65, "LD H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDLRegA : Instruction
    {


        public LDLRegA(Bus bus) : base(bus, 0x6F, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().A;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDLRegB : Instruction
    {


        public LDLRegB(Bus bus) : base(bus, 0x68, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().B;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDLRegC : Instruction
    {


        public LDLRegC(Bus bus) : base(bus, 0x69, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().C;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDLRegD : Instruction
    {


        public LDLRegD(Bus bus) : base(bus, 0x6A, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().D;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDLRegE : Instruction
    {


        public LDLRegE(Bus bus) : base(bus, 0x6B, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().E;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDLRegH : Instruction
    {


        public LDLRegH(Bus bus) : base(bus, 0x6C, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().H;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDLRegL : Instruction
    {


        public LDLRegL(Bus bus) : base(bus, 0x6D, "LD L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().L;
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }
}
