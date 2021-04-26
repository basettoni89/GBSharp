using GBEmu.Core.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.DeviceTest
{
    public class ExecutionTest : AbstractROMTest
    {
        [Fact]
        public void ExecuteSumROM_VerifyExecution()
        {
            cpu.Reset();

            byte[][] banks = LoadROM(TestRom.Sum);
            bus.LoadRomBank(0, banks[0]);

            const int instructions = 3;

            for (int i = 0; i < instructions; i++)
            {
                ExecuteInstruction();
            }

            Assert.Equal(0x0152, cpu.PC);
            Assert.Equal(0x01, cpu.A);
        }

        [Fact]
        public void ExecuteSumAndJumpROM_VerifyExecution()
        {
            cpu.Reset();

            byte[][] banks = LoadROM(TestRom.Sum);
            bus.LoadRomBank(0, banks[0]);

            const int instructions = 5;

            for (int i = 0; i < instructions; i++)
            {
                ExecuteInstruction();
            }

            Assert.Equal(0x0152, cpu.PC);
            Assert.Equal(0x02, cpu.A);
        }
    }
}
