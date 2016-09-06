namespace ProcessingTools.DbSeeder
{
    using System;
    using System.Diagnostics;

    public static class DbSeederStartup
    {
        public static void Main()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var runner = new Runner();
            runner.Run().Wait();

            Console.WriteLine(timer.Elapsed);
        }
    }
}
