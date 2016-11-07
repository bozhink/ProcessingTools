namespace ProcessingTools.Tagger
{
    using System;
    using System.Diagnostics;
    using Contracts;
    using Contracts.Controllers;
    using Core;
    using Ninject;

    public static class Program
    {
        internal static Func<Type, ITaggerController> ControllerFactory { get; private set; } = null;

        public static void Main(string[] args)
        {
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();

            try
            {
                using (var kernel = NinjectConfig.CreateKernel())
                {
                    ControllerFactory = t => (ITaggerController)kernel.Get(t);

                    DI.Kernel = kernel;

                    var startup = kernel.Get<IStartup>();
                    startup.Run(args);

                    ControllerFactory = null;
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
