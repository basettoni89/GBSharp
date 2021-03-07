using GBEmu.Core.Instructions;
using GBEmu.Core.Instructions.Loads;
using GBEmu.Core.Instructions.Math;
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
                {NOP.OpCode, new NOP(bus) },
                {LDAImpl.OpCode, new LDAImpl(bus) },
                {LDBImpl.OpCode, new LDBImpl(bus) },
                {LDCImpl.OpCode, new LDCImpl(bus) },
                {LDDImpl.OpCode, new LDDImpl(bus) },
                {LDEImpl.OpCode, new LDEImpl(bus) },
                {LDHImpl.OpCode, new LDHImpl(bus) },
                {LDLImpl.OpCode, new LDLImpl(bus) },
                {LDARegA.OpCode, new LDARegA(bus) },
                {LDARegB.OpCode, new LDARegB(bus) },
                {LDARegC.OpCode, new LDARegC(bus) },
                {LDARegD.OpCode, new LDARegD(bus) },
                {LDARegE.OpCode, new LDARegE(bus) },
                {LDARegH.OpCode, new LDARegH(bus) },
                {LDARegL.OpCode, new LDARegL(bus) },
                {LDBRegA.OpCode, new LDBRegA(bus) },
                {LDBRegB.OpCode, new LDBRegB(bus) },
                {LDBRegC.OpCode, new LDBRegC(bus) },
                {LDBRegD.OpCode, new LDBRegD(bus) },
                {LDBRegE.OpCode, new LDBRegE(bus) },
                {LDBRegH.OpCode, new LDBRegH(bus) },
                {LDBRegL.OpCode, new LDBRegL(bus) },
                {LDCRegA.OpCode, new LDCRegA(bus) },
                {LDCRegB.OpCode, new LDCRegB(bus) },
                {LDCRegC.OpCode, new LDCRegC(bus) },
                {LDCRegD.OpCode, new LDCRegD(bus) },
                {LDCRegE.OpCode, new LDCRegE(bus) },
                {LDCRegH.OpCode, new LDCRegH(bus) },
                {LDCRegL.OpCode, new LDCRegL(bus) },
                {LDDRegA.OpCode, new LDDRegA(bus) },
                {LDDRegB.OpCode, new LDDRegB(bus) },
                {LDDRegC.OpCode, new LDDRegC(bus) },
                {LDDRegD.OpCode, new LDDRegD(bus) },
                {LDDRegE.OpCode, new LDDRegE(bus) },
                {LDDRegH.OpCode, new LDDRegH(bus) },
                {LDDRegL.OpCode, new LDDRegL(bus) },
                {LDERegA.OpCode, new LDERegA(bus) },
                {LDERegB.OpCode, new LDERegB(bus) },
                {LDERegC.OpCode, new LDERegC(bus) },
                {LDERegD.OpCode, new LDERegD(bus) },
                {LDERegE.OpCode, new LDERegE(bus) },
                {LDERegH.OpCode, new LDERegH(bus) },
                {LDERegL.OpCode, new LDERegL(bus) },
                {LDHRegA.OpCode, new LDHRegA(bus) },
                {LDHRegB.OpCode, new LDHRegB(bus) },
                {LDHRegC.OpCode, new LDHRegC(bus) },
                {LDHRegD.OpCode, new LDHRegD(bus) },
                {LDHRegE.OpCode, new LDHRegE(bus) },
                {LDHRegH.OpCode, new LDHRegH(bus) },
                {LDHRegL.OpCode, new LDHRegL(bus) },
                {LDLRegA.OpCode, new LDLRegA(bus) },
                {LDLRegB.OpCode, new LDLRegB(bus) },
                {LDLRegC.OpCode, new LDLRegC(bus) },
                {LDLRegD.OpCode, new LDLRegD(bus) },
                {LDLRegE.OpCode, new LDLRegE(bus) },
                {LDLRegH.OpCode, new LDLRegH(bus) },
                {LDLRegL.OpCode, new LDLRegL(bus) },
                {LDAIndHL.OpCode, new LDAIndHL(bus) },
                {LDBIndHL.OpCode, new LDBIndHL(bus) },
                {LDCIndHL.OpCode, new LDCIndHL(bus) },
                {LDDIndHL.OpCode, new LDDIndHL(bus) },
                {LDEIndHL.OpCode, new LDEIndHL(bus) },
                {LDHIndHL.OpCode, new LDHIndHL(bus) },
                {LDLIndHL.OpCode, new LDLIndHL(bus) },
                {INCA.OpCode, new INCA(bus) },
                {INCB.OpCode, new INCB(bus) },
                {INCC.OpCode, new INCC(bus) },
                {INCD.OpCode, new INCD(bus) },
                {INCE.OpCode, new INCE(bus) },
                {INCH.OpCode, new INCH(bus) },
                {INCL.OpCode, new INCL(bus) },
                {INCAddrHL.OpCode, new INCAddrHL(bus) },
                {INCBC.OpCode, new INCBC(bus) },
                {INCDE.OpCode, new INCDE(bus) },
                {INCHL.OpCode, new INCHL(bus) },
                {INCSP.OpCode, new INCSP(bus) },
                {DECA.OpCode, new DECA(bus) },
                {DECB.OpCode, new DECB(bus) },
                {DECC.OpCode, new DECC(bus) },
                {DECD.OpCode, new DECD(bus) },
                {DECE.OpCode, new DECE(bus) },
                {DECH.OpCode, new DECH(bus) },
                {DECL.OpCode, new DECL(bus) },
                {DECAddrHL.OpCode, new DECAddrHL(bus) }
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

        /// <summary>
        /// Debug function to retrieve actual instruction lookup table
        /// </summary>
        /// <returns> Instruction lookup table </returns>
        public Dictionary<byte, Instruction> GetInstructionLookup()
        {
            return lookup;
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
