namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using Contracts.Core;
    using Ninject;
    using Settings;

    public class Startup
    {
        public static void Main(string[] args)
        {
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
        }
    }
}
