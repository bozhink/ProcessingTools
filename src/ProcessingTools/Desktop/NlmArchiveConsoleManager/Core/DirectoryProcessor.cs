namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories;

    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly IProcessorFactory processorFactory;
        private readonly IJournalMeta journalMeta;
        private string direcoryName;

        public DirectoryProcessor(string direcoryName, IJournalMeta journalMeta, IProcessorFactory processorFactory)
        {
            this.DirectoryName = direcoryName;
            this.journalMeta = journalMeta ?? throw new ArgumentNullException(nameof(journalMeta));
            this.processorFactory = processorFactory ?? throw new ArgumentNullException(nameof(processorFactory));
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

        public async Task Process()
        {
            var initialDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(this.DirectoryName);

            await this.ProcessDirectory()
                .ContinueWith(_ =>
                {
                    _.Wait();
#if CreateZip
                    this.CreateZipFile();
#endif
                })
                .ConfigureAwait(false);

            Directory.SetCurrentDirectory(initialDirectory);
        }

#if CreateZip
        private void CreateZipFile()
        {
            var zipDestinationDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var zipFileName = this.DirectoryName.Trim(new char[] { ':', '/', '\\' });
            var destinationArchiveFileName = Path.Combine(zipDestinationDirectory, $"{zipFileName}.{FileConstants.ZipFileExtension}");

            ZipFile.CreateFromDirectory(this.DirectoryName, destinationArchiveFileName, CompressionLevel.Optimal, false);
        }
#endif

        private async Task ProcessDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            var xmlFiles = this.GetFiles(directory);
            var exceptions = new ConcurrentQueue<Exception>();

            foreach (var fileName in xmlFiles)
            {
                try
                {
                    var fileProcessor = this.processorFactory.CreateFileProcessor(fileName, this.journalMeta);
                    await fileProcessor.Process().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToList());
            }
        }

        private string[] GetFiles(string directory)
        {
            var matchSupplementaryMaterial = new Regex(@"\-s\d+\Z");

            var files = Directory.GetFiles(directory)
                .Where(f => Path.GetExtension(f).TrimStart('.').ToLowerInvariant() == FileConstants.XmlFileExtension &&
                            !matchSupplementaryMaterial.IsMatch(Path.GetFileNameWithoutExtension(f)))
                .ToArray();

            return files;
        }
    }
}
