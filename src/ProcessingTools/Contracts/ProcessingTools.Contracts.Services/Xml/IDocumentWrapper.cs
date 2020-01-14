﻿// <copyright file="IDocumentWrapper.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Xml
{
    using System.Xml;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document wrapper.
    /// </summary>
    public interface IDocumentWrapper
    {
        /// <summary>
        /// Creates new instance of <see cref="IDocument"/>.
        /// </summary>
        /// <returns>The new instance of <see cref="IDocument"/>.</returns>
        IDocument Create();

        /// <summary>
        /// Creates new instance of <see cref="IDocument"/> from specified context and schema type.
        /// </summary>
        /// <param name="context">Context to be wrapped in new <see cref="IDocument"/> instance.</param>
        /// <param name="schemaType">The <see cref="SchemaType"/> of the output <see cref="IDocument"/> instance.</param>
        /// <returns>The new instance of <see cref="IDocument"/> which wraps the specified context.</returns>
        IDocument Create(XmlNode context, SchemaType schemaType);
    }
}
