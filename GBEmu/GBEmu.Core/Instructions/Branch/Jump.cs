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

    public class JPHLImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xE9;

        public JPHLImpl(Bus bus) : base(bus, "JP HL")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().L;
            byte hi = bus.GetCPU().H;

            value = CombineHILO(hi, lo);

            JumpTo(value);

            return 1;
        }

        public override string ToString()
        {
            return Name;
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

    public class JPCImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xDA;

        public JPCImpl(Bus bus) : base(bus, "JP C")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (bus.GetCPU().Flags.CY)
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

    public class JPNZImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xC2;

        public JPNZImpl(Bus bus) : base(bus, "JP NZ")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (!bus.GetCPU().Flags.ZF)
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

    public class JPNCImpl : JumpInstruction
    {
        private ushort value;

        public static new byte OpCode => 0xD2;

        public JPNCImpl(Bus bus) : base(bus, "JP NC")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (!bus.GetCPU().Flags.CY)
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

    public class JRZImpl : JumpInstruction
    {
        private byte value;

        public static new byte OpCode => 0x28;

        public JRZImpl(Bus bus) : base(bus, "JR Z")
        {
        }

        public override int Execute()
        {
            value = bus.GetCPU().Fetch();

            if(bus.GetCPU().Flags.ZF)
            {
                JumpRelative(value, 2);
                return 3;
            }

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }

    public class JRCImpl : JumpInstruction
    {
        private byte value;

        public static new byte OpCode => 0x38;

        public JRCImpl(Bus bus) : base(bus, "JR C")
        {
        }

        public override int Execute()
        {
            value = bus.GetCPU().Fetch();

            if(bus.GetCPU().Flags.CY)
            {
                JumpRelative(value, 2);
                return 3;
            }

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }

    public class JRNZImpl : JumpInstruction
    {
        private byte value;

        public static new byte OpCode => 0x20;

        public JRNZImpl(Bus bus) : base(bus, "JR NZ")
        {
        }

        public override int Execute()
        {
            value = bus.GetCPU().Fetch();

            if (!bus.GetCPU().Flags.ZF)
            {
                JumpRelative(value, 2);
                return 3;
            }

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }

    public class JRNCImpl : JumpInstruction
    {
        private byte value;

        public static new byte OpCode => 0x30;

        public JRNCImpl(Bus bus) : base(bus, "JR NC")
        {
        }

        public override int Execute()
        {
            value = bus.GetCPU().Fetch();

            if (!bus.GetCPU().Flags.CY)
            {
                JumpRelative(value, 2);
                return 3;
            }

            return 2;
        }

        public override string ToString()
        {
            return $"{Name}, {value:X4}";
        }
    }
}
