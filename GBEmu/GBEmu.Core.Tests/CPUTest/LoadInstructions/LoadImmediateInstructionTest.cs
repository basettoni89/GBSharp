using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadImmediateInstructionTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public LoadImmediateInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void LDA_AContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x3E, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.A);
        }

        [Fact]
        public void LDB_BContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x06, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.B);
        }

        [Fact]
        public void LDC_CContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x0E, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.C);
        }

        [Fact]
        public void LDD_DContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x16, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.D);
        }

        [Fact]
        public void LDE_EContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x1E, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.E);
        }

        [Fact]
        public void LDH_HContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x26, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.H);
        }

        [Fact]
        public void LDL_LContanisAValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x2E, 0xC000);
            bus.SetMemory(0x20, 0xC001);

            cpu.Clock();

            Assert.Equal(0xC002, cpu.PC);
            Assert.Equal(0x20, cpu.L);
        }
    }
}
