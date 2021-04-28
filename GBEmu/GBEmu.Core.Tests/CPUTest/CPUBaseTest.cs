using GBEmu.Core.Exceptions;
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
        public void FetchInstruction_ThrowIllegalInstructionException()
        {
            cpu.Reset();

            cpu.PC = 0xC000;

            bus.SetMemory(0xCB, 0xC000);

            IllegalInstructionException exception = Assert.Throws<IllegalInstructionException>(() => cpu.Clock());
            Assert.Equal("The opCode 0xCB is not valid", exception.Message);
        }

        [Theory]
        [InlineData(true, false, false, false, 0b10000000)]
        [InlineData(false, true, false, false, 0b01000000)]
        [InlineData(false, false, true, false, 0b00100000)]
        [InlineData(false, false, false, true, 0b00010000)]
        [InlineData(false, false, false, false, 0b00000000)]
        [InlineData(true, false, true, false, 0b10100000)]
        [InlineData(true, true, true, true, 0b11110000)]
        public void GetFRegister(bool zeroFlag, bool subFlag, 
            bool halfFlag, bool carryFlag, byte value)
        {
            cpu.Reset();

            cpu.Flags.ZF = zeroFlag;
            cpu.Flags.N = subFlag;
            cpu.Flags.H = halfFlag;
            cpu.Flags.CY = carryFlag;

            Assert.Equal(value, cpu.F);
        }

        [Theory]
        [InlineData(true, false, false, false, 0b10000000)]
        [InlineData(false, true, false, false, 0b01000000)]
        [InlineData(false, false, true, false, 0b00100000)]
        [InlineData(false, false, false, true, 0b00010000)]
        [InlineData(false, false, false, false, 0b00000000)]
        [InlineData(true, false, true, false, 0b10100000)]
        [InlineData(true, true, true, true, 0b11110000)]
        public void SetFRegister(bool zeroFlag, bool subFlag,
            bool halfFlag, bool carryFlag, byte value)
        {
            cpu.Reset();

            cpu.F = value;

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(subFlag, cpu.Flags.N);
            Assert.Equal(halfFlag, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);
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


        [Theory]
        [InlineData(0x42)]
        [InlineData(0x68)]
        [InlineData(0xFF)]
        [InlineData(0x00)]
        public void PopValueFromStack_ReadValue(byte value)
        {
            cpu.Reset();

            cpu.SP = 0xFFFD;

            bus.SetMemory(value, 0xFFFD);

            byte readValue = cpu.Pop();

            Assert.Equal(value, readValue);

            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Fact]
        public void PopValueOntoStack_MultipleValues()
        {
            cpu.Reset();

            cpu.SP = 0xFFFB;

            bus.SetMemory(0x42, 0xFFFB);
            bus.SetMemory(0x35, 0xFFFC);
            bus.SetMemory(0xFF, 0xFFFD);

            byte readValue = 0;

            readValue = cpu.Pop();
            Assert.Equal(0x42, readValue);
            Assert.Equal(0xFFFC, cpu.SP);

            readValue = cpu.Pop();
            Assert.Equal(0x35, readValue);
            Assert.Equal(0xFFFD, cpu.SP);

            readValue = cpu.Pop();
            Assert.Equal(0xFF, readValue);
            Assert.Equal(0xFFFE, cpu.SP);
        }
    }
}
