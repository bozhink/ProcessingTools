// <copyright file="IXQueryTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    /// <summary>
    /// XQuery transformer factory.
    /// </summary>
    public interface IXQueryTransformerFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="IXQueryTransformer"/>.
        /// </summary>
        /// <param name="xqueryFileName">Name of the XQuery file.</param>
        /// <returns>The new instance of <see cref="IXQueryTransformer"/>.</returns>
        IXQueryTransformer CreateTransformer(string xqueryFileName);
    }
}
