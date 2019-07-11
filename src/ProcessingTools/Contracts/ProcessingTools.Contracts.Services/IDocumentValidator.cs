// <copyright file="IDocumentValidator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Validation;

    /// <summary>
    /// Document validator.
    /// </summary>
    public interface IDocumentValidator : IValidator<IDocument, object>
    {
    }
}
