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
        [InlineData(0x00, 0x00, true, false, false, false)]
        [InlineData(0x01, 0x02, false, false, false, false)]
        [InlineData(0x03, 0x06, false, false, false, false)]
        [InlineData(0x08, 0x10, false, false, true, false)]
        [InlineData(0x88, 0x10, false, false, true, true)]
        [InlineData(0x80, 0x00, true, false, false, true)]
        public void SUMARegA_AContainsResultValue(byte a, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0x87, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegB_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0x80, zeroFlag, negative, halfCarry, carryFlag, 1);

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

            Execute8bitTest(0x81, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegD_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0x82, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegE_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0x83, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegH_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0x84, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMARegL_AContainsResultValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0x85, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMAddrHL_HLAddressContainsIncrementValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0x86, zeroFlag, negative, halfCarry, carryFlag, 2);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(Sum8bitTestData))]
        public void SUMAImpl_AContainsIncrementValue(byte a, byte b, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            bus.SetMemory(b, 0xC001);

            Execute8bitTest(0xC6, zeroFlag, negative, halfCarry, carryFlag, 2, 0xC002);

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

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(expectedCycles, cycles);

            Assert.Equal(pc, cpu.PC);

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
                yield return new object[] { 0x80, 0x80, 0x00, true, false, false, true };
                yield return new object[] { 0x88, 0x88, 0x10, false, false, true, true };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
