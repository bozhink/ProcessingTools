namespace ProcessingTools.FileSystem.IO
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.IO;

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

        public XmlReader GetReader(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return XmlReader.Create(fileName, this.ReaderSettings);
        }

        public Task<XmlDocument> ReadXmlAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var reader = this.GetReader(fileName);
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
