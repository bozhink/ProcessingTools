﻿// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using global::Ninject;
    using ProcessingTools.Tagger.Contracts;
    using ProcessingTools.Tagger.Settings;

    /// <summary>
    /// Program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">CLI arguments.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                using (var kernel = NinjectConfig.CreateKernel())
                {
                    var engine = kernel.Get<IEngine>();
                    engine.Run(args);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Main timer {mainTimer.Elapsed}.");
            Console.ResetColor();
        }
    }
}
