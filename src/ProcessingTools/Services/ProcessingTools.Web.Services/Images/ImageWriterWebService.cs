// <copyright file="ImageWriterWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Web.Services.Images;
using ProcessingTools.Services.Imaging;

namespace ProcessingTools.Web.Services.Images
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Image writer web service.
    /// </summary>
    public class ImageWriterWebService : IImageWriterWebService
    {
        /// <inheritdoc/>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (this.CheckIfImageFile(file))
            {
                return await this.WriteFileAsync(file).ConfigureAwait(false);
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Checks if file is valid image file.
        /// </summary>
        /// <param name="file">File to be validated.</param>
        /// <returns>Validation result.</returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImagesHelper.GetImageFormat(fileBytes) != ImageFormat.Unknown;
        }

        /// <summary>
        /// Writes file onto the disk.
        /// </summary>
        /// <param name="file">File to be processed.</param>
        /// <returns>Operation message.</returns>
        private async Task<string> WriteFileAsync(IFormFile file)
        {
            string fileName;
            try
            {
                string extension = Path.GetExtension(file.FileName);
                fileName = Guid.NewGuid().ToString() + extension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string directory = Path.GetDirectoryName(path);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return fileName;
        }
    }
}
