// <copyright file="ISynonymisable.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;

    /// <summary>
    /// Model with synonyms.
    /// </summary>
    /// <typeparam name="T">Type of the model with synonyms.</typeparam>
    public interface ISynonymisable<T>
        where T : ISynonym
    {
        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        ICollection<T> Synonyms { get; }
    }
}
