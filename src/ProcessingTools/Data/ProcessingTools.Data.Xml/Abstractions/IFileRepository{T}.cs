// <copyright file="IFileRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Abstractions
{
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Generic file repository.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IFileRepository<T> : ICrudRepository<T>, IIterableRepository<T>
        where T : class
    {
    }
}
