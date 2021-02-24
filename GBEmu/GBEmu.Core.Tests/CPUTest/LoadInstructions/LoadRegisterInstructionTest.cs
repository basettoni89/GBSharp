using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadRegisterInstructionTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public LoadRegisterInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void LDARegisterA_AContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x7F, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterB_AContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x78, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterC_AContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x79, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterD_AContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x7A, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterE_AContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x7B, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterH_AContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x7C, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDARegisterL_AContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x7D, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.A);
        }

        [Fact]
        public void LDBRegisterA_BContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x47, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterB_BContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x40, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterC_BContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x41, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterD_BContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x42, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterE_BContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x43, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterH_BContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x44, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDBRegisterL_BContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x45, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.B);
        }

        [Fact]
        public void LDCRegisterA_CContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x4F, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterB_CContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x48, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterC_CContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x49, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterD_CContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x4A, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterE_CContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x4B, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterH_CContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x4C, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDCRegisterL_CContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x4D, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.C);
        }

        [Fact]
        public void LDDRegisterA_DContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x57, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterB_DContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x50, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterC_DContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x51, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterD_DContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x52, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterE_DContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x53, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterH_DContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x54, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDDRegisterL_DContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x55, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDERegisterA_EContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x5F, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDERegisterB_EContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x58, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDERegisterC_EContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x59, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDERegisterD_EContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x5A, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.D);
        }

        [Fact]
        public void LDERegisterE_EContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x5B, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDERegisterH_EContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x5C, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDERegisterL_EContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x5D, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.E);
        }

        [Fact]
        public void LDHRegisterA_HContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x67, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDHRegisterB_HContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x60, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDHRegisterC_HContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x61, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LHRegisterD_HContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x62, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDHRegisterE_HContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x63, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDHRegisterH_HContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x64, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDHRegisterL_HContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x65, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.H);
        }

        [Fact]
        public void LDLRegisterA_LContanisRegisterAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = 0x12;

            bus.SetMemory(0x6F, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LDLRegisterB_LContanisRegisterBValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = 0x12;

            bus.SetMemory(0x68, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LDLRegisterC_LContanisRegisterCValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = 0x12;

            bus.SetMemory(0x69, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LLRegisterD_LContanisRegisterDValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = 0x12;

            bus.SetMemory(0x6A, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LDLRegisterE_LContanisRegisterEValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = 0x12;

            bus.SetMemory(0x6B, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LDLRegisterH_LContanisRegisterHValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0x12;

            bus.SetMemory(0x6C, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }

        [Fact]
        public void LDLRegisterL_LContanisRegisterLValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = 0x12;

            bus.SetMemory(0x6D, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x12, cpu.L);
        }
    }
}
