using GBEmu.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GBEmu.Core.Tests.Extensions
{
    public class ByteManipulationExtensionTest : IDisposable
    {
        public void Dispose()
        {
        }

        [Theory]
        [InlineData(0b00000000, 0b00000001, 0)]
        [InlineData(0b00000000, 0b00000100, 2)]
        [InlineData(0b00010000, 0b00010000, 4)]
        [InlineData(0b10101010, 0b11101010, 6)]
        [InlineData(0b11111111, 0b11111111, 6)]
        public void SetBitTest(byte current, byte expected, int index)
        {
            byte result = current.SetBit(index);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0b00000000, 0b00000000, 0)]
        [InlineData(0b00010000, 0b00000000, 4)]
        [InlineData(0b11111111, 0b11110111, 3)]
        public void ResetBitTest(byte current, byte expected, int index)
        {
            byte result = current.ResetBit(index);
            Assert.Equal(expected, result);
        }
    }
}
