using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class SumInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public SumInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0x80, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegC_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0x81, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(expected, cpu.A);
        }

        private void Execute8bitTest(byte opCode, bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = !negative;
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

        class Sum8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00, 0x01, 0x01, false, false, false, false };
                yield return new object[] { 0x00, 0x06, 0x06, false, false, false, false };
                yield return new object[] { 0x03, 0x04, 0x07, false, false, false, false };
                yield return new object[] { 0x09, 0x09, 0x12, false, false, true, false };
                yield return new object[] { 0xFE, 0x03, 0x01, false, false, true, true };
                yield return new object[] { 0xFF, 0x01, 0x00, true, false, true, true };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
