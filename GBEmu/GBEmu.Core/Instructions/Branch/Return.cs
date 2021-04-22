using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public abstract class ReturnInstruction : Instruction
    {
        public ReturnInstruction(Bus bus, string name) : base(bus, name)
        {
        }

        protected void Return()
        {
            byte lo = bus.GetCPU().Pop();
            byte hi = bus.GetCPU().Pop();

            bus.GetCPU().PC = CombineHILO(hi, lo);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class RET : ReturnInstruction
    {
        public static new byte OpCode => 0xC9;

        public RET(Bus bus) : base(bus, "RET")
        {
        }

        public override int Execute()
        {
            Return();
            return 4;
        }
    }

    public class RETZ : ReturnInstruction
    {
        public static new byte OpCode => 0xC8;

        public RETZ(Bus bus) : base(bus, "RET Z")
        {
        }

        public override int Execute()
        {
            if(bus.GetCPU().Flags.ZF)
            {
                Return();
                return 5;
            }

            return 2;
        }
    }

    public class RETC : ReturnInstruction
    {
        public static new byte OpCode => 0xD8;

        public RETC(Bus bus) : base(bus, "RET C")
        {
        }

        public override int Execute()
        {
            if (bus.GetCPU().Flags.CY)
            {
                Return();
                return 5;
            }

            return 2;
        }
    }

    public class RETNZ : ReturnInstruction
    {
        public static new byte OpCode => 0xC0;

        public RETNZ(Bus bus) : base(bus, "RET NZ")
        {
        }

        public override int Execute()
        {
            if (!bus.GetCPU().Flags.ZF)
            {
                Return();
                return 5;
            }

            return 2;
        }
    }

    public class RETNC : ReturnInstruction
    {
        public static new byte OpCode => 0xD0;

        public RETNC(Bus bus) : base(bus, "RET NC")
        {
        }

        public override int Execute()
        {
            if (!bus.GetCPU().Flags.CY)
            {
                Return();
                return 5;
            }

            return 2;
        }
    }
}
