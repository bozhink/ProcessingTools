// <copyright file="IPublishersRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Journals
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Journals;

    /// <summary>
    /// Publishers repository.
    /// </summary>
    public interface IPublishersRepository : IAddressableRepository, ICrudRepository<IPublisher>
    {
    }
}
