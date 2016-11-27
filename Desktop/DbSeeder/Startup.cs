namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Diagnostics;
    using Contracts.Core;
    using Ninject;
    using Settings;

    public static class Startup
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
                    engine.Run(args).Wait();
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
