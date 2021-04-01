﻿// <copyright file="IXmlWriteService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.IO
{
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// XML write service.
    /// </summary>
    public interface IXmlWriteService
    {
        /// <summary>
        /// Gets or sets the settings of the writer.
        /// </summary>
        XmlWriterSettings WriterSettings { get; set; }

        /// <summary>
        /// Writes content under <see cref="XmlReader"/> under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="reader"><see cref="XmlReader"/> object to be read.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(string fileName, XmlReader reader);

        /// <summary>
        /// Writes content under <see cref="XmlReader"/> under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="reader"><see cref="XmlReader"/> object to be read.</param>
        /// <param name="documentType">DOCTYPE for the output file.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(string fileName, XmlReader reader, XmlDocumentType documentType);

        /// <summary>
        /// Writes <see cref="XmlDocument"/> object under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="document"><see cref="XmlDocument"/> object to written.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(string fileName, XmlDocument document);

        /// <summary>
        /// Writes <see cref="XmlDocument"/> object under specified file name.
        /// </summary>
        /// <param name="fileName">Full name of the output file.</param>
        /// <param name="document"><see cref="XmlDocument"/> object to written.</param>
        /// <param name="documentType">DOCTYPE for the output file.</param>
        /// <returns>Task.</returns>
        Task<object> WriteAsync(string fileName, XmlDocument document, XmlDocumentType documentType);
    }
}
