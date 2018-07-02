// <copyright file="XslTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Xml
{
    using System;
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// XSL transformer factory.
    /// </summary>
    public class XslTransformerFactory : IXslTransformerFactory, IXmlTransformerFactory
    {
        /// <summary>
        /// Gets or sets the <see cref="IXslTransformerFromContent"/> factory.
        /// </summary>
        public Func<string, IXslTransformerFromContent> XslTransformerFromContentFactory { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IXslTransformerFromFile"/> factory.
        /// </summary>
        public Func<string, IXslTransformerFromFile> XslTransformerFromFileFactory { get; set; }

        /// <inheritdoc/>
        public IXslTransformerFromContent CreateTransformerFromContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            return this.XslTransformerFromContentFactory?.Invoke(content);
        }

        /// <inheritdoc/>
        public IXslTransformerFromFile CreateTransformerFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return this.XslTransformerFromFileFactory?.Invoke(fileName);
        }

        /// <inheritdoc/>
        public IXmlTransformer CreateXmlTransformerFromSourceScript(string source) => this.CreateTransformerFromContent(source);
    }
}
