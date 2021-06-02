using GBEmu.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core
{
    public class PPU
    {
        public LCDCFlags LCDC { get; }

        public byte SCY { get; set; }

        public byte SCX { get; set; }

        public byte LY { get; set; }

        public byte LYC { get; set; }

        public byte DMA { get; set; }

        public byte BGP { get; set; }

        public byte OBP0 { get; set; }

        public byte OBP1 { get; set; }

        public byte WY { get; set; }

        public byte WX { get; set; }

        public PPU()
        {
            this.LCDC = new LCDCFlags();
        }

        public class LCDCFlags
        {
            /// <summary>
            /// LCD enable
            /// </summary>
            public bool F7 { get; set; }
            /// <summary>
            /// Window tile map area
            /// </summary>
            public bool F6 { get; set; }
            /// <summary>
            /// Window enable
            /// </summary>
            public bool F5 { get; set; }
            /// <summary>
            /// BG and Window tile data area
            /// </summary>
            public bool F4 { get; set; }
            /// <summary>
            /// BG tile map area
            /// </summary>
            public bool F3 { get; set; }
            /// <summary>
            /// OBJ size
            /// </summary>
            public bool F2 { get; set; }
            /// <summary>
            /// OBJ enable
            /// </summary>
            public bool F1 { get; set; }
            /// <summary>
            /// BG and Window enable/priority
            /// </summary>
            public bool F0 { get; set; }

            public void Write(byte value)
            {
                F0 = value.GetBit(0);
                F1 = value.GetBit(1);
                F2 = value.GetBit(2);
                F3 = value.GetBit(3);
                F4 = value.GetBit(4);
                F5 = value.GetBit(5);
                F6 = value.GetBit(6);
                F7 = value.GetBit(7);
            }

            public byte Read()
            {
                return ByteUtil.FromBits(F0, F1, F2, F3, F4, F5, F6, F7);
            }
        }
    }
}
