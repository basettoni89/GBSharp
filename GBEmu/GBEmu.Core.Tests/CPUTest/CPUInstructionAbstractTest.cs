using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest
{
    public class CPUInstructionAbstractTest : IDisposable
    {
        protected readonly CPU cpu;
        protected readonly Bus bus;

        public CPUInstructionAbstractTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        protected void TestExecution(int expectedCycles)
        {
            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);
        }
    }
}
