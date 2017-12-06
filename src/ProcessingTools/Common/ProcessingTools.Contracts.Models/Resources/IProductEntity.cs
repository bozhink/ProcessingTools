// <copyright file="IProductEntity.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Produce.
    /// </summary>
    public interface IProductEntity : INameableIntegerIdentifiable, IEntityWithSources
    {
    }
}
