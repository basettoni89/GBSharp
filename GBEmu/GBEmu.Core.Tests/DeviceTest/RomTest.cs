using GBEmu.Core.Tests.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.DeviceTest
{
    public class RomTest : AbstractROMTest
    {

        [Fact]
        public void ReadROM_VerifyMemoryInROM0()
        {
            bus.SetMemory(0x00, 0x0100);
            bus.SetMemory(0xC3, 0x0101);
            bus.SetMemory(0x50, 0x0102);
            bus.SetMemory(0x01, 0x0103);

            Assert.Equal(0x00, bus.GetMemory(0x0100));
            Assert.Equal(0xC3, bus.GetMemory(0x0101));
            Assert.Equal(0x50, bus.GetMemory(0x0102));
            Assert.Equal(0x01, bus.GetMemory(0x0103));
        }

        [Fact]
        public void ReadROM_VerifyMemoryInROM1()
        {
            bus.SetMemory(0x00, 0x4100);
            bus.SetMemory(0xC3, 0x4101);
            bus.SetMemory(0x50, 0x4102);
            bus.SetMemory(0x01, 0x4103);

            Assert.Equal(0x00, bus.GetMemory(0x4100));
            Assert.Equal(0xC3, bus.GetMemory(0x4101));
            Assert.Equal(0x50, bus.GetMemory(0x4102));
            Assert.Equal(0x01, bus.GetMemory(0x4103));
        }

        [Fact]
        public void LoadROM_VerifyMemoryInROM0()
        {
            byte[][] banks = LoadROM(TestRom.Empty);

            bool result = bus.LoadRomBank(0, banks[0]);
            Assert.True(result);

            Assert.Equal(0x00, bus.GetMemory(0x0100));
            Assert.Equal(0xC3, bus.GetMemory(0x0101));
            Assert.Equal(0x50, bus.GetMemory(0x0102));
            Assert.Equal(0x01, bus.GetMemory(0x0103));
        }

        [Fact]
        public void LoadROM_VerifyMemoryInROM1()
        {
            byte[][] banks = LoadROM(TestRom.Empty);
            bool result = bus.LoadRomBank(1, banks[0]);
            Assert.True(result);

            Assert.Equal(0x00, bus.GetMemory(0x4100));
            Assert.Equal(0xC3, bus.GetMemory(0x4101));
            Assert.Equal(0x50, bus.GetMemory(0x4102));
            Assert.Equal(0x01, bus.GetMemory(0x4103));
        }
    }
}
