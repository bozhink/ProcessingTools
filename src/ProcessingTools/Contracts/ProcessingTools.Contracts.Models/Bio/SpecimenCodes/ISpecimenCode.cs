// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.SpecimenCodes
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
