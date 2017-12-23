// <copyright file="IAddressableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Journals
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Journals;

    /// <summary>
    /// Addressable repository.
    /// </summary>
    public interface IAddressableRepository
    {
        /// <summary>
        /// Add address to entity.
        /// </summary>
        /// <param name="entityId">ID of the entity to be updated.</param>
        /// <param name="address">Address to be added.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAddress(object entityId, IAddress address);

        /// <summary>
        /// Update address of a specified entity.
        /// </summary>
        /// <param name="entityId">ID of the entity to be updated.</param>
        /// <param name="address">Address to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateAddress(object entityId, IAddress address);

        /// <summary>
        /// Remove address from an entity.
        /// </summary>
        /// <param name="entityId">ID of the entity to be updated.</param>
        /// <param name="addressId">ID of the address to be removed.</param>
        /// <returns>Task of result.</returns>
        Task<object> RemoveAddress(object entityId, object addressId);
    }
}
