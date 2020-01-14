// <copyright file="IDocumentGenerator.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document generator.
    /// </summary>
    public interface IDocumentGenerator : IGenerator<IDocument, object>
    {
    }
}
