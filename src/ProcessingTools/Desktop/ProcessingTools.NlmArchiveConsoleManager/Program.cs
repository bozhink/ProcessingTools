// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using System.Diagnostics;
    using global::Ninject;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;
    using ProcessingTools.NlmArchiveConsoleManager.Settings;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                using (var kernel = NinjectConfig.CreateKernel())
                {
                    var engine = kernel.Get<IEngine>();
                    engine.Run(args).Wait();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }

            Console.WriteLine("Elapsed time: {0}", timer.Elapsed);
        }
    }
}
