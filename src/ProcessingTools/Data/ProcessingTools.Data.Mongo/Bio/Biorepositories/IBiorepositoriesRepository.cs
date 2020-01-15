// <copyright file="IBiorepositoriesRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using ProcessingTools.Data.Mongo.Abstractions;

    public interface IBiorepositoriesRepository<T> : IMongoCrudRepository<T>
        where T : class
    {
    }
}
