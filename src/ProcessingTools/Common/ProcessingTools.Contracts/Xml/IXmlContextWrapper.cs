// <copyright file="IXmlContextWrapper.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    using System.Xml;

    /// <summary>
    /// XML context wrapper.
    /// </summary>
    public interface IXmlContextWrapper
    {
        /// <summary>
        /// Creates a new instance of <see cref="XmlDocument"/> which wraps a specified <see cref="XmlNode"/> context.
        /// </summary>
        /// <param name="context">The <see cref="XmlNode"/> context to be wrapped.</param>
        /// <returns>The new instance of <see cref="XmlDocument"/> which wraps a specified <see cref="XmlNode"/> context.</returns>
        XmlDocument Create(XmlNode context);
    }
}
