// <copyright file="XmlDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Contracts.Serialization;

    /// <summary>
    /// XML deserializer.
    /// </summary>
    public class XmlDeserializer : IXmlDeserializer
    {
        /// <inheritdoc/>
        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            return Task.Run(() =>
            {
                if (stream == null || !stream.CanRead || !stream.CanSeek)
                {
                    return default(T);
                }

                stream.Position = 0;

                var serializer = new XmlSerializer(typeof(T));

                var result = serializer.Deserialize(stream);
                return (T)result;
            });
        }
    }
}
