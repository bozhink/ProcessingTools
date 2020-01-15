// <copyright file="IBioEnvironmentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioEnvironmentsRepository<T> : IEntityCrudRepository<T>
        where T : class
    {
    }
}
