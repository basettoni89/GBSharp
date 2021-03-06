﻿using System;
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

    public class STAIndBC : StoreAIndirect
    {
        public static new byte OpCode => 0x02;

        public STAIndBC(Bus bus) : base(bus, "LD (BC), A")
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

    public class STAIndDE : StoreAIndirect
    {
        public static new byte OpCode => 0x12;

        public STAIndDE(Bus bus) : base(bus, "LD (DE), A")
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

    public class STAIndHLP : StoreAIndirect
    {
        public static new byte OpCode => 0x22;

        public STAIndHLP(Bus bus) : base(bus, "LD (HL+), A")
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

    public class STAIndHLM : StoreAIndirect
    {
        public static new byte OpCode => 0x32;

        public STAIndHLM(Bus bus) : base(bus, "LD (HL), A")
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

    public class STIndHL : StoreIndirect
    {
        public static new byte OpCode => 0x36;

        private byte value = 0;

        public STIndHL(Bus bus) : base(bus, "LD (HL)")
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

    public class STAInd : StoreAIndirect
    {
        public static new byte OpCode => 0xE0;

        private byte value = 0;

        public STAInd(Bus bus) : base(bus, "LD")
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
            return $"{Name} {value:X4}, A";
        }
    }

    public class STAIndC : StoreAIndirect
    {
        public static new byte OpCode => 0xE2;

        public STAIndC(Bus bus) : base(bus, "LD (C), A")
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

    public class STSPInd : Instruction
    {
        public static new byte OpCode => 0x08;

        private ushort address = 0;

        public STSPInd(Bus bus) : base(bus, "LD")
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
