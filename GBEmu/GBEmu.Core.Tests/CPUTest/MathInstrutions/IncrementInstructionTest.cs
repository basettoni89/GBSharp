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
        [ClassData(typeof(Increment8bitTestData))]
        public void INCA_AContainsIncrementValue(byte actual, byte expected, 
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = actual;

            Execute8bitTest(0x3C, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCB_BContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.B = actual;

            Execute8bitTest(0x04, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.B);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCC_CContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.C = actual;

            Execute8bitTest(0x0C, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.C);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCD_DContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.D = actual;

            Execute8bitTest(0x14, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.D);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCE_EContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.E = actual;

            Execute8bitTest(0x1C, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.E);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCH_HContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.H = actual;

            Execute8bitTest(0x24, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.H);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCL_LContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.L = actual;

            Execute8bitTest(0x2C, zeroFlag, negative, halfCarry, carryFlag, 1);

            Assert.Equal(expected, cpu.L);
        }

        [Theory]
        [ClassData(typeof(Increment8bitTestData))]
        public void INCAddrHL_HLAddressContainsIncrementValue(byte actual, byte expected,
            bool zeroFlag, bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(actual, 0xCC01);

            Execute8bitTest(0x34, zeroFlag, negative, halfCarry, carryFlag, 3);

            Assert.Equal(expected, bus.GetCPU().A);
        }

        [Theory]
        [ClassData(typeof(Increment16bitTestData))]
        public void INCBC_BCContainsIncrementValue(byte actualHI, byte actualLO, byte expectedHI, byte expectedLO)
        {
            cpu.Reset();
            cpu.B = actualHI;
            cpu.C = actualLO;

            Execute16bitTest(0x03, 2);

            Assert.Equal(expectedHI, cpu.B);
            Assert.Equal(expectedLO, cpu.C);
        }

        [Theory]
        [ClassData(typeof(Increment16bitTestData))]
        public void INCDE_DEContainsIncrementValue(byte actualHI, byte actualLO, byte expectedHI, byte expectedLO)
        {
            cpu.Reset();
            cpu.D = actualHI;
            cpu.E = actualLO;

            Execute16bitTest(0x13, 2);

            Assert.Equal(expectedHI, cpu.D);
            Assert.Equal(expectedLO, cpu.E);
        }

        [Theory]
        [ClassData(typeof(Increment16bitTestData))]
        public void INCHL_HLContainsIncrementValue(byte actualHI, byte actualLO, byte expectedHI, byte expectedLO)
        {
            cpu.Reset();
            cpu.H = actualHI;
            cpu.L = actualLO;

            Execute16bitTest(0x23, 2);

            Assert.Equal(expectedHI, cpu.H);
            Assert.Equal(expectedLO, cpu.L);
        }

        [Fact]
        public void INCSP_SPContainsIncrementValue()
        {
            cpu.Reset();
            cpu.SP = 0x0000;

            Execute16bitTest(0x33, 2);

            Assert.Equal(0x0001, cpu.SP);
        }

        private void Execute8bitTest(byte opCode, bool zeroFlag, bool negative, bool halfCarry, bool carryFlag, int expectedCycles)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = !zeroFlag;
            cpu.Flags.N = negative;
            cpu.Flags.H = !halfCarry;
            cpu.Flags.CY = carryFlag;

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

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(negative, cpu.Flags.N);
            Assert.Equal(halfCarry, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
        }

        private void Execute16bitTest(byte opCode, int expectedCycles)
        {
            cpu.PC = 0xC000;

            cpu.Flags.ZF = true;
            cpu.Flags.N = true;
            cpu.Flags.H = true;
            cpu.Flags.CY = true;

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

            Assert.Equal(0xC001, cpu.PC);

            Assert.True(cpu.Flags.ZF);
            Assert.True(cpu.Flags.N);
            Assert.True(cpu.Flags.H);
            Assert.True(cpu.Flags.CY);
        }

        class Increment8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 5, 6, false, false, false, false };
                yield return new object[] { 0, 1, false, false, false, false };
                yield return new object[] { 0b00000111, 0b00001000, false, false, false, false };
                yield return new object[] { 0b00001111, 0b00010000, false, false, true, false };
                yield return new object[] { 0b11111111, 0b00000000, true, false, true, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class Increment16bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0, 0, 0, 1 };
                yield return new object[] { 1, 0, 1, 1 };
                yield return new object[] { 0, 0xFF, 1, 0 };
                yield return new object[] { 0xFF, 0xFF, 0, 0 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
