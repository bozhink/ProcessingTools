// <copyright file="IAbbreviationModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Abbreviations
{
    /// <summary>
    /// Abbreviation.
    /// </summary>
    public interface IAbbreviationModel
    {
        /// <summary>
        /// Gets the content-type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        string Definition { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        string Value { get; }
    }
}
