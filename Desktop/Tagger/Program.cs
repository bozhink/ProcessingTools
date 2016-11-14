namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using Contracts;
    using Ninject;
    using Settings;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                using (var kernel = NinjectConfig.CreateKernel())
                {
                    var startup = kernel.Get<IStartup>();
                    startup.Run(args);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Main timer {0}.", mainTimer.Elapsed);
            Console.ResetColor();
        }
    }
}
