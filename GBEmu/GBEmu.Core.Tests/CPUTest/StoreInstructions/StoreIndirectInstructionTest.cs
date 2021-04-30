using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.StoreInstructions
{
    public class StoreIndirectInstructionTest : AbstractInstructionTest
    {

        [Theory]
        [ClassData(typeof(StoreIndirect8bitTestData))]
        public void LDAIndBC_RAMContanisAValue(ushort addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            cpu.BC = addr;

            bus.SetMemory(0x02, 0xC000);

            TestExecution(2);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(value, bus.GetMemory(addr));
        }

        [Theory]
        [ClassData(typeof(StoreIndirect8bitTestData))]
        public void LDAIndDE_RAMContanisAValue(ushort addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            cpu.DE = addr;

            bus.SetMemory(0x12, 0xC000);

            TestExecution(2);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(value, bus.GetMemory(addr));
        }

        [Theory]
        [ClassData(typeof(StoreIndirect8bitTestData))]
        public void LDAIndHLP_RAMContanisAValue(ushort addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            cpu.HL = addr;

            bus.SetMemory(0x22, 0xC000);

            TestExecution(2);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(addr + 1, cpu.HL);
            Assert.Equal(value, bus.GetMemory(addr));
        }

        [Theory]
        [ClassData(typeof(StoreIndirect8bitTestData))]
        public void LDAIndHLM_RAMContanisAValue(ushort addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            cpu.HL = addr;

            bus.SetMemory(0x32, 0xC000);

            TestExecution(2);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(addr - 1, cpu.HL);
            Assert.Equal(value, bus.GetMemory(addr));
        }

        [Theory]
        [ClassData(typeof(StoreIndirect8bitTestData))]
        public void LDIndHL_RAMContanisValue(ushort addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            cpu.HL = addr;

            bus.SetMemory(0x36, 0xC000);
            bus.SetMemory(value, 0xC001);

            TestExecution(3);

            Assert.Equal(0xC002, cpu.PC);

            Assert.Equal(value, bus.GetMemory(addr));
        }

        [Theory]
        [InlineData(0x01, 0x00)]
        [InlineData(0x01, 0x01)]
        [InlineData(0x01, 0x25)]
        [InlineData(0x01, 0xFF)]
        [InlineData(0x81, 0x00)]
        [InlineData(0x81, 0x01)]
        [InlineData(0x81, 0x25)]
        [InlineData(0x81, 0xFF)]
        [InlineData(0xFF, 0x00)]
        [InlineData(0xFF, 0x01)]
        [InlineData(0xFF, 0x25)]
        [InlineData(0xFF, 0xFF)]
        public void LDAInd_RAMContanisAValue(byte addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            cpu.A = value;

            bus.SetMemory(0xE0, 0xC000);
            bus.SetMemory(addr, 0xC001);

            TestExecution(3);

            Assert.Equal(0xC002, cpu.PC);

            Assert.Equal(value, bus.GetMemory((ushort)(0xFF00 | addr)));
        }

        [Theory]
        [InlineData(0x01, 0x00)]
        [InlineData(0x01, 0x01)]
        [InlineData(0x01, 0x25)]
        [InlineData(0x01, 0xFF)]
        [InlineData(0x81, 0x00)]
        [InlineData(0x81, 0x01)]
        [InlineData(0x81, 0x25)]
        [InlineData(0x81, 0xFF)]
        [InlineData(0xFF, 0x00)]
        [InlineData(0xFF, 0x01)]
        [InlineData(0xFF, 0x25)]
        [InlineData(0xFF, 0xFF)]
        public void LDAIndC_RAMContanisAValue(byte addr, byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            cpu.A = value;
            cpu.C = addr;

            bus.SetMemory(0xE2, 0xC000);

            TestExecution(2);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(value, bus.GetMemory((ushort)(0xFF00 | addr)));
        }

        [Theory]
        [ClassData(typeof(StoreIndirect16bitTestData))]
        public void LDSP_RAMContanisSPValue(ushort addr, ushort value)
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

        public class StoreIndirect8bitTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 0xCC01, 0x00 };
                yield return new object[] { 0xCC01, 0x01 };
                yield return new object[] { 0xCC01, 0x25 };
                yield return new object[] { 0xCC01, 0xFF };
                yield return new object[] { 0xC010, 0x00 };
                yield return new object[] { 0xC010, 0x01 };
                yield return new object[] { 0xC010, 0x25 };
                yield return new object[] { 0xC010, 0xFF };
                yield return new object[] { 0xCCFF, 0x00 };
                yield return new object[] { 0xCCFF, 0x01 };
                yield return new object[] { 0xCCFF, 0x25 };
                yield return new object[] { 0xCCFF, 0xFF };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class StoreIndirect16bitTestData : IEnumerable<object[]>
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
