using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class SubtractionInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [InlineData(0x00, 0x00, true, true, false, false)]
        [InlineData(0x01, 0x00, true, true, false, false)]
        [InlineData(0x32, 0x00, true, true, false, false)]
        [InlineData(0xFF, 0x00, true, true, false, false)]
        public void SUBARegA_AContainsResultValue(byte a, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0x97, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0x90, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegC_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0x91, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegD_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0x92, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegE_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0x93, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegH_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0x94, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBARegL_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0x95, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBAddrHL_HLAddressContainsDecrementValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0x96, zeroFlag, negative, halfCarry, carryFlag, 2);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(Subtraction8bitTestData))]
        public void SUBAImpl_AContainsDecrementValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            bus.SetMemory(b, 0xC001);

            Execute8bitTest(0xD6, zeroFlag, negative, halfCarry, carryFlag, 2, 0xC002);

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

        class Subtraction8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x02, 0x01, 0x01, false, true, false, false };
                yield return new object[] { 0x01, 0x01, 0x00, true, true, false, false };
                yield return new object[] { 0x07, 0x04, 0x03, false, true, false, false };
                yield return new object[] { 0x11, 0x09, 0x08, false, true, true, false };
                yield return new object[] { 0xF3, 0x0F, 0xE4, false, true, true, false };
                yield return new object[] { 0xFF, 0x03, 0xFC, false, true, false, false };
                yield return new object[] { 0x00, 0x01, 0xFF, false, true, true, true };
                yield return new object[] { 0x80, 0x80, 0x00, true, true, false, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
