using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core
{
    public class CPU
    {
        private readonly Bus bus;

        private readonly Dictionary<byte, Instruction> lookup;

        public byte A;
        public byte B;
        public byte C;
        public byte D;
        public byte E;
        public byte F;
        public byte H;
        public byte L;

        public UInt16 SP;
        public UInt16 PC;

        public FlagsClass Flags { get; private set; }

        private Instruction current;
        private int cycles = 0;

        public CPU(Bus bus)
        {
            this.bus = bus;

            Flags = new FlagsClass();

            lookup = new Dictionary<byte, Instruction>()
            {
                {0x00, new NOP(bus) },
                {0x3E, new LDAImpl(bus) },
                {0x06, new LDBImpl(bus) },
                {0x0E, new LDCImpl(bus) },
                {0x16, new LDDImpl(bus) },
                {0x1E, new LDEImpl(bus) },
                {0x26, new LDHImpl(bus) },
                {0x2E, new LDLImpl(bus) },
                {0x7F, new LDARegA(bus) },
                {0x78, new LDARegB(bus) },
                {0x79, new LDARegC(bus) },
                {0x7A, new LDARegD(bus) },
                {0x7B, new LDARegE(bus) },
                {0x7C, new LDARegH(bus) },
                {0x7D, new LDARegL(bus) },
                {0x47, new LDBRegA(bus) },
                {0x40, new LDBRegB(bus) },
                {0x41, new LDBRegC(bus) },
                {0x42, new LDBRegD(bus) },
                {0x43, new LDBRegE(bus) },
                {0x44, new LDBRegH(bus) },
                {0x45, new LDBRegL(bus) },
                {0x4F, new LDCRegA(bus) },
                {0x48, new LDCRegB(bus) },
                {0x49, new LDCRegC(bus) },
                {0x4A, new LDCRegD(bus) },
                {0x4B, new LDCRegE(bus) },
                {0x4C, new LDCRegH(bus) },
                {0x4D, new LDCRegL(bus) },
                {0x57, new LDDRegA(bus) },
                {0x50, new LDDRegB(bus) },
                {0x51, new LDDRegC(bus) },
                {0x52, new LDDRegD(bus) },
                {0x53, new LDDRegE(bus) },
                {0x54, new LDDRegH(bus) },
                {0x55, new LDDRegL(bus) },
                {0x5F, new LDERegA(bus) },
                {0x58, new LDERegB(bus) },
                {0x59, new LDERegC(bus) },
                {0x5A, new LDERegD(bus) },
                {0x5B, new LDERegE(bus) },
                {0x5C, new LDERegH(bus) },
                {0x5D, new LDERegL(bus) },
                {0x67, new LDHRegA(bus) },
                {0x60, new LDHRegB(bus) },
                {0x61, new LDHRegC(bus) },
                {0x62, new LDHRegD(bus) },
                {0x63, new LDHRegE(bus) },
                {0x64, new LDHRegH(bus) },
                {0x65, new LDHRegL(bus) },
                {0x6F, new LDLRegA(bus) },
                {0x68, new LDLRegB(bus) },
                {0x69, new LDLRegC(bus) },
                {0x6A, new LDLRegD(bus) },
                {0x6B, new LDLRegE(bus) },
                {0x6C, new LDLRegH(bus) },
                {0x6D, new LDLRegL(bus) },
                {0x7E, new LDAIndHL(bus) },
                {0x46, new LDBIndHL(bus) },
                {0x4E, new LDCIndHL(bus) },
                {0x56, new LDDIndHL(bus) },
                {0x5E, new LDEIndHL(bus) },
                {0x66, new LDHIndHL(bus) },
                {0x6E, new LDLIndHL(bus) },
                {0x3C, new INCA(bus) },
                {0x04, new INCB(bus) },
                {0x0C, new INCC(bus) },
                {0x14, new INCD(bus) },
                {0x1C, new INCE(bus) },
                {0x24, new INCH(bus) },
                {0x2C, new INCL(bus) },
                {0x34, new INCAddrHL(bus) },
                {0x03, new INCBC(bus) },
                {0x13, new INCDE(bus) },
                {0x23, new INCHL(bus) },
                {0x33, new INCSP(bus) }
            };
        }

        public void Reset()
        {
            A = B = C = D = E = F = H = L = 0;
            SP = 0xFFFE;
            PC = 0x0100;
            Flags = new FlagsClass();
        }

        public void Clock()
        {
            if(cycles == 0)
            {
                current = Fetch();

                cycles = current.Cycles;
                cycles -= current.Execute();
            }

            cycles--;
        }

        public Instruction Fetch()
        {
            byte opCode = bus.ReadMemory(PC);
            PC++;
            return lookup[opCode];
        }

        public class FlagsClass
        {
            /// <summary>
            /// Zero Flag
            /// </summary>
            public bool ZF { get; set; }

            /// <summary>
            /// Subtraction
            /// </summary>
            public bool N { get; set; }

            /// <summary>
            /// Half Carry
            /// </summary>
            public bool H { get; set; }

            /// <summary>
            /// Carry Flag
            /// </summary>
            public bool CY { get; set; }
        }
    }
}
