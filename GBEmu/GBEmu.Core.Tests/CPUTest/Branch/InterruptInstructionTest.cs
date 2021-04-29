using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class InterruptInstructionTest : AbstractInstructionTest
    {
        [Fact]
        public void DI_InterruptAreDisabled()
        {
            cpu.Reset();

            cpu.IME = true;

            cpu.PC = 0xC000;

            bus.SetMemory(0xF3, 0xC000);

            TestExecution(1);

            Assert.Equal(0xC001, cpu.PC);
            Assert.False(cpu.IME);
        }

        [Fact]
        public void EI_InterruptAreEnabled()
        {
            cpu.Reset();

            cpu.IME = false;

            cpu.PC = 0xC000;

            bus.SetMemory(0xFB, 0xC000);

            TestExecution(1);

            Assert.Equal(0xC001, cpu.PC);
            Assert.True(cpu.IME);
        }
    }
}
