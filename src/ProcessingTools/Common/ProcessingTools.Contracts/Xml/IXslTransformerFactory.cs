// <copyright file="IXslTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    /// <summary>
    /// XSL transformer factory.
    /// </summary>
    public interface IXslTransformerFactory
    {
        /// <summary>
        /// Creates <see cref="IXslTransformer"/> from file with a specified file name.
        /// </summary>
        /// <param name="xslFileName">File name of the XSL file.</param>
        /// <returns>The transformer.</returns>
        IXslTransformer CreateTransformer(string xslFileName);
    }
}
