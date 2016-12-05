﻿using System;
using ProcessingTools.Contracts.Types;
using ProcessingTools.Loggers.Contracts;

namespace ProcessingTools.Loggers.Loggers
{
    public class ConsoleLogger : TextWriterLogger, IConsoleLogger
    {
        public ConsoleLogger()
            : base(Console.Out)
        {
        }

        protected override void ResetLogTypeColor()
        {
            Console.ResetColor();
        }

        protected override void SetLogTypeColor(LogType type)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case LogType.Exception:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    break;
            }
        }
    }
}
