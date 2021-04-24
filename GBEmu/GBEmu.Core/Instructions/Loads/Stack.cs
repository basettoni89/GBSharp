using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Instructions.Loads
{
    public class PUSHBC : Instruction
    {
        public static new byte OpCode => 0xC5;

        public PUSHBC(Bus bus) : base(bus, "PUSH BC")
        {
        }

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.Push(cpu.B);
            cpu.Push(cpu.C);

            return 4;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PUSHDE : Instruction
    {
        public static new byte OpCode => 0xD5;

        public PUSHDE(Bus bus) : base(bus, "PUSH DE")
        {
        }

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.Push(cpu.D);
            cpu.Push(cpu.E);

            return 4;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PUSHHL : Instruction
    {
        public static new byte OpCode => 0xE5;

        public PUSHHL(Bus bus) : base(bus, "PUSH HL")
        {
        }

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.Push(cpu.H);
            cpu.Push(cpu.L);

            return 4;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PUSHAF : Instruction
    {
        public static new byte OpCode => 0xF5;

        public PUSHAF(Bus bus) : base(bus, "PUSH AF")
        {
        }

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.Push(cpu.A);
            cpu.Push(cpu.F);

            return 4;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class POPBC : Instruction
    {
        public POPBC(Bus bus) : base(bus, "POP BC")
        {
        }

        public static new byte OpCode => 0xC1;

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.C = cpu.Pop();
            cpu.B = cpu.Pop();

            return 3;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class POPDE : Instruction
    {
        public POPDE(Bus bus) : base(bus, "POP DE")
        {
        }

        public static new byte OpCode => 0xD1;

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.E = cpu.Pop();
            cpu.D = cpu.Pop();

            return 3;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class POPHL : Instruction
    {
        public POPHL(Bus bus) : base(bus, "POP HL")
        {
        }

        public static new byte OpCode => 0xE1;

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.L = cpu.Pop();
            cpu.H = cpu.Pop();

            return 3;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class POPAF : Instruction
    {
        public POPAF(Bus bus) : base(bus, "POP AF")
        {
        }

        public static new byte OpCode => 0xF1;

        public override int Execute()
        {
            var cpu = bus.GetCPU();

            cpu.F = cpu.Pop();
            cpu.A = cpu.Pop();

            return 3;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
