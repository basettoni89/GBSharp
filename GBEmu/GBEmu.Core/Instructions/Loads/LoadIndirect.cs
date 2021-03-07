using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
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
}
