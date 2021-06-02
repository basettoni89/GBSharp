using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.PPUTest
{
    public class PPUBaseTest : IDisposable
    {

        readonly PPU ppu;
        readonly Bus bus;

        public PPUBaseTest()
        {
            this.bus = new Bus();
            this.ppu = bus.GetPPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetSCYValue(byte value)
        {
            TestGetRegisterValue(0xFF42, value, () => ppu.SCY);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetSCYValue(byte value)
        {
            TestSetRegisterValue(0xFF42, value, (byte value) => ppu.SCY = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetSCXValue(byte value)
        {
            TestGetRegisterValue(0xFF43, value, () => ppu.SCX);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetSCXValue(byte value)
        {
            TestSetRegisterValue(0xFF43, value, (byte value) => ppu.SCX = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetLYValue(byte value)
        {
            TestGetRegisterValue(0xFF44, value, () => ppu.LY);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetLYValue(byte value)
        {
            TestSetRegisterValue(0xFF44, value, (byte value) => ppu.LY = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetLYCValue(byte value)
        {
            TestGetRegisterValue(0xFF45, value, () => ppu.LYC);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetLYCValue(byte value)
        {
            TestSetRegisterValue(0xFF45, value, (byte value) => ppu.LYC = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetDMAValue(byte value)
        {
            TestGetRegisterValue(0xFF46, value, () => ppu.DMA);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetDMAValue(byte value)
        {
            TestSetRegisterValue(0xFF46, value, (byte value) => ppu.DMA = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetBGPValue(byte value)
        {
            TestGetRegisterValue(0xFF47, value, () => ppu.BGP);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetBGPValue(byte value)
        {
            TestSetRegisterValue(0xFF47, value, (byte value) => ppu.BGP = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetOBP0Value(byte value)
        {
            TestGetRegisterValue(0xFF48, value, () => ppu.OBP0);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetOBP0Value(byte value)
        {
            TestSetRegisterValue(0xFF48, value, (byte value) => ppu.OBP0 = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetOBP1Value(byte value)
        {
            TestGetRegisterValue(0xFF49, value, () => ppu.OBP1);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetOBP1Value(byte value)
        {
            TestSetRegisterValue(0xFF49, value, (byte value) => ppu.OBP1 = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetWYValue(byte value)
        {
            TestGetRegisterValue(0xFF4A, value, () => ppu.WY);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetWYValue(byte value)
        {
            TestSetRegisterValue(0xFF4A, value, (byte value) => ppu.WY = value);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void GetWXValue(byte value)
        {
            TestGetRegisterValue(0xFF4B, value, () => ppu.WX);
        }

        [Theory]
        [ClassData(typeof(RegisterTestData))]
        public void SetWXValue(byte value)
        {
            TestSetRegisterValue(0xFF4B, value, (byte value) => ppu.WX = value);
        }

        [Theory]
        [ClassData(typeof(LCDCTestData))]
        public void GetLCDCValue(bool[] bits, byte value)
        {
            ppu.LCDC.F0 = bits[0];
            ppu.LCDC.F1 = bits[1];
            ppu.LCDC.F2 = bits[2];
            ppu.LCDC.F3 = bits[3];
            ppu.LCDC.F4 = bits[4];
            ppu.LCDC.F5 = bits[5];
            ppu.LCDC.F6 = bits[6];
            ppu.LCDC.F7 = bits[7];

            Assert.Equal(value, bus.ReadMemory(0xFF40));
        }

        [Theory]
        [ClassData(typeof(LCDCTestData))]
        public void SetLCDCValue(bool[] bits, byte value)
        {
            bus.WriteMemory(value, 0xFF40);

            Assert.Equal(bits[0], ppu.LCDC.F0);
            Assert.Equal(bits[1], ppu.LCDC.F1);
            Assert.Equal(bits[2], ppu.LCDC.F2);
            Assert.Equal(bits[3], ppu.LCDC.F3);
            Assert.Equal(bits[4], ppu.LCDC.F4);
            Assert.Equal(bits[5], ppu.LCDC.F5);
            Assert.Equal(bits[6], ppu.LCDC.F6);
            Assert.Equal(bits[7], ppu.LCDC.F7);

        }

        private void TestGetRegisterValue(ushort address, byte value, Func<byte> register)
        {
            bus.WriteMemory(value, address);
            Assert.Equal(value, register.Invoke());
        }

        private void TestSetRegisterValue(ushort address, byte value, Action<byte> register)
        {
            register.Invoke(value);
            Assert.Equal(value, bus.ReadMemory(address));
        }

        class RegisterTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00 };
                yield return new object[] { 0x01 };
                yield return new object[] { 0x10 };
                yield return new object[] { 0x32 };
                yield return new object[] { 0xFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class LCDCTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new bool[] { false, false, false, false, false, false, false, false }, 0b00000000};
                yield return new object[] { new bool[] { true, false, false, false, false, false, false, false }, 0b00000001 };
                yield return new object[] { new bool[] { false, true, false, false, false, false, false, false }, 0b00000010 };
                yield return new object[] { new bool[] { false, false, true, false, false, false, false, false }, 0b00000100 };
                yield return new object[] { new bool[] { false, false, false, true, false, false, false, false }, 0b00001000 };
                yield return new object[] { new bool[] { false, false, false, false, true, false, false, false }, 0b00010000 };
                yield return new object[] { new bool[] { false, false, false, false, false, true, false, false }, 0b00100000 };
                yield return new object[] { new bool[] { false, false, false, false, false, false, true, false }, 0b01000000 };
                yield return new object[] { new bool[] { false, false, false, false, false, false, false, true }, 0b10000000 };
                yield return new object[] { new bool[] { true, false, false, true, false, false, false, false }, 0b00001001 };
                yield return new object[] { new bool[] { false, true, false, false, true, false, false, true }, 0b10010010 };
                yield return new object[] { new bool[] { false, false, false, true, false, true, false, false }, 0b00101000 };
                yield return new object[] { new bool[] { true, true, true, true, true, true, true, true }, 0b11111111 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
