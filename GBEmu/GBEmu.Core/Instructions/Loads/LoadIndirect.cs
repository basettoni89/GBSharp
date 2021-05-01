using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public abstract class LDIndirect : Instruction
    {
        public LDIndirect(Bus bus, string name) : base(bus, name)
        {
        }

        public override int Execute()
        {
            ushort address = GetAddress();
            byte value = bus.ReadMemory(address);
            Load(value);

            return GetCycles();
        }

        protected abstract ushort GetAddress();

        protected abstract void Load(byte value);

        protected abstract int GetCycles();
    }

    public abstract class LDIndiretHL : LDIndirect
    {
        public LDIndiretHL(Bus bus, string name) : base(bus, name)
        {
        }

        protected override ushort GetAddress()
        {
            return CombineHILO(bus.GetCPU().H, bus.GetCPU().L);
        }

        public override string ToString()
        {
            return $"{Name}, (HL)";
        }

        protected override int GetCycles() => 2;
    }

    public class LDAIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x7E;

        public LDAIndHL(Bus bus) : base(bus, "LD A")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }
    }

    public class LDBIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x46;

        public LDBIndHL(Bus bus) : base(bus, "LD B")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().B = value;
        }
    }

    public class LDCIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x4E;

        public LDCIndHL(Bus bus) : base(bus, "LD C")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().C = value;
        }
    }

    public class LDDIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x56;

        public LDDIndHL(Bus bus) : base(bus, "LD D")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().D = value;
        }
    }

    public class LDEIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x5E;

        public LDEIndHL(Bus bus) : base(bus, "LD E")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().E = value;
        }
    }

    public class LDHIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x66;

        public LDHIndHL(Bus bus) : base(bus, "LD H")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().H = value;
        }
    }

    public class LDLIndHL : LDIndiretHL
    {
        public static new byte OpCode => 0x6E;

        public LDLIndHL(Bus bus) : base(bus, "LD L")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().L = value;
        }
    }

    public class LDAInd : LDIndirect
    {
        public static new byte OpCode => 0xF0;

        public LDAInd(Bus bus) : base(bus, "LD A, (a8)")
        {
        }

        protected override ushort GetAddress()
        {
            byte lo = bus.GetCPU().Fetch();
            return (ushort)(0xFF00 | lo);
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }

        protected override int GetCycles() => 3;

        public override string ToString() => Name;
    }

    public class LDAIndC : LDIndirect
    {
        public static new byte OpCode => 0xF2;

        public LDAIndC(Bus bus) : base(bus, "LD A, (C)")
        {
        }

        protected override ushort GetAddress()
        {
            return (ushort)(0xFF00 | bus.GetCPU().C);
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }

        protected override int GetCycles() => 2;

        public override string ToString() => Name;
    }
}
