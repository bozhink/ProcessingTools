// <copyright file="UploadController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Files;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Files.Metadata;

    /// <summary>
    /// /Files/Upload.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Files)]
    public class UploadController : Controller
    {
        private readonly IFileNameGenerator fileNameGenerator;
        private readonly IFileNameResolver fileNameResolver;
        private readonly IFilesDataService filesDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadController"/> class.
        /// </summary>
        /// <param name="filesDataService">Instance of <see cref="IFilesDataService"/>.</param>
        /// <param name="fileNameResolver">Instance of <see cref="IFileNameResolver"/>.</param>
        /// <param name="fileNameGenerator">Instance of <see cref="IFileNameGenerator"/>.</param>
        public UploadController(IFilesDataService filesDataService, IFileNameResolver fileNameResolver, IFileNameGenerator fileNameGenerator)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
            this.fileNameResolver = fileNameResolver ?? throw new ArgumentNullException(nameof(fileNameResolver));
            this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
        }

        /// <summary>
        /// GET: Files/Upload.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// GET: Files/UploadMultipleFiles.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult UploadMultipleFiles()
        {
            return this.View();
        }

        /// <summary>
        /// GET: Files/UploadSingleFile.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        public IActionResult UploadSingleFile()
        {
            return this.View();
        }

        /// <summary>
        /// POST: Files/UploadSingleFile.
        /// </summary>
        /// <param name="file">File data.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadSingleFile(IFormFile file)
        {
            if (file is null || file.Length < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var metadata = await this.UploadFile(file).ConfigureAwait(false);

            return this.RedirectToAction("Details", "Metadata", routeValues: metadata);
        }

        private Task<IFileMetadata> UploadFile(IFormFile file)
        {
            if (file is null || file.Length < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var metadata = new FileMetadataModel
            {
                CreatedBy = "system",
                CreatedOn = DateTime.UtcNow,
                ModifiedBy = "system",
                ModifiedOn = DateTime.UtcNow,
                ContentLength = file.Length,
                ContentType = file.ContentType,
                FileExtension = Path.GetExtension(file.FileName),
                FileName = file.FileName,
            };

            string generatedFileName = this.fileNameGenerator.GetNewFileName(file.FileName);

            string fullFileName = this.fileNameResolver.GetFullFileName(generatedFileName);

            metadata.FullName = fullFileName;

            return this.filesDataService.CreateAsync(metadata, file.OpenReadStream());
        }
    }
}
