// <copyright file="IAbbreviation.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using System;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Abbreviation entity.
    /// </summary>
    public interface IAbbreviation : INamed, IIdentified<Guid>, IDefined, IContentTyped, IEntityWithSources
    {
    }
}
