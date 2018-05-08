// <copyright file="XmlTransformDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Serialization;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// XML transform deserializer.
    /// </summary>
    public class XmlTransformDeserializer : IXmlTransformDeserializer
    {
        private readonly IXmlDeserializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTransformDeserializer"/> class.
        /// </summary>
        /// <param name="serializer">Instance of <see cref="IXmlDeserializer"/>.</param>
        public XmlTransformDeserializer(IXmlDeserializer serializer)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <inheritdoc/>
        public async Task<T> DeserializeAsync<T>(IXmlTransformer transformer, string xml)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            var stream = transformer.TransformToStream(xml);

            var result = await this.serializer.DeserializeAsync<T>(stream).ConfigureAwait(false);

            stream.Close();
            stream.Dispose();

            return result;
        }

        /// <inheritdoc/>
        public async Task<T> DeserializeAsync<T>(IXmlTransformer transformer, XmlNode node)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var stream = transformer.TransformToStream(node);

            var result = await this.serializer.DeserializeAsync<T>(stream).ConfigureAwait(false);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
