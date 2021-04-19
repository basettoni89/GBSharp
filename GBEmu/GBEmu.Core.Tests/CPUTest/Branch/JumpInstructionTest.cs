using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.Branch
{
    public class JumpInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public JumpInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

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

            cpu.Clock();

            Assert.Equal(address, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x0100, true, 0x0100)]
        [InlineData(0xC000, 0x0100, false, 0xC003)]
        [InlineData(0xC000, 0x0109, true, 0x0109)]
        [InlineData(0xC000, 0x0109, false, 0xC003)]
        [InlineData(0xC000, 0xC00F, true, 0xC00F)]
        [InlineData(0xC000, 0xC00F, false, 0xC003)]
        [InlineData(0xC000, 0xCC00, true, 0xCC00)]
        [InlineData(0xC000, 0xCC00, false, 0xC003)]
        public void JPZImpl_PCContainsNewAddress(ushort startAddress, ushort address, 
            bool zeroFlag, ushort expected)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0xCA, startAddress);
            bus.SetMemory((byte)address, (uint)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (uint)(startAddress + 2));

            cpu.PC = startAddress;

            cpu.Clock();

            Assert.Equal(expected, cpu.PC);
        }

        [Theory]
        [InlineData(0x00)]
        [InlineData(0x09)]
        [InlineData(0x10)]
        [InlineData(0x0F)]
        [InlineData(0xFF)]
        public void JRImpl_PCContainsNewAddress(byte offset)
        {
            ushort startAddress = 0xC000;
            cpu.Reset();

            bus.SetMemory(0x18, startAddress);
            bus.SetMemory(offset, (uint)(startAddress + 1));

            cpu.PC = startAddress;

            cpu.Clock();

            Assert.Equal(startAddress + offset, cpu.PC);
        }
    }
}
