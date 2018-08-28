// <copyright file="IDecodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Tools
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Tools.Decode;

    /// <summary>
    /// Decode web service.
    /// </summary>
    public interface IDecodeWebService : IWebPresenter
    {
        /// <summary>
        /// Decodes Base64 string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="DecodeBase64ViewModel"/>.</returns>
        Task<DecodeBase64ViewModel> DecodeBase64Async(DecodeBase64RequestModel model);

        /// <summary>
        /// Decodes Base64 URL string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of <see cref="DecodeBase64UrlViewModel"/>.</returns>
        Task<DecodeBase64UrlViewModel> DecodeBase64UrlAsync(DecodeBase64UrlRequestModel model);

        /// <summary>
        /// Gets <see cref="DecodeBase64UrlViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="DecodeBase64UrlViewModel"/>.</returns>
        Task<DecodeBase64UrlViewModel> GetBase64UrlViewModelAsync();

        /// <summary>
        /// Gets <see cref="DecodeBase64ViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="DecodeBase64ViewModel"/>.</returns>
        Task<DecodeBase64ViewModel> GetDecodeBase64ViewModelAsync();
    }
}
