using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadIndirectInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDAHL_AContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x7E, addr, value, 2);
            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDBHL_BContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x46, addr, value, 2);
            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDCHL_CContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x4E, addr, value, 2);
            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDDHL_DContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x56, addr, value, 2);
            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDEHL_EContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x5E, addr, value, 2);
            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDHHL_HContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x66, addr, value, 2);
            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect8bitTestData))]
        public void LDLHL_LContanisHLIndirectValue(ushort addr, byte value)
        {
            Execute8bitTest(0x6E, addr, value, 2);
            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadIndirect16bitTestData))]
        public void LDLSP_SPContanisIndirectValue(ushort addr, ushort value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.SP = value;

            bus.SetMemory(0x08, 0xC000);
            bus.SetMemory((byte)addr, 0xC001);
            bus.SetMemory((byte)(addr >> 8), 0xC002);

            TestExecution(5);

            Assert.Equal(0xC003, cpu.PC);

            Assert.Equal((byte)value, bus.GetMemory(addr));
            Assert.Equal((byte)(value >> 8), bus.GetMemory((ushort)(addr + 1)));
        }

        private void Execute8bitTest(byte opcode, ushort addr, byte value, int expectedCycles)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = (byte)(addr >> 8);
            cpu.L = (byte)addr;

            bus.SetMemory(opcode, 0xC000);
            bus.SetMemory(value, addr);

            TestExecution(expectedCycles);

            Assert.Equal(0xC001, cpu.PC);
        }

        public class LoadIndirect8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0xCC01, 0x00 };
                yield return new object[] { 0xCC01, 0x01 };
                yield return new object[] { 0xCC01, 0x20 };
                yield return new object[] { 0xCC01, 0xFF };
                yield return new object[] { 0xC001, 0x00 };
                yield return new object[] { 0xC001, 0x01 };
                yield return new object[] { 0xC001, 0x20 };
                yield return new object[] { 0xC001, 0xFF };
                yield return new object[] { 0xCCFF, 0x00 };
                yield return new object[] { 0xCCFF, 0x01 };
                yield return new object[] { 0xCCFF, 0x20 };
                yield return new object[] { 0xCCFF, 0xFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class LoadIndirect16bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0xCC01, 0x0000 };
                yield return new object[] { 0xCC01, 0x0001 };
                yield return new object[] { 0xCC01, 0x2025 };
                yield return new object[] { 0xCC01, 0xFFFF };
                yield return new object[] { 0xC010, 0x0000 };
                yield return new object[] { 0xC010, 0x0001 };
                yield return new object[] { 0xC010, 0x2025 };
                yield return new object[] { 0xC010, 0xFFFF };
                yield return new object[] { 0xCCFF, 0x0000 };
                yield return new object[] { 0xCCFF, 0x0001 };
                yield return new object[] { 0xCCFF, 0x2025 };
                yield return new object[] { 0xCCFF, 0xFFFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
