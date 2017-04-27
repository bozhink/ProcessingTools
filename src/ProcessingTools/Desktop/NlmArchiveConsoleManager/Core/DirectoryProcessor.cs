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
    using Contracts.Core;
    using Contracts.Factories;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Documents;

    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly IProcessorFactory processorFactory;
        private readonly IJournalMeta journalMeta;
        private string direcoryName;

        public DirectoryProcessor(string direcoryName, IJournalMeta journalMeta, IProcessorFactory processorFactory)
        {
            if (journalMeta == null)
            {
                throw new ArgumentNullException(nameof(journalMeta));
            }

            if (processorFactory == null)
            {
                throw new ArgumentNullException(nameof(processorFactory));
            }

            this.DirectoryName = direcoryName;
            this.journalMeta = journalMeta;
            this.processorFactory = processorFactory;
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
                    ////this.CreateZipFile();
                });

            Directory.SetCurrentDirectory(initialDirectory);
        }

        private void CreateZipFile()
        {
            var zipDestinationDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var zipFileName = this.DirectoryName.Trim(new char[] { ':', '/', '\\' });
            var destinationArchiveFileName = Path.Combine(zipDestinationDirectory, $"{zipFileName}.{FileConstants.ZipFileExtension}");

            ZipFile.CreateFromDirectory(this.DirectoryName, destinationArchiveFileName, CompressionLevel.Optimal, false);
        }

        private async Task ProcessDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            var xmlFiles = await this.GetFiles(directory);
            var exceptions = new ConcurrentQueue<Exception>();

            foreach (var fileName in xmlFiles)
            {
                try
                {
                    var fileProcessor = this.processorFactory.CreateFileProcessor(fileName, this.journalMeta);
                    fileProcessor.Process().Wait();
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

        private Task<IEnumerable<string>> GetFiles(string directory)
        {
            var matchSupplementaryMaterial = new Regex(@"\-s\d+\Z");

            var files = Directory.GetFiles(directory)
                .Where(f => Path.GetExtension(f).TrimStart('.').ToLower() == FileConstants.XmlFileExtension &&
                            !matchSupplementaryMaterial.IsMatch(Path.GetFileNameWithoutExtension(f)))
                .ToArray();

            return Task.FromResult<IEnumerable<string>>(files);
        }
    }
}
