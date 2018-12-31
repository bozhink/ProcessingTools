﻿// <copyright file="TaxPubDocumentFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code
{
    using System;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;

    /// <summary>
    /// TaxPub Document Factory.
    /// </summary>
    public class TaxPubDocumentFactory : ITaxPubDocumentFactory
    {
        /// <summary>
        /// Creates new instance of <see cref="IDocument"/>.
        /// </summary>
        /// <returns>New instance of <see cref="IDocument"/>.</returns>
        public IDocument Create()
        {
            return new TaxPubDocument();
        }

        /// <summary>
        /// Creates new instance of <see cref="IDocument"/> with specified content.
        /// </summary>
        /// <param name="content">Content of the document as string.</param>
        /// <returns>New instance of <see cref="IDocument"/> with specified content.</returns>
        public IDocument Create(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var document = new TaxPubDocument(content);
            switch (document.XmlDocument.DocumentElement.Name)
            {
                case ElementNames.Article:
                    document.SchemaType = SchemaType.Nlm;
                    break;

                default:
                    document.SchemaType = SchemaType.System;
                    break;
            }

            return document;
        }
    }
}
