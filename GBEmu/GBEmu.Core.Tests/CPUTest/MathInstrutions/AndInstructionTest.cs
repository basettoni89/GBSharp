using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class AndInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public AndInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData(0b00000000, 0b00000000, true, false, true, false)]
        [InlineData(0b00000001, 0b00000001, false, false, true, false)]
        [InlineData(0b00000111, 0b00000111, false, false, true, false)]
        [InlineData(0b11111111, 0b11111111, false, false, true, false)]
        public void ANDARegA_AContainsResultValue(byte a, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0xA7, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0xA0, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegC_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0xA1, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegD_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0xA2, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegE_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0xA3, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegH_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0xA4, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDARegL_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0xA5, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDAddrHL_HLAddressContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0xA6, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(And8bitTestData))]
        public void ANDAImpl_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            bus.SetMemory(b, 0xC001);

            Execute8bitTest(0xE6, zeroFlag, negative, halfCarry, carryFlag, 0xC002);

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

        class And8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00, 0x00, 0x00, true, false, true, false };
                yield return new object[] { 0x00, 0x06, 0x00, true, false, true, false };
                yield return new object[] { 0b00000101, 0b00000100, 0b00000100, false, false, true, false };
                yield return new object[] { 0b00000111, 0b00000111, 0b00000111, false, false, true, false };
                yield return new object[] { 0b11111111, 0b11111111, 0b11111111, false, false, true, false };
                yield return new object[] { 0b11111111, 0b10101010, 0b10101010, false, false, true, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
