// <copyright file="IXmlSerializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Serialization
{
    using System.Xml;

    /// <summary>
    /// Generic XML serializer.
    /// </summary>
    /// <typeparam name="T">Type of input object.</typeparam>
    public interface IXmlSerializer<in T>
    {
        /// <summary>
        /// Serialize object to <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="source">Object to be serialized.</param>
        /// <returns>Serialized <see cref="XmlNode"/>.</returns>
        XmlNode Serialize(T source);

        /// <summary>
        /// Sets <see cref="XmlNamespaceManager"/> for output result.
        /// </summary>
        /// <param name="namespaceManager"><see cref="XmlNamespaceManager"/> to be set.</param>
        void SetNamespaces(XmlNamespaceManager namespaceManager);
    }
}
