using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest
{
    public class CPUBaseTest : IDisposable
    {

        readonly CPU cpu;
        readonly Bus bus;

        public CPUBaseTest()
        {
            this.bus = new Bus();
            this.cpu = bus.GetCPU();
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Reset_ReturnToStartPC()
        {
            cpu.PC = 0x0200;
            cpu.SP = 0x3000;

            cpu.Reset();

            Assert.Equal(0x0100, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Fact]
        public void NOOP_PCNextInstructionTest()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x00, 0xC000);

            int cycles = 0;

            do
            {
                cpu.Clock();
                cycles++;
                if (cycles > 100)
                    break;
            } while (cpu.Complete);

            Assert.Equal(1, cycles);

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Fact]
        public void FetchData_GetMemoryValue()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0x42, 0xC000);

            byte value = cpu.Fetch();

            Assert.Equal(0xC001, cpu.PC);
            Assert.Equal(0x42, value);
        }

        [Fact]
        public void Push8bitValueOntoStack_NewValueInMemory()
        {
            cpu.Reset();

            cpu.SP = 0xFFFE;

            cpu.Push(0x42);

            Assert.Equal(0x42, bus.GetMemory(0xFFFD));
            Assert.Equal(0xFFFD, cpu.SP);
        }

        [Fact]
        public void Push8bitValueOntoStack_MultipleValues()
        {
            cpu.Reset();

            cpu.SP = 0xFFFE;

            cpu.Push(0x42);
            cpu.Push(0x35);
            cpu.Push(0xFF);

            Assert.Equal(0x42, bus.GetMemory(0xFFFD));
            Assert.Equal(0x35, bus.GetMemory(0xFFFC));
            Assert.Equal(0xFF, bus.GetMemory(0xFFFB));

            Assert.Equal(0xFFFB, cpu.SP);
        }

        [Theory]
        [InlineData(0x0042)]
        [InlineData(0x0C68)]
        [InlineData(0xFFFF)]
        [InlineData(0x0000)]
        public void Push16bitValueOntoStack_NewValueInMemory(ushort value)
        {
            cpu.Reset();

            cpu.SP = 0xFFFE;

            cpu.Push(value);

            Assert.Equal((byte)(value >> 8), bus.GetMemory(0xFFFD));
            Assert.Equal((byte)value, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }

        [Fact]
        public void Push16bitValueOntoStack_MultipleValues()
        {
            cpu.Reset();

            cpu.SP = 0xFFFE;

            cpu.Push(0x0C42);
            cpu.Push(0x0C35);
            cpu.Push(0xFFFF);

            Assert.Equal(0x0C, bus.GetMemory(0xFFFD));
            Assert.Equal(0x42, bus.GetMemory(0xFFFC));

            Assert.Equal(0x0C, bus.GetMemory(0xFFFB));
            Assert.Equal(0x35, bus.GetMemory(0xFFFA));

            Assert.Equal(0xFF, bus.GetMemory(0xFFF9));
            Assert.Equal(0xFF, bus.GetMemory(0xFFF8));

            Assert.Equal(0xFFF8, cpu.SP);
        }
    }
}
