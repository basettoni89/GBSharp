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
            int expectedCycles = 4;

            cpu.Reset();

            bus.SetMemory(0xC3, 0xC000);
            bus.SetMemory((byte)address, 0xC001);
            bus.SetMemory((byte)(address >> 8), 0xC002);

            cpu.PC = 0xC000;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

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
            bus.SetMemory((byte)address, (uint)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (uint)(startAddress + 2));

            cpu.PC = startAddress;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

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
            bus.SetMemory((byte)address, (uint)(startAddress + 1));
            bus.SetMemory((byte)(address >> 8), (uint)(startAddress + 2));

            cpu.PC = startAddress;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

            Assert.Equal(expectedPC, cpu.PC);
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
            int expectedCycles = 3;

            cpu.Reset();

            bus.SetMemory(0x18, startAddress);
            bus.SetMemory(offset, (uint)(startAddress + 1));

            cpu.PC = startAddress;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

            Assert.Equal(startAddress + offset, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, true, 0xC000, 3)]
        [InlineData(0xC000, 0x00, false, 0xC002, 2)]
        [InlineData(0xC000, 0x09, true, 0xC009, 3)]
        [InlineData(0xC000, 0x09, false, 0xC002, 2)]
        [InlineData(0xC000, 0x10, true, 0xC010, 3)]
        [InlineData(0xC000, 0x10, false, 0xC002, 2)]
        [InlineData(0xC000, 0x0F, true, 0xC00F, 3)]
        [InlineData(0xC000, 0x0F, false, 0xC002, 2)]
        [InlineData(0xC000, 0xFF, true, 0xC0FF, 3)]
        [InlineData(0xC000, 0xFF, false, 0xC002, 2)]
        public void JRZImpl_PCContainsNewAddress(ushort startAddress, byte offset,
            bool zeroFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;

            bus.SetMemory(0x28, startAddress);
            bus.SetMemory(offset, (uint)(startAddress + 1));

            cpu.PC = startAddress;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

            Assert.Equal(expectedPC, cpu.PC);
        }

        [Theory]
        [InlineData(0xC000, 0x00, true, 0xC000, 3)]
        [InlineData(0xC000, 0x00, false, 0xC002, 2)]
        [InlineData(0xC000, 0x09, true, 0xC009, 3)]
        [InlineData(0xC000, 0x09, false, 0xC002, 2)]
        [InlineData(0xC000, 0x10, true, 0xC010, 3)]
        [InlineData(0xC000, 0x10, false, 0xC002, 2)]
        [InlineData(0xC000, 0x0F, true, 0xC00F, 3)]
        [InlineData(0xC000, 0x0F, false, 0xC002, 2)]
        [InlineData(0xC000, 0xFF, true, 0xC0FF, 3)]
        [InlineData(0xC000, 0xFF, false, 0xC002, 2)]
        public void JRCImpl_PCContainsNewAddress(ushort startAddress, byte offset,
            bool carryFlag, ushort expectedPC, int expectedCycles)
        {
            cpu.Reset();

            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0x38, startAddress);
            bus.SetMemory(offset, (uint)(startAddress + 1));

            cpu.PC = startAddress;

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

            Assert.Equal(expectedPC, cpu.PC);
        }
    }
}
