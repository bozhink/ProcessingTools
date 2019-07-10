// <copyright file="IDocumentValidator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Services.Validation;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document validator.
    /// </summary>
    public interface IDocumentValidator : IValidator<IDocument, object>
    {
    }
}
