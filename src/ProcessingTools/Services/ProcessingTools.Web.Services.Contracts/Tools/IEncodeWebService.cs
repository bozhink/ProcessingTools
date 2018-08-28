// <copyright file="IEncodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Tools
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Tools.Encode;

    /// <summary>
    /// Encode web service.
    /// </summary>
    public interface IEncodeWebService : IWebPresenter
    {
        /// <summary>
        /// Encodes string to Base64 string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="EncodeBase64ViewModel"/>.</returns>
        Task<EncodeBase64ViewModel> EncodeBase64Async(EncodeBase64RequestModel model);

        /// <summary>
        /// Encodes string to Base64 URL string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="EncodeBase64UrlViewModel"/>.</returns>
        Task<EncodeBase64UrlViewModel> EncodeBase64UrlAsync(EncodeBase64UrlRequestModel model);

        /// <summary>
        /// Gets <see cref="EncodeBase64UrlViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="EncodeBase64UrlViewModel"/>.</returns>
        Task<EncodeBase64UrlViewModel> GetEncodeBase64UrlViewModelAsync();

        /// <summary>
        /// Gets <see cref="EncodeBase64ViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="EncodeBase64ViewModel"/>.</returns>
        Task<EncodeBase64ViewModel> GetEncodeBase64ViewModelAsync();

        /// <summary>
        /// Maps <see cref="EncodeBase64RequestModel"/> to <see cref="EncodeBase64ViewModel"/>.
        /// </summary>
        /// <param name="model">Request model to be mapped.</param>
        /// <returns>Task of <see cref="EncodeBase64ViewModel"/>.</returns>
        Task<EncodeBase64ViewModel> MapToViewModelAsync(EncodeBase64RequestModel model);

        /// <summary>
        /// Maps <see cref="EncodeBase64UrlRequestModel"/> to <see cref="EncodeBase64UrlViewModel"/>.
        /// </summary>
        /// <param name="model">Request model to be mapped.</param>
        /// <returns>Task of <see cref="EncodeBase64UrlViewModel"/>.</returns>
        Task<EncodeBase64UrlViewModel> MapToViewModelAsync(EncodeBase64UrlRequestModel model);
    }
}
