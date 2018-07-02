// <copyright file="IAddressableDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Journals
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Services.Data.Journals;

    /// <summary>
    /// Addressable data service.
    /// </summary>
    /// <typeparam name="T">Type of the addressed object.</typeparam>
    public interface IAddressableDataService<T>
        where T : IAddressable
    {
        /// <summary>
        /// Add address to model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="address"><see cref="IAddress"/> model to be attached.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddAddressAsync(object userId, object modelId, IAddress address);

        /// <summary>
        /// Updates address of model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="address"><see cref="IAddress"/> model to be updated.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateAddressAsync(object userId, object modelId, IAddress address);

        /// <summary>
        /// Removes address from model.
        /// </summary>
        /// <param name="userId">User ID for validation.</param>
        /// <param name="modelId">Model ID to be updated.</param>
        /// <param name="addressId">Address ID to be removed.</param>
        /// <returns>Task of result.</returns>
        Task<object> RemoveAddressAsync(object userId, object modelId, object addressId);
    }
}
