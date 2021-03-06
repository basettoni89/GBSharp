﻿using GBEmu.Core.Exceptions;
using GBEmu.Core.Extensions;
using GBEmu.Core.Instructions;
using GBEmu.Core.Instructions.Branch;
using GBEmu.Core.Instructions.Loads;
using GBEmu.Core.Instructions.Math;
using GBEmu.Core.Instructions.Stores;
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

        public byte H;
        public byte L;

        public ushort BC
        {
            get => CombineHILO(B, C);
            set => SplitHILO(value, out B, out C);
        }

        public ushort DE
        {
            get => CombineHILO(D, E);
            set => SplitHILO(value, out D, out E);
        }

        public ushort HL
        {
            get => CombineHILO(H, L);
            set => SplitHILO(value, out H, out L);
        }

        public byte F
        {
            get
            {
                return ByteUtil.FromBits(false, false, false, false, Flags.CY, Flags.H, Flags.N, Flags.ZF);
            }

            set
            {
                Flags.ZF = value.GetBit(7);
                Flags.N = value.GetBit(6);
                Flags.H = value.GetBit(5);
                Flags.CY = value.GetBit(4);
            }
        }

        public UInt16 SP;
        public UInt16 PC;

        public FlagsClass Flags { get; private set; }

        public bool IME { get; set; }

        private int cycles = 0;

        public bool Complete => cycles != 0;

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
                {LDBCImpl.OpCode, new LDBCImpl(bus) },
                {LDDEImpl.OpCode, new LDDEImpl(bus) },
                {LDHLImpl.OpCode, new LDHLImpl(bus) },
                {LDSPImpl.OpCode, new LDSPImpl(bus) },
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
                {LDAInd.OpCode, new LDAInd(bus) },
                {LDAIndC.OpCode, new LDAIndC(bus) },
                {STAIndDE.OpCode, new STAIndDE(bus) },
                {STAIndBC.OpCode, new STAIndBC(bus) },
                {STAIndHLP.OpCode, new STAIndHLP(bus) },
                {STAIndHLM.OpCode, new STAIndHLM(bus) },
                {STAInd.OpCode, new STAInd(bus) },
                {STAIndC.OpCode, new STAIndC(bus) },
                {STIndHL.OpCode, new STIndHL(bus) },
                {STSPInd.OpCode, new STSPInd(bus) },
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
                {CPA.OpCode, new CPA(bus) },
                {CPAInd.OpCode, new CPAInd(bus) },
                {CPB.OpCode, new CPB(bus) },
                {CPC.OpCode, new CPC(bus) },
                {CPD.OpCode, new CPD(bus) },
                {CPE.OpCode, new CPE(bus) },
                {CPH.OpCode, new CPH(bus) },
                {CPL.OpCode, new CPL(bus) },
                {CPAddrHL.OpCode, new CPAddrHL(bus) },
                {JPImpl.OpCode, new JPImpl(bus) },
                {JPHLImpl.OpCode, new JPHLImpl(bus) },
                {JPZImpl.OpCode, new JPZImpl(bus) },
                {JPCImpl.OpCode, new JPCImpl(bus) },
                {JPNZImpl.OpCode, new JPNZImpl(bus) },
                {JPNCImpl.OpCode, new JPNCImpl(bus) },
                {JRImpl.OpCode, new JRImpl(bus) },
                {JRZImpl.OpCode, new JRZImpl(bus) },
                {JRCImpl.OpCode, new JRCImpl(bus) },
                {JRNZImpl.OpCode, new JRNZImpl(bus) },
                {JRNCImpl.OpCode, new JRNCImpl(bus) },
                {CALLImpl.OpCode, new CALLImpl(bus) },
                {CALLZImpl.OpCode, new CALLZImpl(bus) },
                {CALLCImpl.OpCode, new CALLCImpl(bus) },
                {CALLNZImpl.OpCode, new CALLNZImpl(bus) },
                {CALLNCImpl.OpCode, new CALLNCImpl(bus) },
                {RET.OpCode, new RET(bus) },
                {RETZ.OpCode, new RETZ(bus) },
                {RETC.OpCode, new RETC(bus) },
                {RETNZ.OpCode, new RETNZ(bus) },
                {RETNC.OpCode, new RETNC(bus) },
                {PUSHBC.OpCode, new PUSHBC(bus) },
                {PUSHDE.OpCode, new PUSHDE(bus) },
                {PUSHHL.OpCode, new PUSHHL(bus) },
                {PUSHAF.OpCode, new PUSHAF(bus) },
                {POPBC.OpCode, new POPBC(bus) },
                {POPDE.OpCode, new POPDE(bus) },
                {POPHL.OpCode, new POPHL(bus) },
                {POPAF.OpCode, new POPAF(bus) },
                {DI.OpCode, new DI(bus) },
                {EI.OpCode, new EI(bus) },
                {RST0.OpCode, new RST0(bus) },
                {RST1.OpCode, new RST1(bus) },
                {RST2.OpCode, new RST2(bus) },
                {RST3.OpCode, new RST3(bus) },
                {RST4.OpCode, new RST4(bus) },
                {RST5.OpCode, new RST5(bus) },
                {RST6.OpCode, new RST6(bus) },
                {RST7.OpCode, new RST7(bus) },
            };
        }

        public void Reset()
        {
            A = B = C = D = E = H = L = 0;
            SP = 0xFFFE;
            PC = 0x0100;
            Flags = new FlagsClass();

            IME = true;
        }

        public void Clock()
        {
            if (!Complete)
            {
                Instruction current = FetchInstruction();

                cycles = current.Execute();
            }

            cycles--;
        }

        public byte Fetch()
        {
            byte data = bus.ReadMemory(PC);
            PC++;
            return data;
        }

        public Instruction FetchInstruction()
        {
            byte opCode = bus.ReadMemory(PC);
            PC++;

            if (!lookup.ContainsKey(opCode))
                throw new IllegalInstructionException(opCode);

            return lookup[opCode];
        }

        public void Push(byte value)
        {
            SP--;
            bus.WriteMemory(value, SP);
        }

        public void Push(ushort value)
        {
            Push((byte)(value >> 8));
            Push((byte)value);
        }

        public byte Pop()
        {
            byte value = bus.ReadMemory(SP);
            SP++;
            return value;
        }

        /// <summary>
        /// Debug function to retrieve actual instruction lookup table
        /// </summary>
        /// <returns> Instruction lookup table </returns>
        public Dictionary<byte, Instruction> GetInstructionLookup()
        {
            return lookup;
        }

        private ushort CombineHILO(byte hi, byte lo)
        {
            return (ushort)((hi << 8) + lo);
        }

        private void SplitHILO(ushort value, out byte hi, out byte lo)
        {
            hi = (byte)(value >> 8);
            lo = (byte)(value);
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
