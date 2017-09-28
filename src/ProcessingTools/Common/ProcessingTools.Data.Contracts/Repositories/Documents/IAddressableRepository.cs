// <copyright file="IAddressableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Repository related to entities with addresses.
    /// </summary>
    public interface IAddressableRepository
    {
        /// <summary>
        /// Adds address to object.
        /// </summary>
        /// <param name="entityId">ID of the entity to be updated.</param>
        /// <param name="address">Address to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddAddressAsync(object entityId, IAddress address);

        /// <summary>
        /// Removes address of object.
        /// </summary>
        /// <param name="entityId">ID of the entity to be updated.</param>
        /// <param name="addressId">Address ID to be removed.</param>
        /// <returns>Task</returns>
        Task<object> RemoveAddressAsync(object entityId, object addressId);
    }
}
