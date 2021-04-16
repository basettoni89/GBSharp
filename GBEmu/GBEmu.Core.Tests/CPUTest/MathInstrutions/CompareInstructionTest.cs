using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.MathInstrutions
{
    public class CompareInstructionTest : IDisposable
    {
        readonly CPU cpu;
        readonly Bus bus;

        public CompareInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData(0x00, true, true, false, false)]
        [InlineData(0x01, true, true, false, false)]
        [InlineData(0x32, true, true, false, false)]
        [InlineData(0xFF, true, true, false, false)]
        public void CPA_FlagsUpdated(byte a, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;

            Execute8bitTest(0xBF, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPB_FlagsUpdated(byte a, byte b, bool zeroFlag, 
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.B = b;

            Execute8bitTest(0xB8, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.B);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPC_FlagsUpdated(byte a, byte b, bool zeroFlag, 
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.C = b;

            Execute8bitTest(0xB9, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.C);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPD_FlagsUpdated(byte a, byte b, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.D = b;

            Execute8bitTest(0xBA, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.D);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPE_FlagsUpdated(byte a, byte b, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.E = b;

            Execute8bitTest(0xBB, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.E);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPH_FlagsUpdated(byte a, byte b, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = b;

            Execute8bitTest(0xBC, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.H);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPL_FlagsUpdated(byte a, byte b, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.L = b;

            Execute8bitTest(0xBD, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
            Assert.Equal(b, cpu.L);
        }

        [Theory]
        [ClassData(typeof(Compare8bitTestData))]
        public void CPAddrHL_FlagsUpdated(byte a, byte b, bool zeroFlag,
            bool negative, bool halfCarry, bool carryFlag)
        {
            cpu.Reset();
            cpu.A = a;
            cpu.H = 0xCC;
            cpu.L = 0x01;

            bus.SetMemory(b, 0xCC01);

            Execute8bitTest(0xBE, zeroFlag, negative, halfCarry, carryFlag);

            Assert.Equal(a, cpu.A);
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

        class Compare8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x02, 0x01, false, true, false, false };
                yield return new object[] { 0x01, 0x01, true, true, false, false };
                yield return new object[] { 0x07, 0x04, false, true, false, false };
                yield return new object[] { 0x11, 0x09, false, true, true, false };
                yield return new object[] { 0xF3, 0x0F, false, true, true, false };
                yield return new object[] { 0xFF, 0x03, false, true, false, false };
                yield return new object[] { 0x00, 0x01, false, true, true, true };
                yield return new object[] { 0x80, 0x80, true, true, false, false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
