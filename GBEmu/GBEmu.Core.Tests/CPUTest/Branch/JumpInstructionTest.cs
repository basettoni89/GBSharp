using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class JumpInstructionTest : AbstractInstructionTest
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

            bus.SetMemory(0xC3, 0xC000);
            bus.SetMemory((byte)address, 0xC001);
            bus.SetMemory((byte)(address >> 8), 0xC002);

            cpu.PC = 0xC000;

            TestExecution(4);

            Assert.Equal(address, cpu.PC);
        }

        [Theory]
        [InlineData(0x0100)]
        [InlineData(0x0109)]
        [InlineData(0xC000)]
        [InlineData(0xC00F)]
        [InlineData(0xCC00)]
        public void JPHLImpl_PCContainsNewAddress(ushort address)
        {
            cpu.Reset();

            bus.SetMemory(0xE9, 0xC000);

            bus.GetCPU().L = (byte)address;
            bus.GetCPU().H = (byte)(address >> 8);

            cpu.PC = 0xC000;

            TestExecution(1);

            Assert.Equal(address, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x0100, true, 0x0100, 4)]
        [InlineData(0xC000, 0x0100, false, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, true, 0x0109, 4)]
        [InlineData(0xC000, 0x0109, false, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, true, 0xC00F, 4)]
        [InlineData(0xC000, 0xC00F, false, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, true, 0xCC00, 4)]
        [InlineData(0xC000, 0xCC00, false, 0xC003, 3)]
        public void JPZImpl_PCContainsNewAddress(ushort startAddress, ushort address, 
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0xCA, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x0100, true, 0x0100, 4)]
        [InlineData(0xC000, 0x0100, false, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, true, 0x0109, 4)]
        [InlineData(0xC000, 0x0109, false, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, true, 0xC00F, 4)]
        [InlineData(0xC000, 0xC00F, false, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, true, 0xCC00, 4)]
        [InlineData(0xC000, 0xCC00, false, 0xC003, 3)]
        public void JPCImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0xDA, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x0100, false, 0x0100, 4)]
        [InlineData(0xC000, 0x0100, true, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, false, 0x0109, 4)]
        [InlineData(0xC000, 0x0109, true, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, false, 0xC00F, 4)]
        [InlineData(0xC000, 0xC00F, true, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, false, 0xCC00, 4)]
        [InlineData(0xC000, 0xCC00, true, 0xC003, 3)]
        public void JPNZImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0xC2, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x0100, false, 0x0100, 4)]
        [InlineData(0xC000, 0x0100, true, 0xC003, 3)]
        [InlineData(0xC000, 0x0109, false, 0x0109, 4)]
        [InlineData(0xC000, 0x0109, true, 0xC003, 3)]
        [InlineData(0xC000, 0xC00F, false, 0xC00F, 4)]
        [InlineData(0xC000, 0xC00F, true, 0xC003, 3)]
        [InlineData(0xC000, 0xCC00, false, 0xCC00, 4)]
        [InlineData(0xC000, 0xCC00, true, 0xC003, 3)]
        public void JPNCImpl_PCContainsNewAddress(ushort startAddress, ushort address,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0xD2, startAddress);
            bus.SetMemory((byte)address, (ushort)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (ushort)(startAddress + 2));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0x00, 0xC002)]
        [InlineData(0x0B, 0xC00D)]
        [InlineData(0x7F, 0xC081)]
        [InlineData(-0x01, 0xC001)]
        [InlineData(-0x20, 0xBFE2)]
        [InlineData(-0x80, 0xBF82)]
        public void JRImpl_PCContainsNewAddress(sbyte offset, ushort expectedPC)
        {
            ushort startAddress = 0xC000;
            int expectedCycles = 3;

            cpu.Reset();

            bus.SetMemory(0x18, startAddress);
            bus.SetMemory((byte)offset, (ushort)(startAddress + 1));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, true, 0xC002, 3)]
        [InlineData(0xC000, 0x00, false, 0xC002, 2)]
        [InlineData(0xC000, 0x09, true, 0xC00B, 3)]
        [InlineData(0xC000, 0x09, false, 0xC002, 2)]
        [InlineData(0xC000, 0x7F, true, 0xC081, 3)]
        [InlineData(0xC000, 0x7F, false, 0xC002, 2)]
        [InlineData(0xC000, -0x01, true, 0xC001, 3)]
        [InlineData(0xC000, -0x01, false, 0xC002, 2)]
        [InlineData(0xC000, -0x80, true, 0xBF82, 3)]
        [InlineData(0xC000, -0x80, false, 0xC002, 2)]
        public void JRZImpl_PCContainsNewAddress(ushort startAddress, sbyte offset,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0x28, startAddress);
            bus.SetMemory((byte)offset, (ushort)(startAddress + 1));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, true, 0xC002, 3)]
        [InlineData(0xC000, 0x00, false, 0xC002, 2)]
        [InlineData(0xC000, 0x09, true, 0xC00B, 3)]
        [InlineData(0xC000, 0x09, false, 0xC002, 2)]
        [InlineData(0xC000, 0x7F, true, 0xC081, 3)]
        [InlineData(0xC000, 0x7F, false, 0xC002, 2)]
        [InlineData(0xC000, -0x01, true, 0xC001, 3)]
        [InlineData(0xC000, -0x01, false, 0xC002, 2)]
        [InlineData(0xC000, -0x80, true, 0xBF82, 3)]
        [InlineData(0xC000, -0x80, false, 0xC002, 2)]
        public void JRCImpl_PCContainsNewAddress(ushort startAddress, sbyte offset,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0x38, startAddress);
            bus.SetMemory((byte)offset, (ushort)(startAddress + 1));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, false, 0xC002, 3)]
        [InlineData(0xC000, 0x00, true, 0xC002, 2)]
        [InlineData(0xC000, 0x09, false, 0xC00B, 3)]
        [InlineData(0xC000, 0x09, true, 0xC002, 2)]
        [InlineData(0xC000, 0x7F, false, 0xC081, 3)]
        [InlineData(0xC000, 0x7F, true, 0xC002, 2)]
        [InlineData(0xC000, -0x01, false, 0xC001, 3)]
        [InlineData(0xC000, -0x01, true, 0xC002, 2)]
        [InlineData(0xC000, -0x80, false, 0xBF82, 3)]
        [InlineData(0xC000, -0x80, true, 0xC002, 2)]
        public void JRNZImpl_PCContainsNewAddress(ushort startAddress, sbyte offset,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0x20, startAddress);
            bus.SetMemory((byte)offset, (ushort)(startAddress + 1));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, false, 0xC002, 3)]
        [InlineData(0xC000, 0x00, true, 0xC002, 2)]
        [InlineData(0xC000, 0x09, false, 0xC00B, 3)]
        [InlineData(0xC000, 0x09, true, 0xC002, 2)]
        [InlineData(0xC000, 0x7F, false, 0xC081, 3)]
        [InlineData(0xC000, 0x7F, true, 0xC002, 2)]
        [InlineData(0xC000, -0x01, false, 0xC001, 3)]
        [InlineData(0xC000, -0x01, true, 0xC002, 2)]
        [InlineData(0xC000, -0x80, false, 0xBF82, 3)]
        [InlineData(0xC000, -0x80, true, 0xC002, 2)]
        public void JRNCImpl_PCContainsNewAddress(ushort startAddress, sbyte offset,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0x30, startAddress);
            bus.SetMemory((byte)offset, (ushort)(startAddress + 1));

            cpu.PC = startAddress;

            TestExecution(expectedCycles);

            Assert.Equal(expectedPC, cpu.PC);
        }
    }
}
