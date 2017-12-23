﻿namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
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
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Web;
    using ProcessingTools.Contracts.Services.Data.Documents;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Data.Documents;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Models.Shared;
    using Strings = Resources.Strings;

    [Authorize]
    public class FilesController : Controller
    {
        private const string DocumentValidationBinding = nameof(FileDetailsViewModel.DocumentId) + "," + nameof(FileDetailsViewModel.FileName) + "," + nameof(FileDetailsViewModel.FileExtension) + "," + nameof(FileDetailsViewModel.ContentType) + "," + nameof(FileDetailsViewModel.Comment);

        private readonly IDocumentsDataService service;

        public FilesController(IDocumentsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private string UserId => this.User.Identity.GetUserId();

        // TODO: To be removed
        private int FakeArticleId => 0;

        // GET: /Articles/Files/Delete/5
        [HttpGet, ActionName(ActionNames.DeafultDeleteActionName)]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var viewModel = await this.GetDetails(userId, articleId, id).ConfigureAwait(false);

            return this.View(viewModel);
        }

        // POST: /Articles/Files/Delete/5
        [HttpPost, ActionName(ActionNames.DeafultDeleteActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await this.service.DeleteAsync(this.UserId, this.FakeArticleId, id).ConfigureAwait(false);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: /Articles/Files/DeleteAll
        [HttpGet, ActionName(ActionNames.DeafultDeleteAllActionName)]
        public ActionResult DeleteAll()
        {
            return this.View();
        }

        // POST: /Articles/Files/DeleteAll
        [HttpPost, ActionName(ActionNames.DeafultDeleteAllActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAllConfirmed()
        {
            await this.service.DeleteAllAsync(this.UserId, this.FakeArticleId).ConfigureAwait(false);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: /Articles/Files/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var viewModel = await this.GetDetails(userId, articleId, id).ConfigureAwait(false);

            return this.View(viewModel);
        }

        // GET: /Articles/Files/Download/5
        [HttpGet]
        public async Task<ActionResult> Download(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var document = await this.service.GetAsync(userId, articleId, id).ConfigureAwait(false);

            var stream = await this.service.GetStreamAsync(userId, articleId, id).ConfigureAwait(false);

            return this.File(
                fileStream: stream,
                contentType: document.ContentType,
                fileDownloadName: $"{document.FileName.Trim('.')}.{document.FileExtension.Trim('.')}");
        }

        // GET: /Articles/Files/Edit/5
        [HttpGet, ActionName(ActionNames.DeafultEditActionName)]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var viewModel = await this.GetDetails(userId, articleId, id).ConfigureAwait(false);

            return this.View(viewModel);
        }

        // POST: /Articles/Files/Edit/5
        [HttpPost, ActionName(ActionNames.DeafultEditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = DocumentValidationBinding)] FileDetailsViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.UserId;
                var articleId = this.FakeArticleId;

                await this.service.UpdateMetaAsync(
                    userId,
                    articleId,
                    new Document
                    {
                        Id = model.DocumentId,
                        Comment = model.Comment,
                        ContentType = model.ContentType,
                        FileExtension = model.FileExtension,
                        FileName = model.FileName
                    })
                    .ConfigureAwait(false);

                this.Response.StatusCode = (int)HttpStatusCode.Redirect;
                return this.RedirectToAction(nameof(this.Index));
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.View(model);
        }

        // GET: /Articles/Files/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        /// <summary>
        /// Index of file list.
        /// </summary>
        /// <param name="p">Page number.</param>
        /// <param name="n">Number of items per page.</param>
        /// <returns>Paginated items</returns>
        /// <example>GET: /Articles/Files</example>
        [HttpGet]
        public async Task<ActionResult> Index(int? p, int? n)
        {
            int currentPage = p ?? PaginationConstants.DefaultPageNumber;
            int numberOfItemsPerPage = n ?? PaginationConstants.DefaultLargeNumberOfItemsPerPage;

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var items = (await this.service.AllAsync(userId, articleId, currentPage, numberOfItemsPerPage).ConfigureAwait(false))
                .Select(d => new FileViewModel
                {
                    ArticleId = articleId.ToString(),
                    DocumentId = d.Id.ToString(),
                    FileName = d.FileName,
                    Comment = d.Comment,
                    DateCreated = d.DateCreated,
                    DateModified = d.DateModified
                })
                .ToArray();

            var numberOfDocuments = await this.service.CountAsync(userId, articleId).ConfigureAwait(false);

            var viewModel = new ListWithPagingViewModel<FileViewModel>(nameof(this.Index), numberOfDocuments, numberOfItemsPerPage, currentPage, items);

            return this.View(viewModel);
        }

        // GET: /Articles/Files/Upload
        [HttpGet]
        public ActionResult Upload()
        {
            return this.View();
        }

        // POST: /Articles/Files/Upload
        [HttpPost]
        public async Task<ActionResult> Upload(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null || !files.Any() || files.All(f => f == null))
            {
                throw new NoFilesSelectedException();
            }

            var userId = this.User.Identity.GetUserId();
            var articleId = this.FakeArticleId;

            var tasks = new ConcurrentQueue<Task>();
            var invalidFiles = new ConcurrentQueue<string>();
            foreach (var file in files)
            {
                try
                {
                    var task = this.UploadSingleFile(userId, articleId, file);
                    tasks.Enqueue(task);
                }
                catch (NullOrEmptyFileException)
                {
                    invalidFiles.Enqueue(file.FileName);
                }
            }

            await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

            if (invalidFiles.Count > 0)
            {
                throw new InvalidOrEmptyFilesException(invalidFiles.OrderBy(f => f).ToList());
            }

            this.Response.StatusCode = (int)HttpStatusCode.Created;
            return this.RedirectToAction(nameof(this.Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.TryDispose();
            }

            base.Dispose(disposing);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName).ExecuteResult(this.ControllerContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is NoFilesSelectedException)
            {
                filterContext.Result = this.NoFilesSelectedErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message,
                    Strings.DefaultUploadNewFileActionLinkTitle);
            }
            else if (filterContext.Exception is InvalidOrEmptyFilesException ex)
            {
                // TODO: Remove ViewBag
                this.ViewBag.InvalidFiles = ex?.FileNames;
                filterContext.Result = this.InvalidOrEmptyFilesErrorView(InstanceNames.FilesControllerInstanceName);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.FilesControllerInstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
        }

        private Task<object> UploadSingleFile(object userId, object articleId, HttpPostedFileBase file)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (file == null || file.ContentLength < 1)
            {
                throw new NullOrEmptyFileException();
            }

            var document = new Document
            {
                FileName = Path.GetFileNameWithoutExtension(file.FileName).Trim('.'),
                FileExtension = Path.GetExtension(file.FileName).Trim('.'),
                ContentLength = file.ContentLength,
                ContentType = file.ContentType
            };

            var task = this.service.CreateAsync(userId, articleId, document, file.InputStream);
            return task;
        }

        private async Task<FileDetailsViewModel> GetDetails(object userId, object articleId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var document = await this.service.GetAsync(userId, articleId, id).ConfigureAwait(false);
            if (document == null)
            {
                throw new EntityNotFoundException();
            }

            var viewModel = new FileDetailsViewModel
            {
                ArticleId = articleId.ToString(),
                DocumentId = document.Id.ToString(),
                FileName = document.FileName,
                FileExtension = document.FileExtension,
                Comment = document.Comment,
                ContentType = document.ContentType,
                ContentLength = document.ContentLength,
                DateCreated = document.DateCreated,
                DateModified = document.DateModified
            };

            return viewModel;
        }
    }
}
