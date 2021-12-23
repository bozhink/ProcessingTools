// <copyright file="EntityWithSources.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.DataResources
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Abstract entity with sources.
    /// </summary>
    public abstract class EntityWithSources : IEntityWithSources
    {
        /// <summary>
        /// Gets the collection of sources.
        /// </summary>
        public virtual ICollection<SourceId> Sources { get; private set; } = new HashSet<SourceId>();

        /// <inheritdoc/>
        [NotMapped]
        IEnumerable<ISourceIdEntity> IEntityWithSources.Sources => this.Sources;
    }
}
