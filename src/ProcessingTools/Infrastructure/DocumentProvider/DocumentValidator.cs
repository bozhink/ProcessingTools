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
        private readonly StringBuilder reportBuilder = new StringBuilder();

        public DocumentValidator()
        {
            // TODO: AppSettingsReader
            var appSettingsReader = new AppSettingsReader();
            try
            {
                string dtdPath = appSettingsReader.GetValue(TaxPubDtdPathKey, typeof(string)).ToString();
                if (string.IsNullOrWhiteSpace(dtdPath) || !File.Exists(dtdPath))
                {
                    throw new ApplicationException("TaxPub DTD Path is invalid.");
                }

                this.taxPubDtdPath = dtdPath;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Invalid application settings.", e);
            }
        }

        public async Task<object> Validate(IDocument context, IReporter reporter)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string fileName = Path.GetTempFileName() + ".xml";

            reporter.AppendContent(string.Format("File name = {0}", fileName));

            await this.WriteXmlFileWithDoctype(context, fileName);

            await this.ReadXmlFileWithDtdValidation(fileName);

            reporter.AppendContent(this.reportBuilder.ToString());
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
                // Skip
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
            this.reportBuilder.AppendFormat("Validation Error: {0}", e.Message);
        }
    }
}