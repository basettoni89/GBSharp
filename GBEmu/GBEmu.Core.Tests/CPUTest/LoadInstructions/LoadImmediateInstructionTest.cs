using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadImmediateInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDA_AContanisAValue(byte value)
        {
            Execute8bitTest(0x3E, value, 2);
            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDB_BContanisAValue(byte value)
        {
            Execute8bitTest(0x06, value, 2);
            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDC_CContanisAValue(byte value)
        {
            Execute8bitTest(0x0E, value, 2);
            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDD_DContanisAValue(byte value)
        {
            Execute8bitTest(0x16, value, 2);
            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDE_EContanisAValue(byte value)
        {
            Execute8bitTest(0x1E, value, 2);
            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDH_HContanisAValue(byte value)
        {
            Execute8bitTest(0x26, value, 2);
            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadImmediateTestData))]
        public void LDL_LContanisAValue(byte value)
        {
            Execute8bitTest(0x2E, value, 2);
            Assert.Equal(value, cpu.L);
        }

        private void Execute8bitTest(byte opcode, byte value, int expectedCycles)
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(opcode, 0xC000);
            bus.SetMemory(value, 0xC001);

            TestExecution(expectedCycles);

            Assert.Equal(0xC002, cpu.PC);
        }

        class LoadImmediateTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x00 };
                yield return new object[] { 0x01 };
                yield return new object[] { 0x0F };
                yield return new object[] { 0x22 };
                yield return new object[] { 0x42 };
                yield return new object[] { 0xFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
