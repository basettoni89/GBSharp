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
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDA_AContanisAValue(byte value)
        {
            Execute8bitTest(0x3E, value, 2);
            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDB_BContanisAValue(byte value)
        {
            Execute8bitTest(0x06, value, 2);
            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDC_CContanisAValue(byte value)
        {
            Execute8bitTest(0x0E, value, 2);
            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDD_DContanisAValue(byte value)
        {
            Execute8bitTest(0x16, value, 2);
            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDE_EContanisAValue(byte value)
        {
            Execute8bitTest(0x1E, value, 2);
            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDH_HContanisAValue(byte value)
        {
            Execute8bitTest(0x26, value, 2);
            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate8bitTestData))]
        public void LDL_LContanisAValue(byte value)
        {
            Execute8bitTest(0x2E, value, 2);
            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate16bitTestData))]
        public void LDBC_BCContanisAValue(ushort value)
        {
            Execute16bitTest(0x01, value, 3);
            Assert.Equal(value, cpu.BC);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate16bitTestData))]
        public void LDDE_DEContanisAValue(ushort value)
        {
            Execute16bitTest(0x11, value, 3);
            Assert.Equal(value, cpu.DE);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate16bitTestData))]
        public void LDHL_HLContanisAValue(ushort value)
        {
            Execute16bitTest(0x21, value, 3);
            Assert.Equal(value, cpu.HL);
        }

        [Theory]
        [ClassData(typeof(LoadImmediate16bitTestData))]
        public void LDSP_SPContanisAValue(ushort value)
        {
            Execute16bitTest(0x31, value, 3);
            Assert.Equal(value, cpu.SP);
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

        private void Execute16bitTest(byte opcode, ushort value, int expectedCycles)
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(opcode, 0xC000);
            bus.SetMemory((byte)value, 0xC001);
            bus.SetMemory((byte)(value >> 8), 0xC002);

            TestExecution(expectedCycles);

            Assert.Equal(0xC003, cpu.PC);
        }

        class LoadImmediate8bitTestData : IEnumerable<object[]>
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

        class LoadImmediate16bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0x0000 };
                yield return new object[] { 0x0001 };
                yield return new object[] { 0x000F };
                yield return new object[] { 0x1122 };
                yield return new object[] { 0xFF42 };
                yield return new object[] { 0xFFFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
