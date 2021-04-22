using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public abstract class CallInstruction : JumpInstruction
    {
        protected CallInstruction(Bus bus, string name) : base(bus, name)
        {
        }
        protected void Call(ushort address)
        {
            bus.GetCPU().Push(bus.GetCPU().PC);
            JumpTo(address);
        }
    }

    public class CALLImpl : CallInstruction
    {
        private ushort value;

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

        public override string ToString()
        {
            return $"{Name}, {value:X8}";
        }
    }
}
