namespace ProcessingTools.NlmArchiveConsoleManager
{
    using Contracts;
    using Loggers;
    using Models;
    using ProcessingTools.Contracts.Log;
    using System;
    using System.IO;
    using System.Linq;

    public class Startup
    {
        private static ILogger logger = new TextWriterLogger();

        private static IJournal journal = new Journal
        {
        };

        public static void Main(string[] args)
        {
            var directories = args.Select(a => Path.GetDirectoryName(a));

            foreach (var directoryName in directories)
            {
                Console.WriteLine(directoryName);

                var direcoryProcessor = new DirectoryProcessor(directoryName, journal, logger);
                direcoryProcessor.Process();
            }
        }
    }
}
