using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class CallInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [InlineData(0x0100)]
        [InlineData(0x0109)]
        [InlineData(0xC000)]
        [InlineData(0xC00F)]
        [InlineData(0xCC00)]
        public void CALLImpl_PCContainsNewAddress(ushort address)
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

        [Theory]
        [InlineData(0xC000, 0x0100, true, 0x0100, 6)]
        [InlineData(0xC000, 0x0100, false, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, true, 0x0109, 6)]
        [InlineData(0xC000, 0x0109, false, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, true, 0xC00F, 6 )]
        [InlineData(0xC000, 0xC00F, false, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, true, 0xCC00, 6)]
        [InlineData(0xC000, 0xCC00, false, 0xC003, 3)]
        public void CALLZImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0xCC, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if(zeroFlag)
            {
                //Verify PC pushed to stack
                Assert.Equal((byte)(startAddress >> 8), bus.GetMemory(0xFFFD));
                Assert.Equal((byte)(startAddress + 3), bus.GetMemory(0xFFFC));

                Assert.Equal(0xFFFC, cpu.SP);
            }
            else
            {
                Assert.Equal(0xFFFE, cpu.SP);
            }
        }

        [Theory]
        [InlineData(0xC000, 0x0100, true, 0x0100, 6)]
        [InlineData(0xC000, 0x0100, false, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, true, 0x0109, 6)]
        [InlineData(0xC000, 0x0109, false, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, true, 0xC00F, 6)]
        [InlineData(0xC000, 0xC00F, false, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, true, 0xCC00, 6)]
        [InlineData(0xC000, 0xCC00, false, 0xC003, 3)]
        public void CALLCImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0xDC, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (carryFlag)
            {
                //Verify PC pushed to stack
                Assert.Equal((byte)(startAddress >> 8), bus.GetMemory(0xFFFD));
                Assert.Equal((byte)(startAddress + 3), bus.GetMemory(0xFFFC));

                Assert.Equal(0xFFFC, cpu.SP);
            }
            else
            {
                Assert.Equal(0xFFFE, cpu.SP);
            }
        }

        [Theory]
        [InlineData(0xC000, 0x0100, false, 0x0100, 6)]
        [InlineData(0xC000, 0x0100, true, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, false, 0x0109, 6)]
        [InlineData(0xC000, 0x0109, true, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, false, 0xC00F, 6)]
        [InlineData(0xC000, 0xC00F, true, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, false, 0xCC00, 6)]
        [InlineData(0xC000, 0xCC00, true, 0xC003, 3)]
        public void CALLNZImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0xC4, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (!zeroFlag)
            {
                //Verify PC pushed to stack
                Assert.Equal((byte)(startAddress >> 8), bus.GetMemory(0xFFFD));
                Assert.Equal((byte)(startAddress + 3), bus.GetMemory(0xFFFC));

                Assert.Equal(0xFFFC, cpu.SP);
            }
            else
            {
                Assert.Equal(0xFFFE, cpu.SP);
            }
        }

        [Theory]
        [InlineData(0xC000, 0x0100, false, 0x0100, 6)]
        [InlineData(0xC000, 0x0100, true, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, false, 0x0109, 6)]
        [InlineData(0xC000, 0x0109, true, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, false, 0xC00F, 6)]
        [InlineData(0xC000, 0xC00F, true, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, false, 0xCC00, 6)]
        [InlineData(0xC000, 0xCC00, true, 0xC003, 3)]
        public void CALLNCImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0xD4, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (!carryFlag)
            {
                //Verify PC pushed to stack
                Assert.Equal((byte)(startAddress >> 8), bus.GetMemory(0xFFFD));
                Assert.Equal((byte)(startAddress + 3), bus.GetMemory(0xFFFC));

                Assert.Equal(0xFFFC, cpu.SP);
            }
            else
            {
                Assert.Equal(0xFFFE, cpu.SP);
            }
        }
    }
}
