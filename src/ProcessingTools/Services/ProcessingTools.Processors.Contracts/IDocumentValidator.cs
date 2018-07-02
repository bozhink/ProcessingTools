// <copyright file="IDocumentValidator.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document validator.
    /// </summary>
    public interface IDocumentValidator : IValidator<IDocument, object>
    {
    }
}
