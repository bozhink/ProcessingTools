// <copyright file="MetadataController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Files.Metadata;

    /// <summary>
    /// /Files/Metadata.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Files)]
    public class MetadataController : Controller
    {
        private readonly IFilesDataService filesDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataController"/> class.
        /// </summary>
        /// <param name="filesDataService">Instance of <see cref="IFilesDataService"/>.</param>
        public MetadataController(IFilesDataService filesDataService)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
        }

        /// <summary>
        /// /Files/Metadata.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// /Files/Metadata/Details.
        /// </summary>
        /// <param name="id">ID of file.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var metadata = await this.filesDataService.GetMetadataAsync(id).ConfigureAwait(false);

                var viewmodel = new FileMetadataViewModel
                {
                    Id = metadata.Id,
                    FileName = metadata.FileName,
                    FileExtension = metadata.FileExtension,
                    FullName = metadata.FullName,
                    ContentLength = metadata.ContentLength,
                    ContentType = metadata.ContentType,
                    Description = metadata.Description,
                    CreatedBy = metadata.CreatedBy,
                    CreatedOn = metadata.CreatedOn,
                    ModifiedBy = metadata.ModifiedBy,
                    ModifiedOn = metadata.ModifiedOn,
                };

                return this.View(viewmodel);
            }

            return this.View();
        }

        /// <summary>
        /// /Files/Metadata/Details.
        /// </summary>
        /// <param name="model">File metadata model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public IActionResult Details(FileMetadataModel model)
        {
            if (model != null)
            {
                var viewmodel = new FileMetadataViewModel
                {
                    Id = model.Id,
                    FileName = model.FileName,
                    FileExtension = model.FileExtension,
                    FullName = model.FullName,
                    ContentLength = model.ContentLength,
                    ContentType = model.ContentType,
                    Description = model.Description,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = model.CreatedOn,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedOn = model.ModifiedOn,
                };

                return this.View(viewmodel);
            }

            return this.View();
        }
    }
}
