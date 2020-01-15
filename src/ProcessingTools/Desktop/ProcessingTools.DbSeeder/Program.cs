// <copyright file="Program.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Diagnostics;
    using global::Ninject;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.DbSeeder.Settings;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            using (var kernel = NinjectConfig.CreateKernel())
            {
                var engine = kernel.Get<IEngine>();

                try
                {
                    engine.RunAsync(args).Wait();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.ToString());
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(timer.Elapsed);
            Console.ResetColor();
        }
    }
}
