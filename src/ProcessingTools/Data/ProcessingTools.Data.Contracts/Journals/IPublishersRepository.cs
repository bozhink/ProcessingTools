// <copyright file="IPublishersRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Journals
{
    using ProcessingTools.Models.Contracts.Journals;

    /// <summary>
    /// Publishers repository.
    /// </summary>
    public interface IPublishersRepository : IAddressableRepository, ICrudRepository<IPublisher>
    {
    }
}
