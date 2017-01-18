namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Core;
    using Contracts.Factories;
    using ProcessingTools.Constants;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public class DirectoryProcessor : IDirectoryProcessor
    {
        private readonly IProcessorFactory processorFactory;
        private readonly IJournal journal;
        private string direcoryName;

        public DirectoryProcessor(string direcoryName, IJournal journal, IProcessorFactory processorFactory)
        {
            if (journal == null)
            {
                throw new ArgumentNullException(nameof(journal));
            }

            if (processorFactory == null)
            {
                throw new ArgumentNullException(nameof(processorFactory));
            }

            this.DirectoryName = direcoryName;
            this.journal = journal;
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

            var exceptions = await this.ProcessDirectory();

            Directory.SetCurrentDirectory(initialDirectory);

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToList());
            }
        }

        private async Task<ConcurrentQueue<Exception>> ProcessDirectory()
        {
            var xmlFiles = await this.GetFiles();
            var exceptions = new ConcurrentQueue<Exception>();

            foreach (var fileName in xmlFiles)
            {
                try
                {
                    var fileProcessor = this.processorFactory.CreateFileProcessor(fileName, this.journal);
                    fileProcessor.Process().Wait();
                }
                catch (Exception e)
                {
                    exceptions.Enqueue(e);
                }
            }

            return exceptions;
        }

        private Task<IEnumerable<string>> GetFiles()
        {
            var matchSupplementaryMaterial = new Regex(@"\-s\d+\Z");

            var files = Directory.GetFiles(Directory.GetCurrentDirectory())
                .Where(f => Path.GetExtension(f).TrimStart('.').ToLower() == FileConstants.XmlFileExtension &&
                            !matchSupplementaryMaterial.IsMatch(Path.GetFileNameWithoutExtension(f)))
                .ToArray();

            return Task.FromResult<IEnumerable<string>>(files);
        }
    }
}
