// <copyright file="XmlDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// XML deserializer.
    /// </summary>
    public class XmlDeserializer : IXmlDeserializer
    {
        /// <summary>
        /// Gets or sets the XML reader settings.
        /// </summary>
        public XmlReaderSettings XmlReaderSettings { get; set; } = new XmlReaderSettings
        {
            IgnoreComments = true,
            IgnoreProcessingInstructions = true,
            IgnoreWhitespace = false,
            DtdProcessing = DtdProcessing.Ignore,
            CloseInput = false,
        };

        /// <inheritdoc/>
        public T Deserialize<T>(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            var bytes = Defaults.Encoding.GetBytes(source);
            using (var stream = new MemoryStream(bytes))
            {
                return this.Deserialize<T>(stream);
            }
        }

        /// <inheritdoc/>
        public T Deserialize<T>(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            using (var reader = XmlReader.Create(source, this.XmlReaderSettings))
            {
                var serializer = new XmlSerializer(typeof(T));

                var result = serializer.Deserialize(reader);

                return (T)result;
            }
        }
    }
}
