namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Contracts.IO;

    public class XmlFileReader : IXmlFileReader
    {
        public XmlFileReader()
        {
            this.ReaderSettings = new XmlReaderSettings
            {
                Async = true,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore,
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.None
            };
        }

        public XmlReaderSettings ReaderSettings { get; set; }

        public XmlReader GetReader(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            return XmlReader.Create(fullName, this.ReaderSettings);
        }

        public Task<XmlDocument> ReadXmlAsync(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var reader = this.GetReader(fullName);
            try
            {
                document.Load(reader);
                return Task.FromResult(document);
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    reader.Close();
                    reader.Dispose();
                }
                catch
                {
                    // Skip
                }
            }
        }
    }
}
