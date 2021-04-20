using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public abstract class JumpInstruction : Instruction
    {
        protected JumpInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        protected void JumpTo(ushort address)
        {
            bus.GetCPU().PC = address;
        }

        protected void JumpRelative(byte offset, int usedCycles)
        {
            bus.GetCPU().PC = (ushort)(bus.GetCPU().PC + offset - usedCycles);
        }
    }

    public class JPImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xC3;

        public JPImpl(Bus bus) : base(bus, "JP")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            JumpTo(value);

            return 4;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X8}";
        }
    }

    public class JPZImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xCA;

        public JPZImpl(Bus bus) : base(bus, "JP Z")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (bus.GetCPU().Flags.ZF)
            {
                JumpTo(value);
                return 4;
            }

            return 3;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X8}";
        }
    }

    public class JRImpl : JumpInstruction
    {
        private byte value;

        public static new byte OpCode => 0x18;

        public JRImpl(Bus bus) : base(bus, "JR")
        {
        }

        public override int Execute()
        {
            value = bus.GetCPU().Fetch();

            JumpRelative(value, 2);

            return 3;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
