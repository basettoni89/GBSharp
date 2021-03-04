using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core
{
    public abstract class Instruction
    {
        protected readonly Bus bus;
        protected int usedCycles;

        public byte OpCode { get; }

        public string Name { get; }

        public byte Cycles { get; }

        protected Instruction(Bus bus, byte opCode, string name, byte cycles)
        {
            this.bus = bus;

            OpCode = opCode;
            Name = name;
            Cycles = cycles;

            usedCycles = 0;
        }

        public abstract override string ToString();

        public abstract int Execute();

        protected ushort CombineHILO(byte hi, byte lo)
        {
            return (ushort)((hi << 8) + lo);
        }
    }

    public abstract class LDImplied : Instruction
    {
        protected byte value = 0;

        public LDImplied(Bus bus, byte opCode, string name) : base(bus, opCode, name, 2)
        {
        }

        public override int Execute()
        {
            value = LoadImmediate();
            Load(value);
            return usedCycles;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }

        protected byte LoadImmediate()
        {
            byte data = bus.ReadMemory(bus.GetCPU().PC);
            bus.GetCPU().PC++;
            usedCycles++;
            return data;
        }

        protected abstract void Load(byte value);
    }

    public abstract class LDIndirect : Instruction
    {
        public LDIndirect(Bus bus, byte opCode, string name) : base(bus, opCode, name, 2)
        {
        }

        public override int Execute()
        {
            UInt16 address = GetAddress();
            byte value = bus.ReadMemory(address);
            Load(value);
            return usedCycles;
        }

        protected abstract UInt16 GetAddress();

        protected abstract void Load(byte value);
    }

    public abstract class LDIndiretHL : LDIndirect
    {
        public LDIndiretHL(Bus bus, byte opCode, string name) : base(bus, opCode, name)
        {
        }

        protected override UInt16 GetAddress()
        {
            return (UInt16)(bus.GetCPU().L | (bus.GetCPU().H << 8));
        }

        public override string ToString()
        {
            return $"{Name}, (HL)";
        }
    }

    public abstract class SumInstruction : Instruction
    {
        protected SumInstruction(Bus bus, byte opCode, string name, byte cycles) : base(bus, opCode, name, cycles)
        {
        }

        protected byte Sum(byte a, byte b, bool halfCarry, bool carry)
        {
            ushort r = (ushort)(a + b);

            bus.GetCPU().Flags.ZF = (byte)r == 0;
            bus.GetCPU().Flags.N = false;

            if (halfCarry)
                bus.GetCPU().Flags.H = (((a & 0b1111) + (b & 0b1111)) & (1 << 4)) != 0;

            if (carry)
                bus.GetCPU().Flags.CY = (r & (1 << 8)) != 0;

            return (byte)r;
        }
    }

    public abstract class SubInstruction : Instruction
    {
        protected SubInstruction(Bus bus, byte opCode, string name, byte cycles) : base(bus, opCode, name, cycles)
        {
        }

        protected byte Sub(byte a, byte b, bool halfCarry, bool carry)
        {
            ushort r = (ushort)(a - b);

            bus.GetCPU().Flags.ZF = (byte)r == 0;
            bus.GetCPU().Flags.N = true;

            if (halfCarry)
                bus.GetCPU().Flags.H = (((a & 0b1111) - (b & 0b1111)) & (1 << 4)) != 0;

            if (carry)
                bus.GetCPU().Flags.CY = (r & (1 << 8)) != 0;

            return (byte)r;
        }
    }
    public class NOP : Instruction
    {
        public NOP(Bus bus) : base(bus, 0x00, "NOP", 1)
        {
        }

        public override int Execute()
        {
            return usedCycles;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDAImpl : LDImplied
    {
        public LDAImpl(Bus bus) : base(bus, 0x3E, "LD A")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }
    }

    public class LDBImpl : LDImplied
    {
        public LDBImpl(Bus bus) : base(bus, 0x06, "LD B")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().B = value;
        }
    }

    public class LDCImpl : LDImplied
    {
        public LDCImpl(Bus bus) : base(bus, 0x0E, "LD C")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().C = value;
        }
    }

    public class LDDImpl : LDImplied
    {
        public LDDImpl(Bus bus) : base(bus, 0x16, "LD D")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().D = value;
        }
    }

    public class LDEImpl : LDImplied
    {

        public LDEImpl(Bus bus) : base(bus, 0x1E, "LD E")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().E = value;
        }
    }

    public class LDHImpl : LDImplied
    {
        public LDHImpl(Bus bus) : base(bus, 0x26, "LD H")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().H = value;
        }
    }

    public class LDLImpl : LDImplied
    {
        public LDLImpl(Bus bus) : base(bus, 0x1E, "LD L")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().L = value;
        }
    }

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

    public class LDAIndHL : LDIndiretHL
    {
        public LDAIndHL(Bus bus) : base(bus, 0x7E, "LD A")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }
    }

    public class LDBIndHL : LDIndiretHL
    {
        public LDBIndHL(Bus bus) : base(bus, 0x46, "LD B")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().B = value;
        }
    }

    public class LDCIndHL : LDIndiretHL
    {
        public LDCIndHL(Bus bus) : base(bus, 0x4E, "LD C")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().C = value;
        }
    }

    public class LDDIndHL : LDIndiretHL
    {
        public LDDIndHL(Bus bus) : base(bus, 0x56, "LD D")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().D = value;
        }
    }

    public class LDEIndHL : LDIndiretHL
    {
        public LDEIndHL(Bus bus) : base(bus, 0x5E, "LD E")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().E = value;
        }
    }

    public class LDHIndHL : LDIndiretHL
    {
        public LDHIndHL(Bus bus) : base(bus, 0x66, "LD H")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().H = value;
        }
    }

    public class LDLIndHL : LDIndiretHL
    {
        public LDLIndHL(Bus bus) : base(bus, 0x6E, "LD L")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().L = value;
        }
    }

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

    public class DECA : SubInstruction
    {
        public DECA(Bus bus) : base(bus, 0x3D, "DEC A", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().A = Sub(bus.GetCPU().A, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECB : SubInstruction
    {
        public DECB(Bus bus) : base(bus, 0x05, "DEC B", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().B = Sub(bus.GetCPU().B, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECC : SubInstruction
    {
        public DECC(Bus bus) : base(bus, 0x0D, "DEC C", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().C = Sub(bus.GetCPU().C, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECD : SubInstruction
    {
        public DECD(Bus bus) : base(bus, 0x15, "DEC D", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().D = Sub(bus.GetCPU().D, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECE : SubInstruction
    {
        public DECE(Bus bus) : base(bus, 0x1D, "DEC E", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().E = Sub(bus.GetCPU().E, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECH : SubInstruction
    {
        public DECH(Bus bus) : base(bus, 0x2D, "DEC H", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().H = Sub(bus.GetCPU().H, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECL : SubInstruction
    {
        public DECL(Bus bus) : base(bus, 0x35, "DEC L", 1)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().L = Sub(bus.GetCPU().L, 1, true, false);
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class DECAddrHL : SubInstruction
    {
        public DECAddrHL(Bus bus) : base(bus, 0x35, "DEC (HL)", 3)
        {
        }

        public override int Execute()
        {
            ushort address = CombineHILO(bus.GetCPU().H, bus.GetCPU().L);

            bus.WriteMemory(Sub(bus.ReadMemory(address), 1, true, false), address);

            usedCycles += 2;
            return usedCycles;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
