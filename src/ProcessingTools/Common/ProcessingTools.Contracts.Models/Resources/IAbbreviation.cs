// <copyright file="IAbbreviation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Abbreviation entity.
    /// </summary>
    public interface IAbbreviation : INameableGuidIdentifiable, IDefinable, IContentTypeable, IEntityWithSources
    {
    }
}
