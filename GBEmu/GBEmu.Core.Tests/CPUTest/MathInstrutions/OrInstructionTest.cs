using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class OrInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [InlineData(0b00000000, 0b00000000, true, false, false, false)]
        [InlineData(0b00000001, 0b00000001, false, false, false, false)]
        [InlineData(0b00000111, 0b00000111, false, false, false, false)]
        [InlineData(0b11111111, 0b11111111, false, false, false, false)]
        public void ORARegA_AContainsResultValue(byte a, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0xB7, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0xB0, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegC_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0xB1, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegD_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0xB2, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegE_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0xB3, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegH_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0xB4, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORARegL_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0xB5, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORAddrHL_HLAddressContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0xB6, zeroFlag, negative, halfCarry, carryFlag, 2);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(Or8bitTestData))]
        public void ORAImpl_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            bus.SetMemory(b, 0xC001);

            Execute8bitTest(0xF6, zeroFlag, negative, halfCarry, carryFlag, 2, 0xC002);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        private void Execute8bitTest(byte opCode, bool zeroFlag, bool negative,
            bool halfCarry, bool carryFlag, int expectedCycles, int pc = 0xC001)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = !negative;
            cpu.Flags.H = !halfCarry;
            cpu.Flags.CY = !carryFlag;

            bus.SetMemory(opCode, 0xC000);

            TestExecution(expectedCycles);

            Assert.Equal(pc, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(negative, cpu.Flags.N);
            Assert.Equal(halfCarry, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
        }

        class Or8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00, 0x00, 0x00, true, false, false, false };
                yield return new object[] { 0x00, 0x06, 0x06, false, false, false, false };
                yield return new object[] { 0b00000101, 0b00000100, 0b00000101, false, false, false, false };
                yield return new object[] { 0b00000111, 0b00000111, 0b00000111, false, false, false, false };
                yield return new object[] { 0b11111111, 0b11111111, 0b11111111, false, false, false, false };
                yield return new object[] { 0b11111111, 0b10101010, 0b11111111, false, false, false, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
