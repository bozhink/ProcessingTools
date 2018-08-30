// <copyright file="IHashesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Tools
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Tools.Hashes;

    /// <summary>
    /// Hashes web service.
    /// </summary>
    public interface IHashesWebService : IWebPresenter
    {
        /// <summary>
        /// Gets hashes of string content.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="HashesViewModel"/>.</returns>
        Task<HashesViewModel> HashAsync(HashContentRequestModel model);

        /// <summary>
        /// Gets <see cref="HashesViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="HashesViewModel"/>.</returns>
        Task<HashesViewModel> GetHashesViewModelAsync();

        /// <summary>
        /// Maps <see cref="HashContentRequestModel"/> to <see cref="HashesViewModel"/>.
        /// </summary>
        /// <param name="model">Request model to be mapped.</param>
        /// <returns>Task of <see cref="HashesViewModel"/>.</returns>
        Task<HashesViewModel> MapToViewModelAsync(HashContentRequestModel model);
    }
}
