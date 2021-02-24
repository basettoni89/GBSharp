using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.DeviceTest
{
    public class BusUtilityTest
    {
        private readonly Bus device;

        public BusUtilityTest()
        {
            this.device = new Bus();
        }

        [Fact]
        public void GetSetMemoryTest()
        {
            device.SetMemory(4, 0xFFFE);
            Assert.Equal(4, device.GetMemory(0xFFFE));
        }

        [Fact]
        public void GetSetZeroPageRAMTest()
        {
            device.SetMemory(4, 0xFF85);
            Assert.Equal(4, device.GetMemory(0xFF85));
        }

        [Fact]
        public void GetSetOAMTest()
        {
            device.SetMemory(4, 0xFE33);
            Assert.Equal(4, device.GetMemory(0xFE33));
        }

        [Fact]
        public void GetSetVideoRAMTest()
        {
            device.SetMemory(4, 0x9000);
            Assert.Equal(4, device.GetMemory(0x9000));
        }

        [Fact]
        public void GetSetWorkerRAMTest()
        {
            device.SetMemory(4, 0xD000);
            Assert.Equal(4, device.GetMemory(0xD000));
        }

        [Fact]
        public void GetSetUnmappedMemoryTest()
        {
            device.SetMemory(4, 0xB000);
            Assert.Equal(0, device.GetMemory(0xB000));
        }
    }
}
