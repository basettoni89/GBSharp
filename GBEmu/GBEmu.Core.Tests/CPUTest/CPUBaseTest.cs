using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest
{
    public class CPUBaseTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public CPUBaseTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Reset_ReturnToStartPC()
        {
            cpu.PC = 0x0200;
            cpu.SP = 0x3000;

            cpu.Reset();

            Assert.Equal(0x0100, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Fact]
        public void NOOP_PCNextInstructionTest()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x00, 0xC000);

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(1, cycles);

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }
    }
}
