// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.Contracts.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Models;

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
