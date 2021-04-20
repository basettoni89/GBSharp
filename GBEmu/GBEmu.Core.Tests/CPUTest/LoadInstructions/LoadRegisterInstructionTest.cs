using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class LoadRegisterInstructionTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public LoadRegisterInstructionTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterA_AContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x7F, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterB_AContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x78, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterC_AContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x79, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterD_AContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x7A, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterE_AContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x7B, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterH_AContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x7C, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDARegisterL_AContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x7D, 1);

            Assert.Equal(value, cpu.A);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterA_BContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x47, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterB_BContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x40, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterC_BContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x41, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterD_BContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x42, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterE_BContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x43, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterH_BContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x44, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDBRegisterL_BContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x45, 1);

            Assert.Equal(value, cpu.B);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterA_CContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x4F, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterB_CContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x48, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterC_CContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x49, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterD_CContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x4A, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterE_CContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x4B, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterH_CContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x4C, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDCRegisterL_CContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x4D, 1);

            Assert.Equal(value, cpu.C);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterA_DContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x57, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterB_DContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x50, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterC_DContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x51, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterD_DContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x52, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterE_DContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x53, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterH_DContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x54, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDDRegisterL_DContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x55, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterA_EContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x5F, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterB_EContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x58, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterC_EContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x59, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterD_EContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x5A, 1);

            Assert.Equal(value, cpu.D);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterE_EContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x5B, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterH_EContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x5C, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDERegisterL_EContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x5D, 1);

            Assert.Equal(value, cpu.E);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterA_HContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x67, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterB_HContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x60, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterC_HContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x61, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LHRegisterD_HContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x62, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterE_HContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x63, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterH_HContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x64, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDHRegisterL_HContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x65, 1);

            Assert.Equal(value, cpu.H);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterA_LContanisRegisterAValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.A = value;

            Execute8bitTest(0x6F, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterB_LContanisRegisterBValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.B = value;

            Execute8bitTest(0x68, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterC_LContanisRegisterCValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.C = value;

            Execute8bitTest(0x69, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LLRegisterD_LContanisRegisterDValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.D = value;

            Execute8bitTest(0x6A, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterE_LContanisRegisterEValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.E = value;

            Execute8bitTest(0x6B, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterH_LContanisRegisterHValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.H = value;

            Execute8bitTest(0x6C, 1);

            Assert.Equal(value, cpu.L);
        }

        [Theory]
        [ClassData(typeof(LoadRegisterTestData))]
        public void LDLRegisterL_LContanisRegisterLValue(byte value)
        {
            cpu.Reset();

            cpu.PC = 0xC000;
            cpu.L = value;

            Execute8bitTest(0x6D, 1);

            Assert.Equal(value, cpu.L);
        }

        private void Execute8bitTest(byte opcode, int expectedCycles)
        {
            bus.SetMemory(opcode, 0xC000);

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
        }

        class LoadRegisterTestData : IEnumerable<object[]>
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
