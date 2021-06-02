using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Extensions
{
    public class ByteUtil
    {
        public static byte FromBits(params bool[] bits)
        {
            byte value = 0;

            for(int i=0; i < bits.Length; i++)
            {
                if (bits[i])
                    value = value.SetBit(i);
            }

            return value;
        }
    }
}
