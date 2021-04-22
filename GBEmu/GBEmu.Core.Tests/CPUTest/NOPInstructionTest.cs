using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest
{
    public class NOPInstructionTest : AbstractInstructionTest
    {
        [Fact]
        public void NOOP_PCNextInstructionTest()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x00, 0xC000);

            TestExecution(1);

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }
    }
}
