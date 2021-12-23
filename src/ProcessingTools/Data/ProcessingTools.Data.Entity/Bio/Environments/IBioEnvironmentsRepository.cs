// <copyright file="IBioEnvironmentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    /// <summary>
    /// Bio environments repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface IBioEnvironmentsRepository<T> : IEntityCrudRepository<T>
        where T : class
    {
    }
}
