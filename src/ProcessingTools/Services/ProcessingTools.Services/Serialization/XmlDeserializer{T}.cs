// <copyright file="XmlDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Serialization;

    /// <summary>
    /// Generic XML deserializer.
    /// </summary>
    /// <typeparam name="T">Type of serialization model.</typeparam>
    public class XmlDeserializer<T> : IXmlDeserializer<T>
    {
        private readonly XmlSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDeserializer{T}"/> class.
        /// </summary>
        public XmlDeserializer()
        {
            this.serializer = new XmlSerializer(typeof(T));
        }

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
        public T Deserialize(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default;
            }

            var bytes = Defaults.Encoding.GetBytes(source);
            using (var stream = new MemoryStream(bytes))
            {
                return this.Deserialize(stream);
            }
        }

        /// <inheritdoc/>
        public T Deserialize(Stream source)
        {
            if (source is null || !source.CanRead)
            {
                return default;
            }

            using (var reader = XmlReader.Create(source, this.XmlReaderSettings))
            {
                var result = this.serializer.Deserialize(reader);

                return (T)result;
            }
        }
    }
}
