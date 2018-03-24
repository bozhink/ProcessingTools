// <copyright file="IXQueryTransform.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    using System.IO;
    using System.Xml;

    /// <summary>
    /// XQuery transform.
    /// </summary>
    public interface IXQueryTransform
    {
        /// <summary>
        /// Executes XQuery on a specified <see cref="XmlNode"/> context.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> context to be processed.</param>
        /// <returns>Processed resultant <see cref="XmlDocument"/>.</returns>
        XmlDocument Evaluate(XmlNode node);

        /// <summary>
        /// Loads XQuery to transform.
        /// </summary>
        /// <param name="query">Query to be loaded.</param>
        void Load(string query);

        /// <summary>
        /// Loads XQuery to transform.
        /// </summary>
        /// <param name="queryStream">Query to be loaded.</param>
        void Load(Stream queryStream);
    }
}
