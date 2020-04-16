﻿// <copyright file="IDocumentFactory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Factory of <see cref="IDocument"/>.
    /// </summary>
    public interface IDocumentFactory : IFactory<IDocument>
    {
        /// <summary>
        /// Creates instance of <see cref="IDocument"/> with specified content.
        /// </summary>
        /// <param name="content">Content of the resultant document.</param>
        /// <returns><see cref="IDocument"/> object with specified content.</returns>
        IDocument Create(string content);
    }
}