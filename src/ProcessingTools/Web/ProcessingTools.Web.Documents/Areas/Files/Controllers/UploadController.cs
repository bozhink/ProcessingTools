namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Models.Contracts.Files;
    using ProcessingTools.Contracts.Services.Data.Files;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Web.Documents.Areas.Files.Models;

    [Authorize]
    public class UploadController : Controller
    {
        private readonly IStreamingFilesDataService filesDataService;

        public UploadController(IStreamingFilesDataService filesDataService)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
        }

        // GET: Files/Upload
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet]
        public ActionResult UploadSingleFile()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet]
        public ActionResult UploadMultipleFiles()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadSingleFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var userId = this.User.Identity.GetUserId();

            var metadata = await this.UploadSingleFile(userId, file).ConfigureAwait(false);

            this.Response.StatusCode = (int)HttpStatusCode.Created;

            return this.View(
                viewName: "~/Areas/Files/Views/Metadata/Details.cshtml",
                model: new ViewModels.Metadata.FileMetadataViewModel
                {
                    ContentLength = metadata.ContentLength,
                    ContentType = metadata.ContentType,
                    CreatedByUser = metadata.CreatedByUser,
                    DateCreated = metadata.DateCreated,
                    DateModified = metadata.DateModified,
                    Description = metadata.Description,
                    FileExtension = metadata.FileExtension,
                    FileName = metadata.FileName,
                    FullName = metadata.FullName,
                    Id = metadata.Id,
                    ModifiedByUser = metadata.ModifiedByUser
                });

            ////return this.RedirectToAction(
            ////    actionName: nameof(MetadataController.Details),
            ////    controllerName: ControllerNames.MetadataControllerName,
            ////    routeValues: new
            ////    {
            ////        id = metadata.Id
            ////    });
        }

        private Task<IFileMetadata> UploadSingleFile(object userId, HttpPostedFileBase file)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (file == null || file.ContentLength < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var user = userId.ToString();

            var metadata = new FileMetadataModel
            {
                CreatedByUser = user,
                ModifiedByUser = user,
                ContentLength = file.ContentLength,
                ContentType = file.ContentType,
                FileExtension = Path.GetExtension(file.FileName).Trim('.'),
                FileName = Path.GetFileNameWithoutExtension(file.FileName).Trim('.'),
            };

            string fullName = Path.Combine(
                this.Server.MapPath("~/App_Data/"),
                $"{metadata.FileName}-{Guid.NewGuid().ToString()}.{metadata.FileExtension}");

            metadata.FullName = fullName;

            return this.filesDataService.CreateAsync(metadata, file.InputStream);
        }
    }
}
