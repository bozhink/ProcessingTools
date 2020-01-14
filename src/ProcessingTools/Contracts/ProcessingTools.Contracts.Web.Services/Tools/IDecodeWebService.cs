﻿// <copyright file="IDecodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Web.Models.Tools.Decode;

namespace ProcessingTools.Contracts.Web.Services.Tools
{
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
        Task<DecodeBase64UrlViewModel> GetDecodeBase64UrlViewModelAsync();

        /// <summary>
        /// Gets <see cref="DecodeBase64ViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="DecodeBase64ViewModel"/>.</returns>
        Task<DecodeBase64ViewModel> GetDecodeBase64ViewModelAsync();

        /// <summary>
        /// Maps <see cref="DecodeBase64RequestModel"/> to <see cref="DecodeBase64ViewModel"/>.
        /// </summary>
        /// <param name="model">Request model to be mapped.</param>
        /// <returns>Task of <see cref="DecodeBase64ViewModel"/>.</returns>
        Task<DecodeBase64ViewModel> MapToViewModelAsync(DecodeBase64RequestModel model);

        /// <summary>
        /// Maps <see cref="DecodeBase64UrlRequestModel"/> to <see cref="DecodeBase64UrlViewModel"/>.
        /// </summary>
        /// <param name="model">Request model to be mapped.</param>
        /// <returns>Task of <see cref="DecodeBase64UrlViewModel"/>.</returns>
        Task<DecodeBase64UrlViewModel> MapToViewModelAsync(DecodeBase64UrlRequestModel model);
    }
}
