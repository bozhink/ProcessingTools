namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Web.Documents.Areas.Files.ViewModels.Metadata;

    public class MetadataController : Controller
    {
        private readonly IStreamingFilesDataService filesDataService;

        public MetadataController(IStreamingFilesDataService filesDataService)
        {
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
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

            var metadata = await this.filesDataService.GetMetadataAsync(id).ConfigureAwait(false);

            var viewmodel = new FileMetadataViewModel
            {
                ContentLength = metadata.ContentLength,
                ContentType = metadata.ContentType,
                CreatedBy = metadata.CreatedBy,
                CreatedOn = metadata.CreatedOn,
                ModifiedOn = metadata.ModifiedOn,
                Description = metadata.Description,
                FileExtension = metadata.FileExtension,
                FileName = metadata.FileName,
                FullName = metadata.FullName,
                Id = metadata.Id,
                ModifiedBy = metadata.ModifiedBy
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewmodel);
        }
    }
}
