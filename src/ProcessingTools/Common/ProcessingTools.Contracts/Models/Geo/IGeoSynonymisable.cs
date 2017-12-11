// <copyright file="IGeoSynonymisable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;

    /// <summary>
    /// Model with geo-synonym.
    /// </summary>
    /// <typeparam name="T">Type of synonym model.</typeparam>
    public interface IGeoSynonymisable<T>
        where T : IGeoSynonym
    {
        /// <summary>
        /// Gets synonyms.
        /// </summary>
        ICollection<T> Synonyms { get; }
    }
}
