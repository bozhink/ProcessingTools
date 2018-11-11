// <copyright file="IXmlSerializer{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Serialization
{
    using System.Threading.Tasks;
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
        /// <param name="object">Object to be serialized.</param>
        /// <returns>Task of <see cref="XmlNode"/> result.</returns>
        Task<XmlNode> SerializeAsync(T @object);

        /// <summary>
        /// Sets <see cref="XmlNamespaceManager"/> for output result.
        /// </summary>
        /// <param name="namespaceManager"><see cref="XmlNamespaceManager"/> to be set.</param>
        void SetNamespaces(XmlNamespaceManager namespaceManager);
    }
}
