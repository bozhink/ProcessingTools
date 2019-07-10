// <copyright file="IImageWriterWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProcessingTools.Contracts.Web.Services.Images
{
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
