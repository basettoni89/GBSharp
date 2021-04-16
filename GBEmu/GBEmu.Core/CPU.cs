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
                {DECAddrHL.OpCode, new DECAddrHL(bus) },
                {SUMARegA.OpCode, new SUMARegA(bus) },
                {SUMARegB.OpCode, new SUMARegB(bus) },
                {SUMARegC.OpCode, new SUMARegC(bus) },
                {SUMARegD.OpCode, new SUMARegD(bus) },
                {SUMARegE.OpCode, new SUMARegE(bus) },
                {SUMARegH.OpCode, new SUMARegH(bus) },
                {SUMARegL.OpCode, new SUMARegL(bus) },
                {SUMAAddrHL.OpCode, new SUMAAddrHL(bus) },
                {SUMAImpl.OpCode, new SUMAImpl(bus) },
                {SUBARegA.OpCode, new SUBARegA(bus) },
                {SUBARegB.OpCode, new SUBARegB(bus) },
                {SUBARegC.OpCode, new SUBARegC(bus) },
                {SUBARegD.OpCode, new SUBARegD(bus) },
                {SUBARegE.OpCode, new SUBARegE(bus) },
                {SUBARegH.OpCode, new SUBARegH(bus) },
                {SUBARegL.OpCode, new SUBARegL(bus) },
                {SUBAAddrHL.OpCode, new SUBAAddrHL(bus) },
                {SUBAImpl.OpCode, new SUBAImpl(bus) },
                {ANDARegA.OpCode, new ANDARegA(bus) },
                {ANDARegB.OpCode, new ANDARegB(bus) },
                {ANDARegC.OpCode, new ANDARegC(bus) },
                {ANDARegD.OpCode, new ANDARegD(bus) },
                {ANDARegE.OpCode, new ANDARegE(bus) },
                {ANDARegH.OpCode, new ANDARegH(bus) },
                {ANDARegL.OpCode, new ANDARegL(bus) },
                {ANDAAddrHL.OpCode, new ANDAAddrHL(bus) },
                {ANDAImpl.OpCode, new ANDAImpl(bus) },
                {ORARegA.OpCode, new ORARegA(bus) },
                {ORARegB.OpCode, new ORARegB(bus) },
                {ORARegC.OpCode, new ORARegC(bus) },
                {ORARegD.OpCode, new ORARegD(bus) },
                {ORARegE.OpCode, new ORARegE(bus) },
                {ORARegH.OpCode, new ORARegH(bus) },
                {ORARegL.OpCode, new ORARegL(bus) },
                {ORAAddrHL.OpCode, new ORAAddrHL(bus) },
                {ORAImpl.OpCode, new ORAImpl(bus) },
                {XORARegA.OpCode, new XORARegA(bus) },
                {XORARegB.OpCode, new XORARegB(bus) },
                {XORARegC.OpCode, new XORARegC(bus) },
                {XORARegD.OpCode, new XORARegD(bus) },
                {XORARegE.OpCode, new XORARegE(bus) },
                {XORARegH.OpCode, new XORARegH(bus) },
                {XORARegL.OpCode, new XORARegL(bus) },
                {XORAAddrHL.OpCode, new XORAAddrHL(bus) },
                {XORAImpl.OpCode, new XORAImpl(bus) },
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
            if(!Complete())
            {
                current = Fetch();

                cycles = current.Cycles;
                cycles -= current.Execute();
            }

            cycles--;
        }

        public bool Complete()
        {
            return cycles != 0;
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
