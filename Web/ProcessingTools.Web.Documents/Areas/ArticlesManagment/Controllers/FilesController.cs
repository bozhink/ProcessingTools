namespace ProcessingTools.Web.Documents.Areas.ArticlesManagment.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using ProcessingTools.Common;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Xml.Extensions;

    using ViewModels.Files;

    [Authorize]
    public class FilesController : Controller
    {
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

        // GET: File/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: File/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            if (Request?.Files == null || Request.Files.Count < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No files selected.");
            }

            try
            {
                var file = Request.Files[0];
                if (file == null || file.ContentLength < 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid or empty file.");
                }

                var fileMetatadata = new XmlFileMetadataServiceModel
                {
                    FileName = Path.GetFileNameWithoutExtension(file.FileName),
                    FileExtension = Path.GetExtension(file.FileName),
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType
                };

                await this.service.Create(User.Identity.GetUserId(), this.fakeArticleId, fileMetatadata, file.InputStream);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Create));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: File/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await this.service.Delete(User.Identity.GetUserId(), this.fakeArticleId, id);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: File/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: File/Details/5
        public async Task<ActionResult> Details(string id)
        {
            try
            {
                var file = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

                var model = new FileDetailsViewModel
                {
                    Id = id,
                    Content = file.Content.ApplyXslTransform(this.XslTansformFile)
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Details));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: File/Download/5
        public async Task<ActionResult> Download(string id)
        {
            try
            {
                var file = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);
                var bytes = Defaults.DefaultEncoding.GetBytes(file.Content);
                return this.File(
                    fileContents: bytes,
                    contentType: file.ContentType,
                    fileDownloadName: $"{file.FileName.Trim('.')}.{file.FileExtension.Trim('.')}");
            }
            catch (Exception e)
            {
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Details));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }

        // GET: File/Edit/5
        public ActionResult Edit(string id)
        {
            // TODO
            var error = new HandleErrorInfo(new NotImplementedException(), ControllerName, nameof(this.Edit));
            return this.View(ViewConstants.DefaultErrorViewName, error);
        }

        // POST: File/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            // TODO
            var error = new HandleErrorInfo(new NotImplementedException(), ControllerName, nameof(this.Edit));
            return this.View(ViewConstants.DefaultErrorViewName, error);
        }

        // GET: File
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
                var error = new HandleErrorInfo(e, ControllerName, nameof(this.Index));
                return this.View(ViewConstants.DefaultErrorViewName, error);
            }
        }
    }
}
