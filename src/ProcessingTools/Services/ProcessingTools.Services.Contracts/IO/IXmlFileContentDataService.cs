// <copyright file="IXmlFileContentDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// Manipulates content of XML files.
    /// </summary>
    public interface IXmlFileContentDataService
    {
        /// <summary>
        /// Gets or sets custom XmlReaderSettings to be used in read process.
        /// </summary>
        XmlReaderSettings ReaderSettings { get; set; }

        /// <summary>
        /// Gets or sets custom XmlWriterSettings to be used in write process.
        /// </summary>
        XmlWriterSettings WriterSettings { get; set; }

        /// <summary>
        /// Reads an XML file to DOM.
        /// </summary>
        /// <param name="fullName">The full name of the file to be read.</param>
        /// <returns>DOM of the read file.</returns>
        Task<XmlDocument> ReadXmlFile(string fullName);

        /// <summary>
        /// Writes a DOM to a specified file.
        /// </summary>
        /// <param name="fullName">The full name of the file to be written.</param>
        /// <param name="document">XML as DOM to be written.</param>
        /// <param name="documentType">Custom DOCTYPE of the DOM to be set.</param>
        /// <returns>Task</returns>
        Task<object> WriteXmlFile(string fullName, XmlDocument document, XmlDocumentType documentType = null);
    }
}
