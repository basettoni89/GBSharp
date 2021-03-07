using System;
using System.Collections.Generic;
using System.Text;

namespace GBEmu.CLI.CPUDebug
{
    public abstract class Command
    {
        public static string Name => string.Empty;

        public abstract string Description { get; }

        protected readonly ConsoleHandler handler;

        public Command(ConsoleHandler handler)
        {
            this.handler = handler;
        }

        public abstract void Execute();
    }
}
