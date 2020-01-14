// <copyright file="IProductEntity.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Resources
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Produce.
    /// </summary>
    public interface IProductEntity : INamedIntegerIdentified, IEntityWithSources
    {
    }
}
