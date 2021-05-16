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
            public bool _7 { get; set; }
            /// <summary>
            /// Window tile map area
            /// </summary>
            public bool _6 { get; set; }
            /// <summary>
            /// Window enable
            /// </summary>
            public bool _5 { get; set; }
            /// <summary>
            /// BG and Window tile data area
            /// </summary>
            public bool _4 { get; set; }
            /// <summary>
            /// BG tile map area
            /// </summary>
            public bool _3 { get; set; }
            /// <summary>
            /// OBJ size
            /// </summary>
            public bool _2 { get; set; }
            /// <summary>
            /// OBJ enable
            /// </summary>
            public bool _1 { get; set; }
            /// <summary>
            /// BG and Window enable/priority
            /// </summary>
            public bool _0 { get; set; }
        }
    }
}
