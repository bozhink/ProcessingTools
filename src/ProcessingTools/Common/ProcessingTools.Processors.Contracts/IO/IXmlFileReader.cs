// <copyright file="IXmlFileReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// File reader for XML files.
    /// </summary>
    public interface IXmlFileReader
    {
        /// <summary>
        /// Gets or sets the setting of the reader.
        /// </summary>
        XmlReaderSettings ReaderSettings { get; set; }

        /// <summary>
        /// Opens a <see cref="XmlReader"/> for a XML file.
        /// </summary>
        /// <param name="fileName">Full name of the input file.</param>
        /// <returns><see cref="XmlReader"/> object for the XML file.</returns>
        XmlReader GetReader(string fileName);

        /// <summary>
        /// Reads a XML file to a <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="fileName">Full name of the input file.</param>
        /// <returns>Task of resultant <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> ReadXmlAsync(string fileName);
    }
}
