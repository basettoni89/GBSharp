using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public class LDARegA : Instruction
    {
        public static new byte OpCode => 0x7F;

        public LDARegA(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDARegB : Instruction
    {
        public static new byte OpCode => 0x78;

        public LDARegB(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDARegC : Instruction
    {
        public static new byte OpCode => 0x79;

        public LDARegC(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDARegD : Instruction
    {
        public static new byte OpCode => 0x7A;

        public LDARegD(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDARegE : Instruction
    {
        public static new byte OpCode => 0x7B;

        public LDARegE(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDARegH : Instruction
    {
        public static new byte OpCode => 0x7C;

        public LDARegH(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDARegL : Instruction
    {
        public static new byte OpCode => 0x7D;

        public LDARegL(Bus bus) : base(bus, "LD A")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDBRegA : Instruction
    {
        public static new byte OpCode => 0x47;

        public LDBRegA(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDBRegB : Instruction
    {
        public static new byte OpCode => 0x40;

        public LDBRegB(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDBRegC : Instruction
    {
        public static new byte OpCode => 0x41;

        public LDBRegC(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDBRegD : Instruction
    {
        public static new byte OpCode => 0x42;

        public LDBRegD(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDBRegE : Instruction
    {
        public static new byte OpCode => 0x43;

        public LDBRegE(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDBRegH : Instruction
    {
        public static new byte OpCode => 0x44;

        public LDBRegH(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDBRegL : Instruction
    {
        public static new byte OpCode => 0x45;

        public LDBRegL(Bus bus) : base(bus, "LD B")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDCRegA : Instruction
    {
        public static new byte OpCode => 0x4F;

        public LDCRegA(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDCRegB : Instruction
    {
        public static new byte OpCode => 0x48;

        public LDCRegB(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDCRegC : Instruction
    {
        public static new byte OpCode => 0x49;

        public LDCRegC(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDCRegD : Instruction
    {
        public static new byte OpCode => 0x4A;

        public LDCRegD(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDCRegE : Instruction
    {
        public static new byte OpCode => 0x4B;

        public LDCRegE(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDCRegH : Instruction
    {
        public static new byte OpCode => 0x4C;

        public LDCRegH(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDCRegL : Instruction
    {
        public static new byte OpCode => 0x4D;

        public LDCRegL(Bus bus) : base(bus, "LD C")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDDRegA : Instruction
    {
        public static new byte OpCode => 0x57;

        public LDDRegA(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDDRegB : Instruction
    {
        public static new byte OpCode => 0x50;

        public LDDRegB(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDDRegC : Instruction
    {
        public static new byte OpCode => 0x51;

        public LDDRegC(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDDRegD : Instruction
    {
        public static new byte OpCode => 0x52;

        public LDDRegD(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDDRegE : Instruction
    {
        public static new byte OpCode => 0x53;

        public LDDRegE(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDDRegH : Instruction
    {
        public static new byte OpCode => 0x54;

        public LDDRegH(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDDRegL : Instruction
    {
        public static new byte OpCode => 0x55;

        public LDDRegL(Bus bus) : base(bus, "LD D")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDERegA : Instruction
    {
        public static new byte OpCode => 0x5F;

        public LDERegA(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDERegB : Instruction
    {
        public static new byte OpCode => 0x58;

        public LDERegB(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDERegC : Instruction
    {
        public static new byte OpCode => 0x59;

        public LDERegC(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDERegD : Instruction
    {
        public static new byte OpCode => 0x5A;

        public LDERegD(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDERegE : Instruction
    {
        public static new byte OpCode => 0x5B;

        public LDERegE(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDERegH : Instruction
    {
        public static new byte OpCode => 0x5C;

        public LDERegH(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDERegL : Instruction
    {
        public static new byte OpCode => 0x5D;

        public LDERegL(Bus bus) : base(bus, "LD E")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDHRegA : Instruction
    {
        public static new byte OpCode => 0x67;

        public LDHRegA(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDHRegB : Instruction
    {
        public static new byte OpCode => 0x60;

        public LDHRegB(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDHRegC : Instruction
    {
        public static new byte OpCode => 0x61;

        public LDHRegC(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDHRegD : Instruction
    {
        public static new byte OpCode => 0x62;

        public LDHRegD(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDHRegE : Instruction
    {
        public static new byte OpCode => 0x63;

        public LDHRegE(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDHRegH : Instruction
    {
        public static new byte OpCode => 0x64;

        public LDHRegH(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDHRegL : Instruction
    {
        public static new byte OpCode => 0x65;

        public LDHRegL(Bus bus) : base(bus, "LD H")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }

    public class LDLRegA : Instruction
    {
        public static new byte OpCode => 0x6F;

        public LDLRegA(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().A;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, A";
        }
    }

    public class LDLRegB : Instruction
    {
        public static new byte OpCode => 0x68;

        public LDLRegB(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().B;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, B";
        }
    }

    public class LDLRegC : Instruction
    {
        public static new byte OpCode => 0x69;

        public LDLRegC(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().C;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, C";
        }
    }

    public class LDLRegD : Instruction
    {
        public static new byte OpCode => 0x6A;

        public LDLRegD(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().D;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, D";
        }
    }

    public class LDLRegE : Instruction
    {
        public static new byte OpCode => 0x6B;

        public LDLRegE(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().E;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, E";
        }
    }

    public class LDLRegH : Instruction
    {
        public static new byte OpCode => 0x6C;

        public LDLRegH(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().H;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, H";
        }
    }

    public class LDLRegL : Instruction
    {
        public static new byte OpCode => 0x6D;

        public LDLRegL(Bus bus) : base(bus, "LD L")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = bus.GetCPU().L;
            return 1;
        }

        public override string ToString()
        {
            return $"{Name}, L";
        }
    }
}
