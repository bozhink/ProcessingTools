namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Core;
    using Contracts.Factories;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly IFileProcessorFactory fileProcessorFactory;
        private readonly IJournal journal;
        private string direcoryName;

        public DirectoryProcessor(string direcoryName, IJournal journal, IFileProcessorFactory fileProcessorFactory)
        {
            if (journal == null)
            {
                throw new ArgumentNullException(nameof(journal));
            }

            if (fileProcessorFactory == null)
            {
                throw new ArgumentNullException(nameof(fileProcessorFactory));
            }

            this.DirectoryName = direcoryName;
            this.journal = journal;
            this.fileProcessorFactory = fileProcessorFactory;
        }

        private string DirectoryName
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
            var matchSupplementaryMaterial = new Regex(@"\-s\d+\Z");

            return Task.Run(() =>
            {
                var exceptions = new ConcurrentQueue<Exception>();

                var initialDirectory = Directory.GetCurrentDirectory();

                Directory.SetCurrentDirectory(this.DirectoryName);

                var xmlFiles = Directory.GetFiles(Directory.GetCurrentDirectory())
                    .Where(f => Path.GetExtension(f).TrimStart('.').ToLower() == "xml" &&
                                !matchSupplementaryMaterial.IsMatch(Path.GetFileNameWithoutExtension(f)))
                    .ToArray();

                Parallel.ForEach(
                    xmlFiles,
                    (fileName, state) =>
                    {
                        try
                        {
                            var fileProcessor = this.fileProcessorFactory.CreateFileProcessor(fileName, journal);
                            fileProcessor.Process().Wait();
                        }
                        catch (Exception e)
                        {
                            exceptions.Enqueue(e);
                            ////state.Break();
                        }
                    });

                Directory.SetCurrentDirectory(initialDirectory);

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions.ToList());
                }
            });
        }
    }
}
