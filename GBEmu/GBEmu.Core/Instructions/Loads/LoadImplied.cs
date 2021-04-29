using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public abstract class LD8bitImplied : Instruction
    {
        protected byte value = 0;

        public LD8bitImplied(Bus bus, string name) : base(bus, name)
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

    public abstract class LD16bitImplied : Instruction
    {
        protected ushort value = 0;

        public LD16bitImplied(Bus bus, string name) : base(bus, name)
        {
        }

        public override int Execute()
        {
            value = LoadImmediate();
            Load(value);

            return 3;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }

        protected ushort LoadImmediate()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            return CombineHILO(hi, lo);
        }

        protected abstract void Load(ushort value);
    }

    public class LDAImpl : LD8bitImplied
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

    public class LDBImpl : LD8bitImplied
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

    public class LDCImpl : LD8bitImplied
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

    public class LDDImpl : LD8bitImplied
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

    public class LDEImpl : LD8bitImplied
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

    public class LDHImpl : LD8bitImplied
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

    public class LDLImpl : LD8bitImplied
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

    public class LDBCImpl : LD16bitImplied
    {
        public static new byte OpCode => 0x01;

        public LDBCImpl(Bus bus) : base(bus, "LD BC")
        {
        }

        protected override void Load(ushort value)
        {
            bus.GetCPU().BC = value;
        }
    }

    public class LDDEImpl : LD16bitImplied
    {
        public static new byte OpCode => 0x11;

        public LDDEImpl(Bus bus) : base(bus, "LD DE")
        {
        }

        protected override void Load(ushort value)
        {
            bus.GetCPU().DE = value;
        }
    }

    public class LDHLImpl : LD16bitImplied
    {
        public static new byte OpCode => 0x21;

        public LDHLImpl(Bus bus) : base(bus, "LD HL")
        {
        }

        protected override void Load(ushort value)
        {
            bus.GetCPU().HL = value;
        }
    }

    public class LDSPImpl : LD16bitImplied
    {
        public static new byte OpCode => 0x31;

        public LDSPImpl(Bus bus) : base(bus, "LD SP")
        {
        }

        protected override void Load(ushort value)
        {
            bus.GetCPU().SP = value;
        }
    }
}
