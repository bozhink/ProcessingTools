// <copyright file="IDocumentGenerator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document generator.
    /// </summary>
    public interface IDocumentGenerator : IGenerator<IDocument, object>
    {
    }
}
