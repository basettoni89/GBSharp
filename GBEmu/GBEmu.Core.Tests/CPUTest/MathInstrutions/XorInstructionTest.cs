using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class XorInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public XorInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData(0b00000000, 0b00000000, true, false, false, false)]
        [InlineData(0b00000001, 0b00000000, true, false, false, false)]
        [InlineData(0b00000111, 0b00000000, true, false, false, false)]
        [InlineData(0b11111111, 0b00000000, true, false, false, false)]
        public void XORARegA_AContainsResultValue(byte a, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0xAF, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0xA8, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegC_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0xA9, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegD_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0xAA, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegE_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0xAB, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegH_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0xAC, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORARegL_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0xAD, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORAddrHL_HLAddressContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0xAE, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(Xor8bitTestData))]
        public void XORAImpl_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            bus.SetMemory(b, 0xC001);

            Execute8bitTest(0xEE, zeroFlag, negative, halfCarry, carryFlag, 0xC002);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        private void Execute8bitTest(byte opCode, bool zeroFlag, bool negative,
            bool halfCarry, bool carryFlag, int pc = 0xC001)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = !negative;
            cpu.Flags.H = !halfCarry;
            cpu.Flags.CY = !carryFlag;

            bus.SetMemory(opCode, 0xC000);

            cpu.Clock();

            Assert.Equal(pc, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(negative, cpu.Flags.N);
            Assert.Equal(halfCarry, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
        }

        class Xor8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00, 0x00, 0x00, true, false, false, false };
                yield return new object[] { 0x00, 0x06, 0x06, false, false, false, false };
                yield return new object[] { 0b00000101, 0b00000100, 0b00000001, false, false, false, false };
                yield return new object[] { 0b00000111, 0b00000111, 0b00000000, true, false, false, false };
                yield return new object[] { 0b11111111, 0b11111111, 0b00000000, true, false, false, false };
                yield return new object[] { 0b11111111, 0b10101010, 0b01010101, false, false, false, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
