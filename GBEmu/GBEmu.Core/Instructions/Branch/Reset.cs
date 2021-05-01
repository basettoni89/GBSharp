using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public abstract class ResetInstruction : JumpInstruction
    {
        protected ResetInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        public override int Execute()
        {
            bus.GetCPU().Push(bus.GetCPU().PC);

            byte lo = GetZeroPageAddress();
            JumpTo(lo);

            return 4;
        }

        public override string ToString() => Name;

        protected abstract byte GetZeroPageAddress();
    }

    public class RST0 : ResetInstruction
    {
        public static new byte OpCode => 0xC7;

        public RST0(Bus bus) : base(bus, "RST 0")
        {
        }

        protected override byte GetZeroPageAddress() => 0x00;
    }

    public class RST1 : ResetInstruction
    {
        public static new byte OpCode => 0xCF;

        public RST1(Bus bus) : base(bus, "RST 1")
        {
        }

        protected override byte GetZeroPageAddress() => 0x08;
    }

    public class RST2 : ResetInstruction
    {
        public static new byte OpCode => 0xD7;

        public RST2(Bus bus) : base(bus, "RST 2")
        {
        }

        protected override byte GetZeroPageAddress() => 0x10;
    }

    public class RST3 : ResetInstruction
    {
        public static new byte OpCode => 0xDF;

        public RST3(Bus bus) : base(bus, "RST 3")
        {
        }

        protected override byte GetZeroPageAddress() => 0x18;
    }

    public class RST4 : ResetInstruction
    {
        public static new byte OpCode => 0xE7;

        public RST4(Bus bus) : base(bus, "RST 4")
        {
        }

        protected override byte GetZeroPageAddress() => 0x20;
    }

    public class RST5 : ResetInstruction
    {
        public static new byte OpCode => 0xEF;

        public RST5(Bus bus) : base(bus, "RST 5")
        {
        }

        protected override byte GetZeroPageAddress() => 0x28;
    }

    public class RST6 : ResetInstruction
    {
        public static new byte OpCode => 0xF7;

        public RST6(Bus bus) : base(bus, "RST 6")
        {
        }

        protected override byte GetZeroPageAddress() => 0x30;
    }

    public class RST7 : ResetInstruction
    {
        public static new byte OpCode => 0xFF;

        public RST7(Bus bus) : base(bus, "RST 7")
        {
        }

        protected override byte GetZeroPageAddress() => 0x38;
    }
}
