using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public abstract class CallInstruction : JumpInstruction
    {
        protected ushort value;

        protected CallInstruction(Bus bus, string name) : base(bus, name)
        {
        }
        protected void Call(ushort address)
        {
            bus.GetCPU().Push(bus.GetCPU().PC);
            JumpTo(address);
        }

        public override string ToString()
        {
            return $"{Name}, {value:X8}";
        }
    }

    public class CALLImpl : CallInstruction
    {
        public static new byte OpCode => 0xCD;

        public CALLImpl(Bus bus) : base(bus, "CALL")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            Call(value);

            return 6;
        }
    }

    public class CALLZImpl : CallInstruction
    {
        public static new byte OpCode => 0xCC;

        public CALLZImpl(Bus bus) : base(bus, "CALL Z")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (bus.GetCPU().Flags.ZF)
            {
                Call(value);
                return 6;
            }

            return 3;
        }
    }

    public class CALLCImpl : CallInstruction
    {
        public static new byte OpCode => 0xDC;

        public CALLCImpl(Bus bus) : base(bus, "CALL C")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (bus.GetCPU().Flags.CY)
            {
                Call(value);
                return 6;
            }

            return 3;
        }
    }

    public class CALLNZImpl : CallInstruction
    {
        public static new byte OpCode => 0xC4;

        public CALLNZImpl(Bus bus) : base(bus, "CALL NZ")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (!bus.GetCPU().Flags.ZF)
            {
                Call(value);
                return 6;
            }

            return 3;
        }
    }

    public class CALLNCImpl : CallInstruction
    {
        public static new byte OpCode => 0xD4;

        public CALLNCImpl(Bus bus) : base(bus, "CALL NC")
        {
        }

        public override int Execute()
        {
            byte lo = bus.GetCPU().Fetch();
            byte hi = bus.GetCPU().Fetch();

            value = CombineHILO(hi, lo);

            if (!bus.GetCPU().Flags.CY)
            {
                Call(value);
                return 6;
            }

            return 3;
        }
    }
}
