// <copyright file="IDocumentValidator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Document validator.
    /// </summary>
    public interface IDocumentValidator : IValidator<IDocument, object>
    {
    }
}
