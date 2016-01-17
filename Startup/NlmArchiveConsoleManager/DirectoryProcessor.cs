namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class DirectoryProcessor : IProcessor
    {
        private string direcoryName;
        private IJournal journal;
        private ILogger logger;

        public DirectoryProcessor(string direcoryName, IJournal journal)
            : this(direcoryName, journal, null)
        {
        }

        public DirectoryProcessor(string direcoryName, IJournal journal, ILogger logger)
        {
            this.DirectoryName = direcoryName;
            this.journal = journal;
            this.logger = logger;
        }

        public string DirectoryName
        {
            get
            {
                return this.direcoryName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(value);
                }

                if (!Directory.Exists(value))
                {
                    throw new DirectoryNotFoundException(value);
                }

                this.direcoryName = value;
            }
        }

        public Task Process()
        {
            return Task.Run(() =>
            {
                var exceptions = new ConcurrentQueue<Exception>();

                var initialDirectory = Directory.GetCurrentDirectory();

                Directory.SetCurrentDirectory(this.DirectoryName);

                {
                    var files = Directory.GetFiles(Directory.GetCurrentDirectory());

                    var xmlFiles = files.Where(f => Path.GetExtension(f).TrimStart('.') == "xml");

                    Parallel.ForEach(
                        xmlFiles,
                        (fileName, state) =>
                        {
                            try
                            {
                                var fileProcessor = new FileProcessor(fileName, journal, logger);
                                fileProcessor.Process();
                            }
                            catch (Exception e)
                            {
                                exceptions.Enqueue(e);
                                ////state.Break();
                            }
                        });
                }

                Directory.SetCurrentDirectory(initialDirectory);

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions.ToList());
                }
            });
        }
    }
}