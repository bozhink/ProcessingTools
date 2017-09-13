namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Contracts.Services.Data.Files;
    using ViewModels.Metadata;

    public class MetadataController : Controller
    {
        private readonly IStreamingFilesDataService filesDataService;

        public MetadataController(IStreamingFilesDataService filesDataService)
        {
            if (filesDataService == null)
            {
                throw new ArgumentNullException(nameof(filesDataService));
            }

            this.filesDataService = filesDataService;
        }

        // GET: /Files/Metadata
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var metadata = await this.filesDataService.GetMetadata(id).ConfigureAwait(false);

            var viewmodel = new FileMetadataViewModel
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
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewmodel);
        }
    }
}
