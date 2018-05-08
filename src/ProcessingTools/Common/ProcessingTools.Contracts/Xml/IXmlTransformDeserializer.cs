// <copyright file="IXmlTransformDeserializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// XML transform deserializer.
    /// </summary>
    public interface IXmlTransformDeserializer
    {
        /// <summary>
        /// Deserializes XML string with a specified transformer.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized object.</typeparam>
        /// <param name="transformer">Transformer to be applied on the XML.</param>
        /// <param name="xml">XML as string to be processed.</param>
        /// <returns>Deserialized object.</returns>
        Task<T> DeserializeAsync<T>(IXmlTransformer transformer, string xml);

        /// <summary>
        /// Deserializes <see cref="XmlNode"/> context with a specified transformer.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized object.</typeparam>
        /// <param name="transformer">Transformer to be applied on the XML.</param>
        /// <param name="node"><see cref="XmlNode"/> context to be processed.</param>
        /// <returns>Deserialized object.</returns>
        Task<T> DeserializeAsync<T>(IXmlTransformer transformer, XmlNode node);
    }
}
