﻿using System;
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

            return 2;
        }

        protected abstract ushort GetAddress();

        protected abstract void Load(byte value);
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
