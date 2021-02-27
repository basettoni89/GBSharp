using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class IncrementInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public IncrementInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [ClassData(typeof(IncrementTestData))]
        public void INCA_AContainsIncrementValue(byte actual, byte expected, 
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = actual;

            ExecuteTest(0x3C, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(IncrementTestData))]
        public void INCB_BContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.B = actual;

            ExecuteTest(0x04, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.B);
        }

        private void ExecuteTest(byte opCode, bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = negative;
            cpu.Flags.H = !halfCarry;
            cpu.Flags.CY = !carryFlag;

            bus.SetMemory(opCode, 0xC000);

            cpu.Clock();

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(negative, cpu.Flags.N);
            Assert.Equal(halfCarry, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
        }

        class IncrementTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 5, 6, false, false, false, false };
                yield return new object[] { 0, 1, false, false, false, false };
                yield return new object[] { 0b00000111, 0b00001000, false, false, true, false };
                yield return new object[] { 0b00001111, 0b00010000, false, false, true, false };
                yield return new object[] { 0b11111111, 0b00000000, true, false, true, true };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
