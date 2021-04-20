using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public abstract class LDImplied : Instruction
    {
        protected byte value = 0;

        public LDImplied(Bus bus, string name) : base(bus, name)
        {
        }

        public override int Execute()
        {
            value = LoadImmediate();
            Load(value);

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }

        protected byte LoadImmediate()
        {
            byte data = bus.GetCPU().Fetch();
            return data;
        }

        protected abstract void Load(byte value);
    }

    public class LDAImpl : LDImplied
    {
        public static new byte OpCode => 0x3E;

        public LDAImpl(Bus bus) : base(bus, "LD A")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().A = value;
        }
    }

    public class LDBImpl : LDImplied
    {
        public static new byte OpCode => 0x06;

        public LDBImpl(Bus bus) : base(bus, "LD B")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().B = value;
        }
    }

    public class LDCImpl : LDImplied
    {
        public static new byte OpCode => 0x0E;

        public LDCImpl(Bus bus) : base(bus, "LD C")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().C = value;
        }
    }

    public class LDDImpl : LDImplied
    {
        public static new byte OpCode => 0x16;

        public LDDImpl(Bus bus) : base(bus, "LD D")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().D = value;
        }
    }

    public class LDEImpl : LDImplied
    {
        public static new byte OpCode => 0x1E;

        public LDEImpl(Bus bus) : base(bus, "LD E")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().E = value;
        }
    }

    public class LDHImpl : LDImplied
    {
        public static new byte OpCode => 0x26;

        public LDHImpl(Bus bus) : base(bus, "LD H")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().H = value;
        }
    }

    public class LDLImpl : LDImplied
    {
        public static new byte OpCode => 0x2E;

        public LDLImpl(Bus bus) : base(bus, "LD L")
        {
        }

        protected override void Load(byte value)
        {
            bus.GetCPU().L = value;
        }
    }
}
