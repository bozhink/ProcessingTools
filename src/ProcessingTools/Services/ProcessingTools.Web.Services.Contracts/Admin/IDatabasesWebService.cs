// <copyright file="IDatabasesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Admin
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Admin.Databases;

    /// <summary>
    /// Databases service.
    /// </summary>
    public interface IDatabasesWebService : IWebPresenter
    {
        /// <summary>
        /// Initialize all databases.
        /// </summary>
        /// <returns>Result response model.</returns>
        Task<InitializeResponseModel> InitializeAllAsync();

        /// <summary>
        /// Map <see cref="InitializeResponseModel"/> to <see cref="InitializeViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<InitializeViewModel> MapToViewModelAsync(InitializeResponseModel model);
    }
}
