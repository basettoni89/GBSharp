using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GBEmu.Core.Exceptions
{
    public class IllegalInstructionException : KeyNotFoundException
    {
        public IllegalInstructionException(byte opCode) : base($"The opCode 0x{opCode:X2} is not valid")
        {
        }

        public IllegalInstructionException(byte opCode, Exception innerException) : base($"The opCode 0x{opCode:X2} is not valid", innerException)
        {
        }
    }
}
