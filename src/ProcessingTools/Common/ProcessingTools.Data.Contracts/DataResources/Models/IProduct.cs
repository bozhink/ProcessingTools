// <copyright file="IProduct.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.DataResources.Models
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Produce.
    /// </summary>
    public interface IProduct : INameableIntegerIdentifiable, IEntityWithSources
    {
    }
}
