using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core
{
    public class Bus
    {
        private readonly CPU cpu;

        //$FF80-$FFFE	Zero Page RAM - 127 bytes
        private readonly byte[] ZPRAM = new byte[0x7F];
        //$FE00-$FE9F OAM - Object Attribute Memory
        private readonly byte[] OAM = new byte[0x400];
        //$8000-$9FFF Video RAM
        private readonly byte[] VRAM = new byte[0x2000];
        //Worker RAM
        //$D000-$DFFF Internal RAM - Bank 1-7 (switchable - CGB only)
        //$C000-$CFFF Internal RAM - Bank 0 (fixed)
        private readonly byte[] WRAM = new byte[0x2000];

        public Bus()
        {
            this.cpu = new CPU(this);
        }

        public byte ReadMemory(UInt32 address)
        {
            switch (address)
            {
                case UInt32 a when (a <= 0xFFFE && a >= 0xFF80):
                    return ZPRAM[address - 0xFF80];
                case UInt32 a when (a <= 0xFE9F && a >= 0xFE00):
                    return OAM[address - 0xFE00];
                case UInt32 a when (a <= 0x9FFF && a >= 0x8000):
                    return VRAM[address - 0x8000];
                case UInt32 a when (a <= 0xDFFF && a >= 0xC000):
                    return WRAM[address - 0xC000];
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Utility command for setting memory values
        /// </summary>
        /// <param name="value"></param>
        /// <param name="address"></param>
        public void SetMemory(byte value, UInt32 address)
        {
            switch(address)
            {
                case UInt32 a when (a <= 0xFFFE && a >= 0xFF80):
                    ZPRAM[address - 0xFF80] = value;
                    break;
                case UInt32 a when (a <= 0xFE9F && a >= 0xFE00):
                    OAM[address - 0xFE00] = value;
                    break;
                case UInt32 a when (a <= 0x9FFF && a >= 0x8000):
                    VRAM[address - 0x8000] = value;
                    break;
                case UInt32 a when (a <= 0xDFFF && a >= 0xC000):
                    WRAM[address - 0xC000] = value;
                    break;
            }
        }

        /// <summary>
        /// Utility command for getting memory value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte GetMemory(UInt32 address)
        {
            switch (address)
            {
                case UInt32 a when (a <= 0xFFFE && a >= 0xFF80):
                    return ZPRAM[address - 0xFF80];
                case UInt32 a when (a <= 0xFE9F && a >= 0xFE00):
                    return OAM[address - 0xFE00];
                case UInt32 a when (a <= 0x9FFF && a >= 0x8000):
                    return VRAM[address - 0x8000];
                case UInt32 a when (a <= 0xDFFF && a >= 0xC000):
                    return WRAM[address - 0xC000];
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Utility function for receiving CPU object
        /// </summary>
        /// <returns></returns>
        public CPU GetCPU()
        {
            return this.cpu;
        }
    }
}
