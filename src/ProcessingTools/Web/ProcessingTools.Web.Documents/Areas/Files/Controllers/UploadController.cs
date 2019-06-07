﻿namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Files;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Files.Metadata;

    /// <summary>
    /// /Files/Upload
    /// </summary>
    [Authorize]
    [Area(AreaNames.Files)]
    public class UploadController : Controller
    {
        private readonly IFilesDataService filesDataService;
        private readonly IFileNameResolver fileNameResolver;
        private readonly IFileNameGenerator fileNameGenerator;

        public UploadController(IFilesDataService filesDataService, IFileNameResolver fileNameResolver, IFileNameGenerator fileNameGenerator)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
            this.fileNameResolver = fileNameResolver ?? throw new ArgumentNullException(nameof(fileNameResolver));
            this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
        }

        // GET: Files/Upload
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult UploadSingleFile()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult UploadMultipleFiles()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadSingleFile(IFormFile file)
        {
            if (file == null || file.Length < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var metadata = await this.UploadFile(file).ConfigureAwait(false);

            return this.RedirectToAction("Details", "Metadata", routeValues: metadata);
        }

        private Task<IFileMetadata> UploadFile(IFormFile file)
        {
            if (file == null || file.Length < 1)
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
