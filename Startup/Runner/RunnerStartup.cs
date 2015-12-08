namespace Runner
{
    using System;
    using System.Diagnostics;

    public partial class RunnerStartup
    {
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            InitializeMediaTypeDatabase();
            InitializeTaxonomyDatabase();

            Console.WriteLine(timer.Elapsed);
        }
    }
}