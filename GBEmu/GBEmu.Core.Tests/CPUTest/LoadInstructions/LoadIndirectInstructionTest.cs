using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadIndirectInstructionTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public LoadIndirectInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void LDAHL_AContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x7E, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.A);
        }

        [Fact]
        public void LDBHL_BContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x46, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.B);
        }

        [Fact]
        public void LDCHL_CContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x4E, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.C);
        }

        [Fact]
        public void LDDHL_DContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x56, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.D);
        }

        [Fact]
        public void LDEHL_EContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x5E, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.E);
        }

        [Fact]
        public void LDHHL_HContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x66, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.H);
        }

        [Fact]
        public void LDLHL_LContanisHLIndirectValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(0x6E, 0xC000);
            bus.SetMemory(0x42, 0xCC01);


            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, cpu.L);
        }
    }
}
