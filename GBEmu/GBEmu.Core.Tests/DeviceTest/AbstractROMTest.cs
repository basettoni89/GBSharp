using GBEmu.Core.Tests.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.DeviceTest
{
    public abstract class AbstractROMTest : IDisposable
    {
        protected readonly Bus bus;
        protected readonly CPU cpu;

        public AbstractROMTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        protected void ExecuteInstruction()
        {
            do
            {
                cpu.Clock();
            } while (cpu.Complete);
        }

        protected byte[][] LoadROM(TestRom rom)
        {
            byte[] data = File.ReadAllBytes(rom.Path);

            Assert.Equal(0x8000, data.Length);

            byte[][] result = new byte[2][];

            for (int bank = 0; bank < 2; bank++)
            {
                result[bank] = new byte[0x4000];

                for (int addr = 0; addr < 0x4000; addr++)
                {
                    result[bank][addr] = data[bank * 0x4000 + addr];
                }
            }

            return result;
        }
    }
}
