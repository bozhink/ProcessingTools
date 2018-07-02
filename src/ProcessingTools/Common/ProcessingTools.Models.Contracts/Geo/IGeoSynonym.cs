// <copyright file="IGeoSynonym.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Base synonym model for geo-objects.
    /// </summary>
    public interface IGeoSynonym : INameableIntegerIdentifiable
    {
        /// <summary>
        /// Gets parent ID.
        /// </summary>
        int ParentId { get; }

        /// <summary>
        /// Gets language code.
        /// </summary>
        int? LanguageCode { get; }
    }
}
