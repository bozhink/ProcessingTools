// <copyright file="XmlTransformDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Contracts.Services.Xml;

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
        public Task<T> DeserializeAsync<T>(IXmlTransformer transformer, string xml)
        {
            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            return this.DeserializeInternalAsync<T>(transformer, xml);
        }

        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(IXmlTransformer transformer, XmlNode node)
        {
            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.DeserializeInternalAsync<T>(transformer, node);
        }

        private async Task<T> DeserializeInternalAsync<T>(IXmlTransformer transformer, string xml)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            var stream = transformer.TransformToStream(xml);

            var result = this.serializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }

        private async Task<T> DeserializeInternalAsync<T>(IXmlTransformer transformer, XmlNode node)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            var stream = transformer.TransformToStream(node);

            var result = this.serializer.Deserialize<T>(stream);

            stream.Close();
            stream.Dispose();

            return result;
        }
    }
}
