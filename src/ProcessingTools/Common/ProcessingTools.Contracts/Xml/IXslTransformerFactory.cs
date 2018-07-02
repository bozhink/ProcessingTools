// <copyright file="IXslTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    /// <summary>
    /// XSL transformer factory.
    /// </summary>
    public interface IXslTransformerFactory
    {
        /// <summary>
        /// Creates <see cref="IXslTransformerFromFile"/> from file with a specified file name.
        /// </summary>
        /// <param name="fileName">File name of the source file.</param>
        /// <returns>The transformer.</returns>
        IXslTransformerFromFile CreateTransformerFromFile(string fileName);

        /// <summary>
        /// Creates <see cref="IXslTransformerFromContent"/> from specified source content.
        /// </summary>
        /// <param name="content">Source content.</param>
        /// <returns>The transformer.</returns>
        IXslTransformerFromContent CreateTransformerFromContent(string content);
    }
}
