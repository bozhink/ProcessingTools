﻿// <copyright file="IAddressableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Journals.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Journals.Models;

    public interface IAddressableRepository
    {
        Task<object> AddAddress(object entityId, IAddress address);

        Task<object> UpdateAddress(object entityId, IAddress address);

        Task<object> RemoveAddress(object entityId, object addressId);
    }
}
