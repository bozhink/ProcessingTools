// <copyright file="XmlContextWrapper.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using System.Xml;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// XML context wrapper.
    /// </summary>
    public class XmlContextWrapper : IXmlContextWrapper
    {
        /// <inheritdoc/>
        public XmlDocument Create(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var wrapper = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var element = wrapper.CreateElement("context:context-wrapper", "urn:processing-tools-xml:context-wrapper");

            wrapper.LoadXml(element.OuterXml);
            wrapper.DocumentElement.InnerXml = context.InnerXml;

            return wrapper;
        }
    }
}
