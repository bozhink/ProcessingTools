// <copyright file="IXmlReadService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.IO
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// XML read service.
    /// </summary>
    public interface IXmlReadService
    {
        /// <summary>
        /// Gets or sets the text encoding.
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Gets or sets the settings of the reader.
        /// </summary>
        XmlReaderSettings ReaderSettings { get; set; }

        /// <summary>
        /// Opens a <see cref="XmlReader"/> for a XML file.
        /// </summary>
        /// <param name="fileName">File name of the XML file.</param>
        /// <returns>Instance of <see cref="XmlReader"/>.</returns>
        XmlReader GetXmlReaderForFile(string fileName);

        /// <summary>
        /// Opens a <see cref="XmlReader"/> for XML content stream.
        /// </summary>
        /// <param name="stream">Stream of the content.</param>
        /// <returns>Instance of <see cref="XmlReader"/>.</returns>
        XmlReader GetXmlReaderForStream(Stream stream);

        /// <summary>
        /// Opens a <see cref="XmlReader"/> for XML string.
        /// </summary>
        /// <param name="xml">XML string.</param>
        /// <returns>Instance of <see cref="XmlReader"/>.</returns>
        XmlReader GetXmlReaderForXmlString(string xml);

        /// <summary>
        /// Opens a <see cref="Stream"/> for XML string.
        /// </summary>
        /// <param name="xml">XML string.</param>
        /// <returns>Instance of <see cref="Stream"/>.</returns>
        Stream GetStreamForXmlString(string xml);

        /// <summary>
        /// Reads <see cref="XmlReader"/> to <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="reader"><see cref="XmlReader"/> to be read.</param>
        /// <returns>Instance of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> ReadXmlReaderToXmlDocumentAsync(XmlReader reader);

        /// <summary>
        /// Reads XML file to <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="fileName">File name of the XML file.</param>
        /// <returns>Instance of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> ReadFileToXmlDocumentAsync(string fileName);

        /// <summary>
        /// Reads XML content stream to <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="stream">Stream of the content.</param>
        /// <returns>Instance of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> ReadStreamToXmlDocumentAsync(Stream stream);

        /// <summary>
        /// Reads XML string to <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="xml">XML string.</param>
        /// <returns>Instance of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> ReadXmlStringToXmlDocumentAsync(string xml);
    }
}
