using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.Core.Tests.Utility
{
    public class TestRom
    {
        public static TestRom Empty = new TestRom("Empty", "./Assets/Roms/Empty.gb");
        public static TestRom Sum = new TestRom("Sum", "./Assets/Roms/Sum.gb");

        public string Name { get; }
        public string Path { get; }

        private TestRom(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
