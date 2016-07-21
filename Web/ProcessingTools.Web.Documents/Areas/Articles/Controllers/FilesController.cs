namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class FilesController : Controller
    {
        public const string InstanceName = "File";

        // TODO: To be removed
        private readonly int fakeArticleId = 0;

        private readonly IXmlPresenter presenter;
        private readonly IDocumentsDataService service;

        public FilesController(IDocumentsDataService service, IXmlPresenter presenter)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (presenter == null)
            {
                throw new ArgumentNullException(nameof(presenter));
            }

            this.service = service;
            this.presenter = presenter;
        }

        public static string ControllerName => ControllerConstants.FilesControllerName;

        // GET: Files/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NullIdErrorView(InstanceName, string.Empty, ContentConstants.DefaultDeleteActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                await this.service.Delete(User.Identity.GetUserId(), this.fakeArticleId, id);
                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultDeleteActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        // POST: Files/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Files/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NullIdErrorView(InstanceName, string.Empty, ContentConstants.DefaultDetailsActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

                var model = new DocumentViewModel
                {
                    Id = id,
                    FileName = document.FileName,
                    FileExtension = document.FileExtension,
                    ContentType = document.ContentType,
                    ContentLength = document.ContentLength,
                    DateCreated = document.DateCreated,
                    DateModified = document.DateModified
                };

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.View(model);
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceName, e.Message, ContentConstants.DefaultDetailsActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultDetailsActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultDetailsActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        // GET: Files/Download/5
        public async Task<ActionResult> Download(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NullIdErrorView(InstanceName, string.Empty, ContentConstants.DefaultDownloadActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                var stream = await this.service.GetStream(User.Identity.GetUserId(), this.fakeArticleId, id);
                return this.File(
                    fileStream: stream,
                    contentType: document.ContentType,
                    fileDownloadName: $"{document.FileName.Trim('.')}.{document.FileExtension.Trim('.')}");
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceName, e.Message, ContentConstants.DefaultDownloadActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultDownloadActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultDownloadActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        // GET: Files/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NullIdErrorView(InstanceName, string.Empty, ContentConstants.DefaultEditActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                var model = new DocumentViewModel
                {
                    Id = id
                };

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.View(model);
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        /// <summary>
        /// Index of file list.
        /// </summary>
        /// <param name="p">Page number.</param>
        /// <param name="n">Number of items per page.</param>
        /// <returns></returns>
        /// <example>GET: Files</example>
        public async Task<ActionResult> Index(int? p, int? n)
        {
            try
            {
                int pageNumber = p ?? PagingConstants.DefaultPageNumber;
                int itemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

                var userId = User.Identity.GetUserId();

                var viewModels = (await this.service.All(userId, this.fakeArticleId, pageNumber, itemsPerPage))
                    .Select(d => new DocumentViewModel
                    {
                        Id = d.Id,
                        FileName = d.FileName,
                        DateCreated = d.DateCreated,
                        DateModified = d.DateModified
                    })
                    .ToList();

                var numberOfDocuments = await this.service.Count(userId, this.fakeArticleId);

                this.ViewBag.PageNumber = pageNumber;
                this.ViewBag.NumberOfItemsPerPage = itemsPerPage;
                this.ViewBag.NumberOfPages = (numberOfDocuments % itemsPerPage) == 0 ? numberOfDocuments / itemsPerPage : (numberOfDocuments / itemsPerPage) + 1;
                this.ViewBag.ActionName = nameof(this.Index);

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.View(viewModels);
            }
            catch (InvalidPageNumberException e)
            {
                return this.InvalidPageNumberErrorView(InstanceName, e.Message, ContentConstants.DefaultBackToListActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (InvalidItemsPerPageException e)
            {
                return this.InvalidNumberOfItemsPerPageErrorView(InstanceName, e.Message, ContentConstants.DefaultBackToListActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultIndexActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultIndexActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        // GET: Files/Preview/5
        public ActionResult Preview(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.NullIdErrorView(InstanceName, string.Empty, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                var model = new DocumentViewModel
                {
                    Id = id
                };

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.View(model);
            }
            catch (EntityNotFoundException e)
            {
                return this.DefaultNotFoundView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }

        // GET: Files/Upload
        public ActionResult Upload()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        // POST: Files/Upload
        [HttpPost]
        public async Task<ActionResult> Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || files.Count() < 1 || files.All(f => f == null))
            {
                return this.NoFilesSelectedErrorView(InstanceName, string.Empty, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            try
            {
                var userId = User.Identity.GetUserId();
                var articleId = this.fakeArticleId;

                var tasks = new ConcurrentQueue<Task>();
                var invalidFiles = new ConcurrentQueue<string>();
                foreach (var file in files)
                {
                    if (file == null || file.ContentLength < 1)
                    {
                        invalidFiles.Enqueue(file.FileName);
                    }
                    else
                    {
                        try
                        {
                            var document = new DocumentServiceModel
                            {
                                FileName = Path.GetFileNameWithoutExtension(file.FileName).Trim('.'),
                                FileExtension = Path.GetExtension(file.FileName).Trim('.'),
                                ContentLength = file.ContentLength,
                                ContentType = file.ContentType
                            };

                            tasks.Enqueue(this.service.Create(userId, articleId, document, file.InputStream));
                        }
                        catch
                        {
                            invalidFiles.Enqueue(file.FileName);
                        }
                    }
                }

                await Task.WhenAll(tasks.ToArray());

                if (invalidFiles.Count > 0)
                {
                    this.ViewBag.InvalidFiles = invalidFiles.OrderBy(f => f).ToList();
                    return this.InvalidOrEmptyFileErrorView(InstanceName, string.Empty, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
                }

                this.Response.StatusCode = (int)HttpStatusCode.Created;
                return this.RedirectToAction(nameof(this.Index));
            }
            catch (ArgumentException e)
            {
                return this.BadRequestErrorView(InstanceName, e.Message, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
            catch (Exception e)
            {
                return this.DefaultErrorView(InstanceName, e.Message, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
            }
        }
    }
}
