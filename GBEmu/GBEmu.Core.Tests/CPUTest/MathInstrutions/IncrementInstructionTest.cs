using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class IncrementInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public IncrementInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void INCA_AContainsIncrementValue()
        {
            cpu.Reset();

            cpu.A = 0x05;
            cpu.PC = 0xC000;

            cpu.Flags.ZF = true;
            cpu.Flags.N = false;
            cpu.Flags.H = true;
            cpu.Flags.CY = true;

            bus.SetMemory(0x3C, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x06, cpu.A);

            Assert.False(cpu.Flags.ZF);
            Assert.False(cpu.Flags.N);
            Assert.False(cpu.Flags.H);
            Assert.False(cpu.Flags.CY);
        }
    }
}
