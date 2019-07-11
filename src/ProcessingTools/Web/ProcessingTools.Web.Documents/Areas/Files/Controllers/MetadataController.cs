// <copyright file="MetadataController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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

        public MetadataController(IFilesDataService filesDataService)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

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

        public async Task<ActionResult> Details(FileMetadataModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

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
    }
}
