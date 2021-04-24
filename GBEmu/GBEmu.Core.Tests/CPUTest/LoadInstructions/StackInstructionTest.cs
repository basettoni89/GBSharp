using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.CPUTest.LoadInstructions
{
    public class StackInstructionTest : AbstractInstructionTest
    {
        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void PUSHBCImpl_StackContainsValues(byte b, byte c)
        {
            cpu.Reset();

            cpu.B = b;
            cpu.C = c;

            bus.SetMemory(0xC5, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(4);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(b, bus.GetMemory(0xFFFD));
            Assert.Equal(c, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void PUSHDEImpl_StackContainsValues(byte d, byte e)
        {
            cpu.Reset();

            cpu.D = d;
            cpu.E = e;

            bus.SetMemory(0xD5, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(4);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(d, bus.GetMemory(0xFFFD));
            Assert.Equal(e, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void PUSHHLImpl_StackContainsValues(byte h, byte l)
        {
            cpu.Reset();

            cpu.H = h;
            cpu.L = l;

            bus.SetMemory(0xE5, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(4);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(h, bus.GetMemory(0xFFFD));
            Assert.Equal(l, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, false, false, false, false, 0b00000000)]
        [InlineData(0x01, false, false, false, true, 0b00010000)]
        [InlineData(0x10, true, false, true, false, 0b10100000)]
        [InlineData(0x42, true, false, false, false, 0b10000000)]
        [InlineData(0xFF, true, true, true, true, 0b11110000)]
        public void PUSHAFImpl_StackContainsValues(byte a, bool zeroFlag, 
            bool subFlag, bool halfFlag, bool carryFlag, byte f)
        {
            cpu.Reset();

            cpu.A = a;

            cpu.Flags.ZF = zeroFlag;
            cpu.Flags.N = subFlag;
            cpu.Flags.H = halfFlag;
            cpu.Flags.CY = carryFlag;

            bus.SetMemory(0xF5, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(4);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(a, bus.GetMemory(0xFFFD));
            Assert.Equal(f, bus.GetMemory(0xFFFC));

            Assert.Equal(0xFFFC, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void POPBCImpl_RegistersContainsValues(byte b, byte c)
        {
            cpu.Reset();

            bus.SetMemory(c, 0xFFFC);
            bus.SetMemory(b, 0xFFFD);
            cpu.SP = 0xFFFC;

            bus.SetMemory(0xC1, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(3);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(b, cpu.B);
            Assert.Equal(c, cpu.C);

            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void POPDEImpl_RegistersContainsValues(byte d, byte e)
        {
            cpu.Reset();

            bus.SetMemory(e, 0xFFFC);
            bus.SetMemory(d, 0xFFFD);
            cpu.SP = 0xFFFC;

            bus.SetMemory(0xD1, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(3);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(d, cpu.D);
            Assert.Equal(e, cpu.E);

            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, 0x00)]
        [InlineData(0x01, 0x03)]
        [InlineData(0x10, 0x00)]
        [InlineData(0x42, 0x24)]
        [InlineData(0xFF, 0xFF)]
        public void POPHLImpl_RegistersContainsValues(byte h, byte l)
        {
            cpu.Reset();

            bus.SetMemory(l, 0xFFFC);
            bus.SetMemory(h, 0xFFFD);
            cpu.SP = 0xFFFC;

            bus.SetMemory(0xE1, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(3);

            Assert.Equal(0xC001, cpu.PC);

            //Verify pushed to stack
            Assert.Equal(h, cpu.H);
            Assert.Equal(l, cpu.L);

            Assert.Equal(0xFFFE, cpu.SP);
        }

        [Theory]
        [InlineData(0x00, false, false, false, false, 0b00000000)]
        [InlineData(0x01, false, false, false, true, 0b00010000)]
        [InlineData(0x10, true, false, true, false, 0b10100000)]
        [InlineData(0x42, true, false, false, false, 0b10000000)]
        [InlineData(0xFF, true, true, true, true, 0b11110000)]
        public void POPAFImpl_RegistersContainsValues(byte a, bool zeroFlag,
            bool subFlag, bool halfFlag, bool carryFlag, byte f)
        {
            cpu.Reset();

            cpu.A = a;

            bus.SetMemory(f, 0xFFFC);
            bus.SetMemory(a, 0xFFFD);
            cpu.SP = 0xFFFC;

            bus.SetMemory(0xF1, 0xC000);
            cpu.PC = 0xC000;

            TestExecution(3);

            Assert.Equal(0xC001, cpu.PC);

            Assert.Equal(zeroFlag, cpu.Flags.ZF);
            Assert.Equal(subFlag, cpu.Flags.N);
            Assert.Equal(halfFlag, cpu.Flags.H);
            Assert.Equal(carryFlag, cpu.Flags.CY);

            Assert.Equal(0xFFFE, cpu.SP);
        }
    }
}
