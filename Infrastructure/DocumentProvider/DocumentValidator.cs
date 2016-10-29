namespace ProcessingTools.DocumentProvider
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Schema;

    using ProcessingTools.Contracts;

    public class DocumentValidator : IDocumentValidator
    {
        private const string TaxPubDtdPathKey = "TaxPubDtdPath";
        private readonly string taxPubDtdPath;
        private readonly ILogger logger;

        public DocumentValidator(ILogger logger)
        {
            this.logger = logger;

            // TODO: AppSettingsReader
            var appSettingsReader = new AppSettingsReader();
            try
            {
                string taxPubDtdPath = appSettingsReader.GetValue(TaxPubDtdPathKey, typeof(string)).ToString();
                if (string.IsNullOrWhiteSpace(taxPubDtdPath) || !File.Exists(taxPubDtdPath))
                {
                    throw new ApplicationException("TaxPub DTD Path is invalid.");
                }

                this.taxPubDtdPath = taxPubDtdPath;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Invalid application settings.", e);
            }
        }

        public async Task<object> Validate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string fileName = Path.GetTempFileName() + ".xml";
            this.logger?.Log(fileName);

            await this.WriteXmlFileWithDoctype(document, fileName);

            await this.ReadXmlFileWithDtdValidation(fileName);

            return true;
        }

        private async Task ReadXmlFileWithDtdValidation(string fileName)
        {
            var readerSettings = new XmlReaderSettings
            {
                Async = true,
                CheckCharacters = true,
                CloseInput = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Parse,
                IgnoreComments = false,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = false,
                ValidationType = ValidationType.DTD,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
            };

            readerSettings.ValidationEventHandler += new ValidationEventHandler(this.ValidationCallBack);

            var reader = XmlReader.Create(fileName, readerSettings);
            while (await reader.ReadAsync())
            {
            }

            reader.Close();
        }

        private async Task WriteXmlFileWithDoctype(IDocument document, string fileName)
        {
            var writerSettings = new XmlWriterSettings
            {
                Async = true,
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                CloseOutput = true,
                DoNotEscapeUriAttributes = true,
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = " ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                NewLineOnAttributes = false,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true,
            };

            var writer = XmlWriter.Create(fileName, writerSettings);
            await writer.WriteStartDocumentAsync();
            await writer.WriteDocTypeAsync(
                "article",
                "-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN",
                this.taxPubDtdPath,
                null);

            document.XmlDocument.DocumentElement.WriteTo(writer);
            await writer.FlushAsync();
            writer.Close();
        }

        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            this.logger?.Log("Validation Error: {0}", e.Message);
        }
    }
}