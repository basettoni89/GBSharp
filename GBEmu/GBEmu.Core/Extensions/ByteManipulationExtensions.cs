using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Extensions
{
    public static class ByteManipulationExtensions
    {
        public static bool GetBit(this byte value, int index)
        {
            return (value & (1 << index)) != 0;
        }

        public static byte SetBit(this byte value, int index)
        {
            return (byte)(value | (1 << index));
        }

        public static byte ResetBit(this byte value, int index)
        {
            return (byte)(value & ~(1 << index));
        }
    }
}
