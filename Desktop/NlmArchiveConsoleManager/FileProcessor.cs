namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using DocumentProvider;
    using Models;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    public class FileProcessor : ISimpleProcessor
    {
        private const string SelectHrefXPath = "//graphic/@xlink:href|//inline-graphic/@xlink:href|//media/@xlink:href";

        private ILogger logger;
        private IJournal journal;
        private string fileName;
        private string fileNameWithoutExtension;
        private ICollection<string> externalFiles;

        public FileProcessor(string fileName, IJournal journal)
            : this(fileName, journal, null)
        {
        }

        public FileProcessor(string fileName, IJournal journal, ILogger logger)
        {
            this.FileName = fileName;
            this.logger = logger;
            this.journal = journal;
            this.externalFiles = new HashSet<string>();
        }

        public string FileName
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
                var xmlFileProcessor = new XmlFileProcessor(this.FileName, this.logger);
                xmlFileProcessor.Read(document);

                if (document.XmlDocument.DocumentElement.Name != "article")
                {
                    throw new ApplicationException($"'{this.fileName}' is not a NLM xml file.");
                }

                var article = new Article
                {
                    Doi = document.XmlDocument.SelectSingleNode("/article/front/article-meta/article-id[@pub-id-type='doi']")?.InnerText,
                    Volume = document.XmlDocument.SelectSingleNode("/article/front/article-meta/volume")?.InnerText,
                    Issue = document.XmlDocument.SelectSingleNode("/article/front/article-meta/issue")?.InnerText,
                    FirstPage = document.XmlDocument.SelectSingleNode("/article/front/article-meta/fpage")?.InnerText,
                    LastPage = document.XmlDocument.SelectSingleNode("/article/front/article-meta/lpage")?.InnerText,
                    Id = document.XmlDocument.SelectSingleNode("/article/front/article-meta/elocation-id")?.InnerText,
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

                XmlDocumentType taxpubDtd = document.XmlDocument.CreateDocumentType("article", "-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN", "tax-treatment-NS0.dtd", null);

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
                .SelectNodes(SelectHrefXPath, document.NamespaceManager)
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
            foreach (XmlAttribute hrefAttribute in document.XmlDocument.SelectNodes(SelectHrefXPath, document.NamespaceManager))
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