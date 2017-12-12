// <copyright file="IXmlTransformer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// XML transformer.
    /// </summary>
    public interface IXmlTransformer : ITransformer
    {
        /// <summary>
        /// Transforms XML as string to resultant string object.
        /// </summary>
        /// <param name="xml">XML as string to be transformed</param>
        /// <returns>Task of resultant string object</returns>
        Task<string> TransformAsync(string xml);

        /// <summary>
        /// Transforms <see cref="XmlNode"/> object to resultant string object.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be transformed</param>
        /// <returns>Task of resultant string object</returns>
        Task<string> TransformAsync(XmlNode node);

        /// <summary>
        /// Transforms content under <see cref="XmlReader"/> object to resultant string object.
        /// </summary>
        /// <param name="reader"><see cref="XmlReader"/> to read XML content</param>
        /// <param name="closeReader">Specifies whether to close the reader after transform is completed</param>
        /// <returns>Task of resultant string object</returns>
        Task<string> TransformAsync(XmlReader reader, bool closeReader);

        /// <summary>
        /// Transforms XML as string to stream.
        /// </summary>
        /// <param name="xml">XML as string to be transformed</param>
        /// <returns><see cref="Stream"/> of transformed result</returns>
        Stream TransformToStream(string xml);

        /// <summary>
        /// Transforms <see cref="XmlNode"/> object to stream.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object to be transformed</param>
        /// <returns><see cref="Stream"/> of transformed result</returns>
        Stream TransformToStream(XmlNode node);

        /// <summary>
        /// Transforms content under <see cref="XmlReader"/> object to stream.
        /// </summary>
        /// <param name="reader"><see cref="XmlReader"/> to read XML content</param>
        /// <returns><see cref="Stream"/> of transformed result</returns>
        Stream TransformToStream(XmlReader reader);
    }
}
