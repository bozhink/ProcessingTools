// <copyright file="XmlFileReaderWriter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.IO
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services.IO;

    /// <summary>
    /// XML file reader-writer.
    /// </summary>
    public class XmlFileReaderWriter : IXmlFileReaderWriter
    {
        private const int MaximalNumberOfTrialsToGenerateNewFileName = 100;

        private XmlReaderSettings readerSettings;
        private XmlWriterSettings writerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileReaderWriter"/> class.
        /// </summary>
        public XmlFileReaderWriter()
        {
            this.ReaderSettings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
            };

            this.WriterSettings = new XmlWriterSettings
            {
                Async = true,
                CloseOutput = true,
                Encoding = Defaults.Encoding,
                Indent = false,
                IndentChars = " ",
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                NewLineChars = "\n",
                NewLineOnAttributes = false,
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true,
            };
        }

        /// <inheritdoc/>
        public XmlReaderSettings ReaderSettings
        {
            get => this.readerSettings;

            set => this.readerSettings = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        public XmlWriterSettings WriterSettings
        {
            get => this.writerSettings;

            set => this.writerSettings = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        public XmlReader GetXmlReader(string fileName, string basePath)
        {
            return XmlReader.Create(this.ReadToStream(fileName, basePath), this.ReaderSettings);
        }

        /// <inheritdoc/>
        public Stream ReadToStream(string fileName, string basePath)
        {
            string path = this.CombineFileName(fileName, basePath);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return stream;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string fileName, string basePath)
        {
            return Task.Run(() =>
            {
                string path = this.CombineFileName(fileName, basePath);
                File.Delete(path);
            });
        }

        /// <inheritdoc/>
        public Task<long> WriteAsync(Stream stream, string fileName, string basePath)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return this.WriteInternalAsync(stream, fileName, basePath);
        }

        /// <inheritdoc/>
        public Task<string> GetNewFilePathAsync(string fileName, string basePath, int length)
        {
            return Task.Run(() =>
            {
                string path = this.CombineFileName(this.GenerateFileName(fileName, length), basePath);
                int pathLength = path.Length;

                string result = path;

                for (int i = 0; (i < MaximalNumberOfTrialsToGenerateNewFileName) && File.Exists(result); ++i)
                {
                    string suffix = i.ToString();
                    result = path.Substring(0, pathLength - suffix.Length) + suffix;
                }

                if (File.Exists(result))
                {
                    throw new CanNotGenerateUniqueFileNameException();
                }

                return result;
            });
        }

        private async Task<long> WriteInternalAsync(Stream stream, string fileName, string basePath)
        {
            string path = this.CombineFileName(fileName, basePath);
            using (var writer = XmlWriter.Create(path, this.WriterSettings))
            {
                using (var reader = XmlReader.Create(stream, this.ReaderSettings))
                {
                    await writer.WriteNodeAsync(reader, true).ConfigureAwait(false);
                    reader.Close();
                }

                await writer.FlushAsync().ConfigureAwait(false);
                writer.Close();
            }

            var contentLength = new FileInfo(path).Length;
            return contentLength;
        }

        private string GenerateFileName(string prefix, int length)
        {
            Regex matchInvalidFileNameSymbols = new Regex(@"[^A-Za-z0-9_\-\.]");
            string prefixString = matchInvalidFileNameSymbols.Replace(prefix, "_");

            prefixString = Path.GetFileNameWithoutExtension(prefixString);
            prefixString = prefixString.Substring(0, Math.Min(prefixString.Length, length / 2));

            string timeStamp = matchInvalidFileNameSymbols.Replace(DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss"), "-");

            string fileName = $"{prefixString}-{timeStamp}-{Guid.NewGuid().ToString()}";

            return fileName.PadRight(length, 'X')
                .Substring(0, length);
        }

        private string CombineFileName(string fileName, string basePath)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            Regex matchDriveLetter = new Regex(@"\A\W+:/");

            string path;

            if (string.IsNullOrWhiteSpace(basePath))
            {
                path = fileName;
            }
            else
            {
                path = Path.Combine(basePath, matchDriveLetter.Replace(fileName, string.Empty));
            }

            return path;
        }
    }
}
