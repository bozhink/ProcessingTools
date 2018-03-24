// <copyright file="IProductEntity.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Resources
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Produce.
    /// </summary>
    public interface IProductEntity : INameableIntegerIdentifiable, IEntityWithSources
    {
    }
}
