using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
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
}
