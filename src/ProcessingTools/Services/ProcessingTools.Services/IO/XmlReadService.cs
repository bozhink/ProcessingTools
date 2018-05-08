// <copyright file="XmlReadService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.IO;

    /// <summary>
    /// Xml read service.
    /// </summary>
    public class XmlReadService : IXmlReadService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlReadService"/> class.
        /// </summary>
        public XmlReadService()
        {
            this.Encoding = Defaults.Encoding;
            this.ReaderSettings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
                ValidationType = ValidationType.None
            };
        }

        /// <inheritdoc/>
        public Encoding Encoding { get; set; }

        /// <inheritdoc/>
        public XmlReaderSettings ReaderSettings { get; set; }

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return XmlReader.Create(fileName, this.ReaderSettings);
        }

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamCannotBeReadException();
            }

            return XmlReader.Create(stream, this.ReaderSettings);
        }

        /// <inheritdoc/>
        public XmlReader GetXmlReaderForXmlString(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            try
            {
                byte[] bytes = this.Encoding.GetBytes(xml);
                Stream stream = new MemoryStream(bytes)
                {
                    Position = 0
                };
                return XmlReader.Create(stream, this.ReaderSettings);
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.Encoding.EncodingName} encoded.", e);
            }
        }

        /// <inheritdoc/>
        public Stream GetStreamForXmlString(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            try
            {
                byte[] bytes = this.Encoding.GetBytes(xml);
                Stream stream = new MemoryStream(bytes)
                {
                    Position = 0
                };

                return stream;
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.Encoding.EncodingName} encoded.", e);
            }
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> ReadFileToXmlDocumentAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var reader = this.GetXmlReaderForFile(fileName);

            try
            {
                return await this.ReadXmlReaderToXmlDocumentAsync(reader).ConfigureAwait(false);
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

        /// <inheritdoc/>
        public async Task<XmlDocument> ReadStreamToXmlDocumentAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamCannotBeReadException();
            }

            var reader = this.GetXmlReaderForStream(stream);

            try
            {
                return await this.ReadXmlReaderToXmlDocumentAsync(reader).ConfigureAwait(false);
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

        /// <inheritdoc/>
        public Task<XmlDocument> ReadXmlReaderToXmlDocumentAsync(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return Task.Run(() =>
            {
                XmlDocument xmlDocument = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                xmlDocument.Load(reader);

                return xmlDocument;
            });
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> ReadXmlStringToXmlDocumentAsync(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var reader = this.GetXmlReaderForXmlString(xml);

            try
            {
                return await this.ReadXmlReaderToXmlDocumentAsync(reader).ConfigureAwait(false);
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
