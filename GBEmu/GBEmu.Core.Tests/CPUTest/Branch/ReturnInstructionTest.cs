using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class ReturnInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [InlineData(0xC001)]
        [InlineData(0xCC00)]
        [InlineData(0xCCFF)]
        public void RET_SetPCFromMemory(ushort address)
        {
            cpu.Reset();

            cpu.PC = 0xC010;

            bus.SetMemory(0xC9, 0xC010);

            bus.SetMemory((byte)(address >> 8), 0xFFFD);
            bus.SetMemory((byte)(address), 0xFFFC);

            cpu.SP = 0xFFFC;

            TestExecution(4);

            Assert.Equal(address, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Theory]
        [InlineData(0xC010, 0xC001, true, 0xC001, 5)]
        [InlineData(0xC010, 0xC001, false, 0xC011, 2)]
        [InlineData(0xC010, 0xCC00, true, 0xCC00, 5)]
        [InlineData(0xC010, 0xCC00, false, 0xC011, 2)]
        [InlineData(0xC010, 0xCCFF, true, 0xCCFF, 5)]
        [InlineData(0xC010, 0xCCFF, false, 0xC011, 2)]
        public void RETZ_SetPCFromMemory(ushort startAddress, ushort address,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            cpu.PC = startAddress;

            bus.SetMemory(0xC8, 0xC010);

            bus.SetMemory((byte)(address >> 8), 0xFFFD);
            bus.SetMemory((byte)(address), 0xFFFC);

            cpu.SP = 0xFFFC;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (zeroFlag)
                Assert.Equal(0xFFFE, cpu.SP);
            else
                Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0xC010, 0xC001, true, 0xC001, 5)]
        [InlineData(0xC010, 0xC001, false, 0xC011, 2)]
        [InlineData(0xC010, 0xCC00, true, 0xCC00, 5)]
        [InlineData(0xC010, 0xCC00, false, 0xC011, 2)]
        [InlineData(0xC010, 0xCCFF, true, 0xCCFF, 5)]
        [InlineData(0xC010, 0xCCFF, false, 0xC011, 2)]
        public void RETC_SetPCFromMemory(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            cpu.PC = startAddress;

            bus.SetMemory(0xD8, 0xC010);

            bus.SetMemory((byte)(address >> 8), 0xFFFD);
            bus.SetMemory((byte)(address), 0xFFFC);

            cpu.SP = 0xFFFC;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (carryFlag)
                Assert.Equal(0xFFFE, cpu.SP);
            else
                Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0xC010, 0xC001, false, 0xC001, 5)]
        [InlineData(0xC010, 0xC001, true, 0xC011, 2)]
        [InlineData(0xC010, 0xCC00, false, 0xCC00, 5)]
        [InlineData(0xC010, 0xCC00, true, 0xC011, 2)]
        [InlineData(0xC010, 0xCCFF, false, 0xCCFF, 5)]
        [InlineData(0xC010, 0xCCFF, true, 0xC011, 2)]
        public void RETNZ_SetPCFromMemory(ushort startAddress, ushort address,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            cpu.PC = startAddress;

            bus.SetMemory(0xC0, 0xC010);

            bus.SetMemory((byte)(address >> 8), 0xFFFD);
            bus.SetMemory((byte)(address), 0xFFFC);

            cpu.SP = 0xFFFC;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (!zeroFlag)
                Assert.Equal(0xFFFE, cpu.SP);
            else
                Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0xC010, 0xC001, false, 0xC001, 5)]
        [InlineData(0xC010, 0xC001, true, 0xC011, 2)]
        [InlineData(0xC010, 0xCC00, false, 0xCC00, 5)]
        [InlineData(0xC010, 0xCC00, true, 0xC011, 2)]
        [InlineData(0xC010, 0xCCFF, false, 0xCCFF, 5)]
        [InlineData(0xC010, 0xCCFF, true, 0xC011, 2)]
        public void RETNC_SetPCFromMemory(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            cpu.PC = startAddress;

            bus.SetMemory(0xD0, 0xC010);

            bus.SetMemory((byte)(address >> 8), 0xFFFD);
            bus.SetMemory((byte)(address), 0xFFFC);

            cpu.SP = 0xFFFC;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);

            if (!carryFlag)
                Assert.Equal(0xFFFE, cpu.SP);
            else
                Assert.Equal(0xFFFC, cpu.SP);
        }
    }
}
