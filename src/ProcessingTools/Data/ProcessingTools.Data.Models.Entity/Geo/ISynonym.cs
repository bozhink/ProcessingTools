// <copyright file="ISynonym.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Synonym.
    /// </summary>
    public interface ISynonym : INamedIntegerIdentified
    {
        /// <summary>
        /// Gets the language code.
        /// </summary>
        int? LanguageCode { get; }
    }
}
