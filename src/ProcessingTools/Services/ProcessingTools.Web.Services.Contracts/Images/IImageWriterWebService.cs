// <copyright file="IImageWriterWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Images
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Image writer web service.
    /// </summary>
    public interface IImageWriterWebService
    {
        /// <summary>
        /// Uploads image file.
        /// </summary>
        /// <param name="file">Image file to be uploaded.</param>
        /// <returns>Operation message.</returns>
        Task<string> UploadImageAsync(IFormFile file);
    }
}
