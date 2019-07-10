// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services.Models.Bio.SpecimenCodes
{
    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode : IContent, IContentTyped
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        string Url { get; }
    }
}
