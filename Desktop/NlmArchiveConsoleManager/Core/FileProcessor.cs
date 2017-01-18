namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Core;
    using Contracts.Models;
    using Models;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Meta;
    using ProcessingTools.Services.Data.Contracts.Files;
    using ProcessingTools.Services.Data.Contracts.Models.Meta;

    public class FileProcessor : IFileProcessor
    {
        private readonly IArticleMetaHarvester articleMetaHarvester;
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileContentDataService fileManager;
        private readonly IJournal journal;
        private readonly ILogger logger;
        private string fileName;
        private string fileNameWithoutExtension;

        public FileProcessor(
            string fileName,
            IJournal journal,
            IDocumentFactory documentFactory,
            IXmlFileContentDataService fileManager,
            IArticleMetaHarvester articleMetaHarvester,
            ILogger logger)
        {
            if (journal == null)
            {
                throw new ArgumentNullException(nameof(journal));
            }

            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (fileManager == null)
            {
                throw new ArgumentNullException(nameof(fileManager));
            }

            if (articleMetaHarvester == null)
            {
                throw new ArgumentNullException(nameof(articleMetaHarvester));
            }

            this.FileName = fileName;
            this.journal = journal;
            this.documentFactory = documentFactory;
            this.fileManager = fileManager;
            this.articleMetaHarvester = articleMetaHarvester;
            this.logger = logger;
        }

        private string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(value);
                }

                if (!File.Exists(value))
                {
                    throw new FileNotFoundException();
                }

                this.fileName = value;
                this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.fileName);
            }
        }

        public async Task Process()
        {
            var document = await this.ReadDocument();
            if (document.XmlDocument.DocumentElement.Name != ElementNames.Article)
            {
                throw new ApplicationException($"'{this.fileName}' is not a NLM xml file.");
            }

            string fileNameReplacementPrefix = await this.ComposeFileNameReplacementPrefix(document);
            this.logger?.Log("{0} / {1}", this.FileName, fileNameReplacementPrefix);

            this.ProcessExternalFiles(document, this.fileNameWithoutExtension, fileNameReplacementPrefix);
            this.MoveXmlFile(fileNameReplacementPrefix);

            var outputFileName = $"{fileNameReplacementPrefix}.xml";
            await this.WriteDocument(document, outputFileName);
        }

        private async Task<string> ComposeFileNameReplacementPrefix(IDocument document)
        {
            var articleMeta = await this.articleMetaHarvester.Harvest(document);

            string fileNameReplacementPrefix = string.Format(
                this.journal.FileNamePattern,
                articleMeta.Volume?.ConvertTo<int>(),
                articleMeta.Issue?.ConvertTo<int>(),
                articleMeta.Id,
                articleMeta.FirstPage?.ConvertTo<int>());

            return fileNameReplacementPrefix;
        }

        // Move external files to the new destinations.
        private void MoveExternalFiles(IEnumerable<IFileReplacementModel> referencesNamesReplacements)
        {
            Parallel.ForEach(
                referencesNamesReplacements,
                (reference) =>
            {
                try
                {
                    // File names here are supposed to be (1) relative or (2) web links
                    if (reference.Source == reference.OriginalFileName)
                    {
                        // (1) Relative local file names
                        File.Move(reference.Source, reference.Destination);
                    }
                    else
                    {
                        // (2) Web links, etc.
                        File.Copy(reference.Source, reference.Destination);

                        try
                        {
                            File.Delete(reference.Source);
                        }
                        catch (Exception e)
                        {
                            this.logger?.Log(e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger?.Log(ex);
                }
            });
        }

        // Move other files with name FileName and different extensions to corresponding destination.
        private void MoveXmlFile(string fileNameReplacementPrefix)
        {
            var filesToMove = new HashSet<IFileReplacementModel>(Directory
                .GetFiles(Directory.GetCurrentDirectory())
                .Where(f => Path.GetFileNameWithoutExtension(f) == this.fileNameWithoutExtension)
                .Select(f => new FileReplacementModel
                {
                    OriginalFileName = f,
                    Source = f,
                    Destination = fileNameReplacementPrefix + Path.GetExtension(f)
                }));

            Parallel.ForEach(
                filesToMove,
                (file) =>
                {
                    try
                    {
                        File.Move(file.Source, file.Destination);
                    }
                    catch (Exception ex)
                    {
                        this.logger?.Log(ex);
                    }
                });
        }

        private void ProcessExternalFiles(IDocument document, string orginalPrefix, string fileNameReplacementPrefix)
        {
            // Get external files references.
            var externalFiles = new HashSet<string>(document.SelectNodes(XPathStrings.XLinkHref)
                .Cast<XmlAttribute>()
                .Select(h => h.InnerText));

            var matchXmlFileName = new Regex($"\\A{Regex.Escape(orginalPrefix)}");
            var referencesNamesReplacements = new HashSet<IFileReplacementModel>(externalFiles
                .Select(f =>
                {
                    string externalFileName = Path.GetFileName(f);
                    return new FileReplacementModel
                    {
                        Source = f,
                        Destination = matchXmlFileName.Replace(externalFileName, fileNameReplacementPrefix),
                        OriginalFileName = externalFileName
                    };
                }));

            this.MoveExternalFiles(referencesNamesReplacements);

            this.UpdateContentInDocument(document, referencesNamesReplacements);
        }

        private async Task<IDocument> ReadDocument()
        {
            var xml = await this.fileManager.ReadXmlFile(this.FileName);
            var document = this.documentFactory.Create(xml.DocumentElement.OuterXml);
            return document;
        }

        // Replace references in the xml document.
        private void UpdateContentInDocument(IDocument document, IEnumerable<IFileReplacementModel> referencesNamesReplacements)
        {
            foreach (XmlAttribute hrefAttribute in document.SelectNodes(XPathStrings.XLinkHref))
            {
                string content = hrefAttribute.InnerText;

                hrefAttribute.InnerText = referencesNamesReplacements
                    .FirstOrDefault(r => r.Source == content)
                    .Destination;
            }
        }

        private async Task<object> WriteDocument(IDocument document, string outputFileName)
        {
            var taxpubDtd = document.XmlDocument.CreateDocumentType(
                ElementNames.Article,
                DocumentTypes.TaxPubPublicId,
                DocumentTypes.TaxPubSystemId,
                null);

            return await this.fileManager.WriteXmlFile(outputFileName, document.XmlDocument, taxpubDtd);
        }
    }
}
