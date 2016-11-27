namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Diagnostics;
    using Core;

    public static class Startup
    {
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var runner = new Engine();
            runner.Run(args).Wait();

            Console.WriteLine(timer.Elapsed);
        }
    }
}
