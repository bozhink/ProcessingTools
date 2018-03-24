// <copyright file="DocumentValidator.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Schema;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    /// <summary>
    /// Document Validator.
    /// </summary>
    public class DocumentValidator : IDocumentValidator
    {
        private readonly XmlReaderSettings readerSettings;
        private readonly XmlWriterSettings writerSettings;
        private readonly StringBuilder reportBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentValidator"/> class.
        /// </summary>
        public DocumentValidator()
        {
            this.readerSettings = new XmlReaderSettings
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

            this.readerSettings.ValidationEventHandler += new ValidationEventHandler(this.ValidationCallBack);

            this.writerSettings = new XmlWriterSettings
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

            this.reportBuilder = new StringBuilder();
        }

        /// <inheritdoc/>
        public async Task<object> ValidateAsync(IDocument context, IReporter reporter)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string fileName = Path.GetTempFileName() + "." + FileConstants.XmlFileExtension;

            reporter.AppendContent(string.Format("File name = {0}", fileName));

            await this.WriteXmlFileWithDoctype(context, fileName).ConfigureAwait(false);

            await this.ReadXmlFileWithDtdValidation(fileName).ConfigureAwait(false);

            reporter.AppendContent(this.reportBuilder.ToString());
            return true;
        }

        private async Task ReadXmlFileWithDtdValidation(string fileName)
        {
            using (var reader = XmlReader.Create(fileName, this.readerSettings))
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    // Skip
                }

                reader.Close();
            }
        }

        private async Task WriteXmlFileWithDoctype(IDocument document, string fileName)
        {
            using (var writer = XmlWriter.Create(fileName, this.writerSettings))
            {
                await writer.WriteStartDocumentAsync().ConfigureAwait(false);
                await writer.WriteDocTypeAsync(
                    DocTypeConstants.TaxPubName,
                    DocTypeConstants.TaxPubPublicId,
                    DocTypeConstants.TaxPubDtdPath,
                    null)
                    .ConfigureAwait(false);

                document.XmlDocument.DocumentElement.WriteTo(writer);

                await writer.FlushAsync().ConfigureAwait(false);
                writer.Close();
            }
        }

        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            this.reportBuilder.AppendFormat("Validation Error: {0}", e.Message);
        }
    }
}
