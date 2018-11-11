// <copyright file="IDocument.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Represents default document type.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets the encoding of the document.
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// Gets the namespace manager of the document.
        /// </summary>
        XmlNamespaceManager NamespaceManager { get; }

        /// <summary>
        /// Gets the nametable of the document.
        /// </summary>
        NameTable NameTable { get; }

        /// <summary>
        /// Gets or sets the schema type of the document.
        /// </summary>
        SchemaType SchemaType { get; set; }

        /// <summary>
        /// Gets or sets the XML content of the document as string.
        /// </summary>
        string Xml { get; set; }

        /// <summary>
        /// Gets the <see cref="XmlDocument"/> of the document.
        /// </summary>
        XmlDocument XmlDocument { get; }

        /// <summary>
        /// Evaluated an XPath expression using the namespace manager of the document.
        /// </summary>
        /// <param name="xpath">XPath expression to be evaluated</param>
        /// <returns><see cref="IEnumerable{T}"/> of nodes</returns>
        IEnumerable<XmlNode> SelectNodes(string xpath);

        /// <summary>
        /// Evaluated an XPath expression using the namespace manager of the document
        /// and returns the first node.
        /// </summary>
        /// <param name="xpath">XPath expression to be evaluated</param>
        /// <returns>First node</returns>
        XmlNode SelectSingleNode(string xpath);
    }
}
