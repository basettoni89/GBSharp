using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Branch
{
    public class DI : Instruction
    {
        public static new byte OpCode => 0xF3;

        public DI(Bus bus) : base(bus, "DI")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().IME = false;
            return 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public class EI : Instruction
    {
        public static new byte OpCode => 0xFB;

        public EI(Bus bus) : base(bus, "EI")
        {
        }

        public override int Execute()
        {
            bus.GetCPU().IME = true;
            return 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
