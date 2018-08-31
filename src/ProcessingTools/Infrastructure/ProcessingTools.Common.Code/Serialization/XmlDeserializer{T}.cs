// <copyright file="XmlDeserializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Serialization
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Contracts.Serialization;

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

        /// <inheritdoc/>
        public Task<T> DeserializeAsync(Stream stream)
        {
            return Task.Run(() =>
            {
                if (stream == null || !stream.CanRead || !stream.CanSeek)
                {
                    return default(T);
                }

                stream.Position = 0;
                var result = this.serializer.Deserialize(stream);
                return (T)result;
            });
        }
    }
}
