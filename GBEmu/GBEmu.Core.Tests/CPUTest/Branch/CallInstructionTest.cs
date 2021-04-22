using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class CallInstructionTest : CPUInstructionAbstractTest
    {
        [Theory]
        [InlineData(0x0100)]
        [InlineData(0x0109)]
        [InlineData(0xC000)]
        [InlineData(0xC00F)]
        [InlineData(0xCC00)]
        public void JPImpl_PCContainsNewAddress(ushort address)
        {
            cpu.Reset();

            bus.SetMemory(0xCD, 0xC000);
            bus.SetMemory((byte)address, 0xC001);
            bus.SetMemory((byte)(address >> 8), 0xC002);

            cpu.PC = 0xC000;

            TestExecution(6);

            //Verify jump to new address
            Assert.Equal(address, cpu.PC);

            //Verify PC pushed to stack
            Assert.Equal(0xC0, bus.GetMemory(0xFFFD));
            Assert.Equal(0x03, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }
    }
}
