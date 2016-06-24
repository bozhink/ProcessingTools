namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Xml.Extensions;

    using ViewModels.Files;

    [Authorize]
    public class FilesController : Controller
    {
        private const string NoFilesSelectedErrorViewName = "NoFilesSelectedError";
        private const string InvalidOrEmptyFileErrorViewName = "InvalidOrEmptyFileError";
        private const string NullIdErrorViewName = "NullIdError";

        private readonly IXmlFilesDataService service;

        // TODO: To be removed
        private readonly int fakeArticleId = 0;

        public FilesController(IXmlFilesDataService filesDataService)
        {
            if (filesDataService == null)
            {
                throw new ArgumentNullException(nameof(filesDataService));
            }

            this.service = filesDataService;
        }

        public static string ControllerName => ControllerConstants.FilesControllerName;

        private string XslTansformFile => Path.Combine(Server.MapPath("~/App_Code/Xsl"), "main.xsl");

        // GET: Files/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Files/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            if (Request?.Files == null || Request.Files.Count < 1)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NoFilesSelectedErrorViewName);
            }

            try
            {
                var file = Request.Files[0];
                if (file == null || file.ContentLength < 1)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.View(InvalidOrEmptyFileErrorViewName);
                }

                var fileMetatadata = new XmlFileMetadataServiceModel
                {
                    FileName = Path.GetFileNameWithoutExtension(file.FileName),
                    FileExtension = Path.GetExtension(file.FileName),
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType
                };

                await this.service.Create(User.Identity.GetUserId(), this.fakeArticleId, fileMetatadata, file.InputStream);

                this.Response.StatusCode = (int)HttpStatusCode.Created;
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Files/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            try
            {
                await this.service.Delete(User.Identity.GetUserId(), this.fakeArticleId, id);
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // POST: Files/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Files/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            try
            {
                var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);
                if (document == null)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return this.View(ViewConstants.DefaultNotFaoundViewName);
                }

                var model = new FileMetadataViewModel
                {
                    Id = id,
                    FileName = document.FileName,
                    FileExtension = document.FileExtension,
                    ContentType = document.ContentType,
                    ContentLength = document.ContentLength,
                    DateCreated = document.DateCreated,
                    DateModified = document.DateModified
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Files/Download/5
        public async Task<ActionResult> Download(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            try
            {
                var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);
                if (document == null)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return this.View(ViewConstants.DefaultNotFaoundViewName);
                }

                var stream = await this.service.GetStream(User.Identity.GetUserId(), this.fakeArticleId, id);
                return this.File(
                    fileStream: stream,
                    contentType: document.ContentType,
                    fileDownloadName: $"{document.FileName.Trim('.')}.{document.FileExtension.Trim('.')}");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Files/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            // TODO
            return new HttpStatusCodeResult(HttpStatusCode.NotImplemented);
        }

        // POST: Files/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            // TODO
            return new HttpStatusCodeResult(HttpStatusCode.NotImplemented);
        }

        // GET: Files
        public async Task<ActionResult> Index()
        {
            try
            {
                int pageNumber = 0;
                int itemsPerPage = 20;

                var files = (await this.service.All(User.Identity.GetUserId(), this.fakeArticleId, pageNumber, itemsPerPage))
                    .Select(f => new FileMetadataViewModel
                    {
                        Id = f.Id,
                        FileName = f.FileName,
                        DateCreated = f.DateCreated,
                        DateModified = f.DateModified
                    })
                    .ToList();

                return this.View(files);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Index));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: Files/Preview/5
        public async Task<ActionResult> Preview(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View(NullIdErrorViewName);
            }

            try
            {
                var content = (await this.service.GetReader(User.Identity.GetUserId(), this.fakeArticleId, id))
                    .ApplyXslTransform(this.XslTansformFile);

                var model = new FileDetailsViewModel
                {
                    Id = id,
                    Content = content
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }
    }
}
