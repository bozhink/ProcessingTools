// <copyright file="ISpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Processors.Bio.Codes
{
    /// <summary>
    /// Specimen code.
    /// </summary>
    public interface ISpecimenCode
    {
        /// <summary>
        /// Gets code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets full string.
        /// </summary>
        string FullString { get; }

        /// <summary>
        /// Gets prefix.
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// Gets type.
        /// </summary>
        string Type { get; }
    }
}
