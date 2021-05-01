using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class ResetInstructionTest : AbstractInstructionTest
    {
        [Fact]
        public void RST0_PCContainsNewAddress()
        {
            ExecuteResetTest(0xC7, 0x00);
        }

        [Fact]
        public void RST1_PCContainsNewAddress()
        {
            ExecuteResetTest(0xCF, 0x08);
        }

        [Fact]
        public void RST2_PCContainsNewAddress()
        {
            ExecuteResetTest(0xD7, 0x10);
        }

        [Fact]
        public void RST3_PCContainsNewAddress()
        {
            ExecuteResetTest(0xDF, 0x18);
        }

        [Fact]
        public void RST4_PCContainsNewAddress()
        {
            ExecuteResetTest(0xE7, 0x20);
        }

        [Fact]
        public void RST5_PCContainsNewAddress()
        {
            ExecuteResetTest(0xEF, 0x28);
        }

        [Fact]
        public void RST6_PCContainsNewAddress()
        {
            ExecuteResetTest(0xF7, 0x30);
        }

        [Fact]
        public void RST7_PCContainsNewAddress()
        {
            ExecuteResetTest(0xFF, 0x38);
        }

        private void ExecuteResetTest(byte opcode, byte addr)
        {
            cpu.Reset();

            bus.SetMemory(opcode, 0xC000);

            cpu.PC = 0xC000;
            cpu.SP = 0xFFFE;

            TestExecution(4);

            Assert.Equal(addr, cpu.PC);

            Assert.Equal(0xC0, bus.GetMemory(0xFFFD));
            Assert.Equal(0x01, bus.GetMemory(0xFFFC));
        }
    }
}
