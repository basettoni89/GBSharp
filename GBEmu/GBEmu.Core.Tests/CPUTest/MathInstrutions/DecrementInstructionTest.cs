using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class DecrementInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public DecrementInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECA_AContainsDecrementValue(byte actual, byte expected, 
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = actual;

            Execute8bitTest(0x3D, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECB_BContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.B = actual;

            Execute8bitTest(0x05, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.B);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECC_CContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.C = actual;

            Execute8bitTest(0x0D, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.C);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECD_DContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.D = actual;

            Execute8bitTest(0x15, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.D);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECE_EContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.E = actual;

            Execute8bitTest(0x1D, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.E);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECH_HContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.H = actual;

            Execute8bitTest(0x25, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.H);
        }

        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECAddrHL_HLAddressContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(actual, 0xCC01);

            Execute8bitTest(0x35, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, bus.GetCPU().A);
        }
        [Theory]
        [ClassData(typeof(Decrement8bitTestData))]
        public void DECL_LContainsDecrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.L = actual;

            Execute8bitTest(0x2D, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.L);
        }

        private void Execute8bitTest(byte opCode, bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = negative;
            cpu.Flags.H = !halfCarry;
            cpu.Flags.CY = carryFlag;

            bus.SetMemory(opCode, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(negative, cpu.Flags.N);
            Assert.Equal(halfCarry, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
        }

        class Decrement8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 5, 4, false, true, false, false };
                yield return new object[] { 1, 0, true, true, false, false };
                yield return new object[] { 0b00001000, 0b00000111, false, true, false, false };
                yield return new object[] { 0b00010000, 0b00001111, false, true, true, false };
                yield return new object[] { 0b00000000, 0b11111111, false, true, true, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
