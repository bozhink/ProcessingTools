namespace ProcessingTools.NlmArchiveConsoleManager.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Core;
    using Contracts.Models;
    using DocumentProvider;
    using Models;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    public class FileProcessor : IFileProcessor
    {
        private readonly ILogger logger;
        private readonly IJournal journal;
        private string fileName;
        private string fileNameWithoutExtension;
        private ICollection<string> externalFiles;

        public FileProcessor(string fileName, IJournal journal, ILogger logger)
        {
            this.FileName = fileName;
            this.logger = logger;
            this.journal = journal;
            this.externalFiles = new HashSet<string>();
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

        public Task Process()
        {
            return Task.Run(() =>
            {
                this.logger?.Log(this.FileName);

                var document = new TaxPubDocument(Encoding.UTF8);
                var xmlFileProcessor = new XmlFileProcessor(this.FileName, null, this.logger);
                xmlFileProcessor.Read(document);

                if (document.XmlDocument.DocumentElement.Name != ElementNames.Article)
                {
                    throw new ApplicationException($"'{this.fileName}' is not a NLM xml file.");
                }

                var article = new Article
                {
                    Doi = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText,
                    Volume = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleMetaVolume)?.InnerText,
                    Issue = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleMetaIssue)?.InnerText,
                    FirstPage = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleMetaFirstPage)?.InnerText,
                    LastPage = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleMetaLastPage)?.InnerText,
                    Id = document.XmlDocument.SelectSingleNode(XPathStrings.ArticleMetaElocationId)?.InnerText
                };

                string fileNameReplacementPrefix = string.Format(
                    this.journal.FileNamePattern,
                    article.Volume?.ConvertTo<int>(),
                    article.Issue?.ConvertTo<int>(),
                    article.Id,
                    article.FirstPage?.ConvertTo<int>());

                this.logger?.Log(fileNameReplacementPrefix);

                xmlFileProcessor.OutputFileName = $"{fileNameReplacementPrefix}.xml";

                this.ProcessExternalFiles(document, this.fileNameWithoutExtension, fileNameReplacementPrefix);

                this.MoveXmlFile(fileNameReplacementPrefix);

                var taxpubDtd = document.XmlDocument.CreateDocumentType(ElementNames.Article, DocumentTypes.TaxPubPublicId, DocumentTypes.TaxPubSystemId, null);

                xmlFileProcessor.Write(document, taxpubDtd);
            });
        }

        // Move other files with name FileName and different extensions to corresponding destination.
        private void MoveXmlFile(string fileNameReplacementPrefix)
        {
            var filesToMove = new HashSet<FileReplacementModel>(Directory
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

        private void ProcessExternalFiles(TaxPubDocument document, string orginalPrefix, string fileNameReplacementPrefix)
        {
            // Get external files references.
            this.externalFiles = new HashSet<string>(document.XmlDocument
                .SelectNodes(XPathStrings.XLinkHref, document.NamespaceManager)
                .Cast<XmlAttribute>()
                .Select(h => h.InnerText));

            var matchXmlFileName = new Regex($"\\A{Regex.Escape(orginalPrefix)}");
            var referencesNamesReplacements = new HashSet<FileReplacementModel>(this.externalFiles
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

        // Replace references in the xml document.
        private void UpdateContentInDocument(TaxPubDocument document, HashSet<FileReplacementModel> referencesNamesReplacements)
        {
            foreach (XmlAttribute hrefAttribute in document.XmlDocument.SelectNodes(XPathStrings.XLinkHref, document.NamespaceManager))
            {
                string content = hrefAttribute.InnerText;

                hrefAttribute.InnerText = referencesNamesReplacements
                    .FirstOrDefault(r => r.Source == content)
                    .Destination;
            }
        }

        // Move external files to the new destinations.
        private void MoveExternalFiles(HashSet<FileReplacementModel> referencesNamesReplacements)
        {
            Parallel.ForEach(
                referencesNamesReplacements,
                (reference) =>
            {
                try
                {
                    if (reference.Source == reference.OriginalFileName)
                    {
                        File.Move(reference.Source, reference.Destination);
                    }
                    else
                    {
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
    }
}