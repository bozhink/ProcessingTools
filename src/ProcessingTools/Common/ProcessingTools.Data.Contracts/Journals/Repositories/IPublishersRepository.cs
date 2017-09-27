﻿// <copyright file="IPublishersRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Journals.Repositories
{
    using ProcessingTools.Contracts.Data.Journals.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IPublishersRepository : IAddressableRepository, ICrudRepository<IPublisher>
    {
    }
}
