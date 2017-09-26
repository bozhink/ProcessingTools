// <copyright file="IAbbreviation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.DataResources.Models
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Abbreviation entity.
    /// </summary>
    public interface IAbbreviation : INameableGuidIdentifiable, IDefinable, IContentTypeable, IEntityWithSources
    {
    }
}
