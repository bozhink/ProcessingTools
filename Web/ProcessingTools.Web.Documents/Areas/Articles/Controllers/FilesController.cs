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
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class FilesController : Controller
    {
        // TODO: To be removed
        private readonly int fakeArticleId = 0;

        private readonly IDocumentsDataService service;

        public FilesController(IDocumentsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        // GET: /Articles/Files/Delete/5
        [HttpGet, ActionName(ActionNames.DeafultDeleteActionName)]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NullIdErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultDeleteActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

            var model = new DocumentViewModel
            {
                Id = id.ToString(),
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

        // POST: /Articles/Files/Delete/5
        [HttpPost, ActionName(ActionNames.DeafultDeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await this.service.Delete(User.Identity.GetUserId(), this.fakeArticleId, id);
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: /Articles/Files/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return this.NullIdErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultDetailsActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

            var model = new DocumentViewModel
            {
                Id = id.ToString(),
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

        // GET: /Articles/Files/Download/5
        [HttpGet]
        public async Task<ActionResult> Download(Guid? id)
        {
            if (id == null)
            {
                return this.NullIdErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultDownloadActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            var document = await this.service.Get(User.Identity.GetUserId(), this.fakeArticleId, id);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            var stream = await this.service.GetStream(User.Identity.GetUserId(), this.fakeArticleId, id);
            return this.File(
                fileStream: stream,
                contentType: document.ContentType,
                fileDownloadName: $"{document.FileName.Trim('.')}.{document.FileExtension.Trim('.')}");
        }

        // GET: /Articles/Files/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return this.NullIdErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultEditActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            var model = new DocumentViewModel
            {
                Id = id.ToString()
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(model);
        }

        /// <summary>
        /// Index of file list.
        /// </summary>
        /// <param name="p">Page number.</param>
        /// <param name="n">Number of items per page.</param>
        /// <returns></returns>
        /// <example>GET: /Articles/Files</example>
        [HttpGet]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PagingConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PagingConstants.DefaultLargeNumberOfItemsPerPage;

            var userId = User.Identity.GetUserId();

            var items = (await this.service.All(userId, this.fakeArticleId, currentPage, numberOfItemsPerPage))
                .Select(d => new DocumentViewModel
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    DateCreated = d.DateCreated,
                    DateModified = d.DateModified
                })
                .ToArray();

            var numberOfDocuments = await this.service.Count(userId, this.fakeArticleId);

            var viewModel = new ListWithPagingViewModel<DocumentViewModel>(nameof(this.Index), numberOfDocuments, numberOfItemsPerPage, currentPage, items);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Articles/Files/Preview/5
        [HttpGet]
        public ActionResult Preview(Guid? id)
        {
            if (id == null)
            {
                return this.NullIdErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultPreviewActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            var model = new DocumentViewModel
            {
                Id = id.ToString()
            };

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(model);
        }

        // GET: /Articles/Files/Upload
        [HttpGet]
        public ActionResult Upload()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        // POST: /Articles/Files/Upload
        [HttpPost]
        public async Task<ActionResult> Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || files.Count() < 1 || files.All(f => f == null))
            {
                return this.NoFilesSelectedErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

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
                return this.InvalidOrEmptyFileErrorView(InstanceNames.FilesControllerInstanceName, string.Empty, ContentConstants.DefaultUploadNewFileActionLinkTitle, AreasConstants.ArticlesAreaName);
            }

            this.Response.StatusCode = (int)HttpStatusCode.Created;
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // TODO
            string actionName = this.Request.RequestContext.RouteData.Values["action"].ToString();

            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    actionName,
                    AreasConstants.ArticlesAreaName);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    actionName,
                    AreasConstants.ArticlesAreaName);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    actionName,
                    AreasConstants.ArticlesAreaName);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    actionName,
                    AreasConstants.ArticlesAreaName);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    ContentConstants.DefaultDeleteActionLinkTitle,
                    AreasConstants.ArticlesAreaName);
            }

            filterContext.ExceptionHandled = true;
        }
    }
}
