namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Meta;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Contracts.Services.Data.Files;
    using ProcessingTools.Extensions;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Models;

    public class FileProcessor : IFileProcessor
    {
        private readonly IArticleMetaHarvester articleMetaHarvester;
        private readonly IDocumentFactory documentFactory;
        private readonly IXmlFileContentDataService fileManager;
        private readonly IModelFactory modelFactory;
        private readonly IJournalMeta journalMeta;
        private readonly ILogger logger;
        private string fileName;
        private string fileNameWithoutExtension;

        public FileProcessor(
            string fileName,
            IJournalMeta journalMeta,
            IDocumentFactory documentFactory,
            IXmlFileContentDataService fileManager,
            IArticleMetaHarvester articleMetaHarvester,
            IModelFactory modelFactory,
            ILogger logger)
        {
            this.FileName = fileName;
            this.journalMeta = journalMeta ?? throw new ArgumentNullException(nameof(journalMeta));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            this.articleMetaHarvester = articleMetaHarvester ?? throw new ArgumentNullException(nameof(articleMetaHarvester));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
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
            var document = await this.ReadDocumentAsync().ConfigureAwait(false);
            if (document.XmlDocument.DocumentElement.Name != ElementNames.Article)
            {
                throw new XmlException($"'{this.FileName}' is not a NLM XML file.");
            }

            string fileNameReplacementPrefix = await this.ComposeFileNameReplacementPrefixAsync(document).ConfigureAwait(false);
            var outputFileName = $"{fileNameReplacementPrefix}.{FileConstants.XmlFileExtension}";

            this.logger?.Log("{0} / {1} / {2}", this.FileName, fileNameReplacementPrefix, outputFileName);

            this.ProcessReferencedFiles(document, this.fileNameWithoutExtension, fileNameReplacementPrefix);
            this.MoveNonXmlFile(fileNameReplacementPrefix);
            this.UpdateJournalMetaInDocument(document);

            await this.WriteDocumentAsync(document, outputFileName).ConfigureAwait(false);

            // Remove original file
            if (outputFileName != Path.GetFileName(this.FileName))
            {
                try
                {
                    File.Delete(this.FileName);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e);
                }
            }
        }

        private async Task<string> ComposeFileNameReplacementPrefixAsync(ProcessingTools.Contracts.IDocument document)
        {
            var articleMeta = await this.articleMetaHarvester.HarvestAsync(document);

            string fileNameReplacementPrefix = string.Format(
                this.journalMeta.FileNamePattern,
                articleMeta.Volume?.ConvertTo<int>(),
                articleMeta.Issue?.ConvertTo<int>(),
                articleMeta.Id,
                articleMeta.FirstPage?.ConvertTo<int>());

            return fileNameReplacementPrefix;
        }

        // Move files referenced in the XML document to the new destinations.
        private void MoveReferencedFiles(IEnumerable<IFileReplacementModel> referencedFileNamesReplacements)
        {
            foreach (var reference in referencedFileNamesReplacements)
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
            }
        }

        // Move other files with name FileName and different extensions to corresponding destination.
        private void MoveNonXmlFile(string fileNameReplacementPrefix)
        {
            var filesToMove = Directory.GetFiles(Directory.GetCurrentDirectory())
                .Where(
                    f =>
                        Path.GetFileNameWithoutExtension(f) == this.fileNameWithoutExtension &&
                        Path.GetExtension(f).TrimStart('.').ToLowerInvariant() != FileConstants.XmlFileExtension)
                .Select(
                    f => this.modelFactory.CreateFileReplacementModel(
                        destination: fileNameReplacementPrefix + Path.GetExtension(f),
                        originalFileName: f,
                        source: f))
                .ToList();

            foreach (var file in filesToMove)
            {
                try
                {
                    File.Move(file.Source, file.Destination);
                }
                catch (Exception ex)
                {
                    this.logger?.Log(ex);
                }
            }
        }

        private void ProcessReferencedFiles(ProcessingTools.Contracts.IDocument document, string originalPrefix, string fileNameReplacementPrefix)
        {
            // Get file references.
            var referencedFileNames = new HashSet<string>(document.SelectNodes(XPathStrings.XLinkHref).Select(h => h.InnerText));

            var matchXmlFileName = new Regex($"\\A{Regex.Escape(originalPrefix)}");
            var referencesNamesReplacements = new HashSet<IFileReplacementModel>(referencedFileNames
                .Select(
                    f =>
                    {
                        string externalFileName = Path.GetFileName(f);
                        return this.modelFactory.CreateFileReplacementModel(
                            destination: matchXmlFileName.Replace(externalFileName, fileNameReplacementPrefix),
                            originalFileName: externalFileName,
                            source: f);
                    }));

            this.MoveReferencedFiles(referencesNamesReplacements);

            this.UpdateContentInDocument(document, referencesNamesReplacements);
        }

        private async Task<ProcessingTools.Contracts.IDocument> ReadDocumentAsync()
        {
            var xml = await this.fileManager.ReadXmlFile(this.FileName).ConfigureAwait(false);
            var document = this.documentFactory.Create(xml.DocumentElement.OuterXml);
            return document;
        }

        // Replace references in the XML document.
        private void UpdateContentInDocument(ProcessingTools.Contracts.IDocument document, IEnumerable<IFileReplacementModel> replacements)
        {
            foreach (var hrefAttribute in document.SelectNodes(XPathStrings.XLinkHref))
            {
                string content = hrefAttribute.InnerText;

                hrefAttribute.InnerText = replacements.FirstOrDefault(r => r.Source == content)?.Destination;
            }
        }

        private void UpdateJournalMetaInDocument(ProcessingTools.Contracts.IDocument document)
        {
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaJournalId,
                this.journalMeta.JournalId);
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaJournalTitle,
                this.journalMeta.JournalTitle);
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaJournalAbbreviatedTitle,
                this.journalMeta.AbbreviatedJournalTitle);
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaIssnPPub,
                this.journalMeta.IssnPPub);
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaIssnEPub,
                this.journalMeta.IssnEPub);
            this.UpdateMetaNodeContent(
                document,
                XPathStrings.ArticleJournalMetaPublisherName,
                this.journalMeta.PublisherName);
        }

        private void UpdateMetaNodeContent(ProcessingTools.Contracts.IDocument document, string xpath, string content)
        {
            var node = document.SelectSingleNode(xpath);
            if (node != null)
            {
                node.InnerXml = content;
            }
        }

        private async Task<object> WriteDocumentAsync(ProcessingTools.Contracts.IDocument document, string outputFileName)
        {
            var taxpubDtd = document.XmlDocument.CreateDocumentType(
                DocTypeConstants.TaxPubName,
                DocTypeConstants.TaxPubPublicId,
                DocTypeConstants.TaxPubSystemId,
                null);

            return await this.fileManager
                .WriteXmlFile(outputFileName, document.XmlDocument, taxpubDtd)
                .ConfigureAwait(false);
        }
    }
}
