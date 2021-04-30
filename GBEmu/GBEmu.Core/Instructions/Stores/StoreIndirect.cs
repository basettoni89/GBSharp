using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Stores
{
    public abstract class StoreIndirect : Instruction
    {
        protected StoreIndirect(Bus bus, string name) : base(bus, name)
        {
        }

        public override int Execute()
        {
            ushort addr = GetAddress();
            byte value = GetValue();

            bus.WriteMemory(value, addr);

            return GetCycles();
        }

        protected abstract ushort GetAddress();

        protected abstract byte GetValue();

        protected abstract int GetCycles();
    }

    public abstract class StoreAIndirect : StoreIndirect
    {
        protected StoreAIndirect(Bus bus, string name) : base(bus, name)
        {
        }

        protected override byte GetValue()
        {
            return bus.GetCPU().A;
        }

        protected override int GetCycles() => 2;
    }

    public class LDAIndBC : StoreAIndirect
    {
        public static new byte OpCode => 0x02;

        public LDAIndBC(Bus bus) : base(bus, "LD (BC), A")
        {
        }

        protected override ushort GetAddress()
        {
            return bus.GetCPU().BC;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDAIndDE : StoreAIndirect
    {
        public static new byte OpCode => 0x12;

        public LDAIndDE(Bus bus) : base(bus, "LD (DE), A")
        {
        }

        protected override ushort GetAddress()
        {
            return bus.GetCPU().DE;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDAIndHLP : StoreAIndirect
    {
        public static new byte OpCode => 0x22;

        public LDAIndHLP(Bus bus) : base(bus, "LD (HL+), A")
        {
        }

        protected override ushort GetAddress()
        {
            return bus.GetCPU().HL++;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDAIndHLM : StoreAIndirect
    {
        public static new byte OpCode => 0x32;

        public LDAIndHLM(Bus bus) : base(bus, "LD (HL), A")
        {
        }

        protected override ushort GetAddress()
        {
            return bus.GetCPU().HL--;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDIndHL : StoreIndirect
    {
        public static new byte OpCode => 0x36;

        private byte value = 0;

        public LDIndHL(Bus bus) : base(bus, "LD (HL)")
        {
        }

        protected override ushort GetAddress()
        {
            return bus.GetCPU().HL;
        }

        protected override byte GetValue()
        {
            value = bus.GetCPU().Fetch();
            return value;
        }

        protected override int GetCycles() => 3;

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }

    public class LDAInd : StoreAIndirect
    {
        public static new byte OpCode => 0xE0;

        private byte value = 0;

        public LDAInd(Bus bus) : base(bus, "LD")
        {
        }

        protected override ushort GetAddress()
        {
            value = bus.GetCPU().Fetch();
            return (ushort)(0xFF00 | value);
        }

        protected override int GetCycles() => 3;

        public override string ToString()
        {
            return $"{Name} {value}, A";
        }
    }

    public class LDAIndC : StoreAIndirect
    {
        public static new byte OpCode => 0xE2;

        public LDAIndC(Bus bus) : base(bus, "LD (C), A")
        {
        }

        protected override ushort GetAddress()
        {
            return (ushort)(0xFF00 | bus.GetCPU().C);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class LDSPInd : Instruction
    {
        public static new byte OpCode => 0x08;

        private ushort address = 0;

        public LDSPInd(Bus bus) : base(bus, "LD")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            address = CombineHILO(hi, lo);

            bus.WriteMemory((byte)bus.GetCPU().SP, address);
            bus.WriteMemory((byte)(bus.GetCPU().SP >> 8), (ushort)(address + 1));

            return 5;
        }

        public override string ToString()
        {
            return $"{Name} {address:X4}, SP";
        }
    }
}
