// <copyright file="DocumentsController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Documents;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// /Documents/Documents
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class DocumentsController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Documents";

        /// <summary>
        /// Upload action name.
        /// </summary>
        public const string UploadActionName = nameof(Upload);

        /// <summary>
        /// Download action name.
        /// </summary>
        public const string DownloadActionName = nameof(Download);

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Edit action name.
        /// </summary>
        public const string EditActionName = nameof(Edit);

        /// <summary>
        /// Delete action name.
        /// </summary>
        public const string DeleteActionName = nameof(Delete);

        /// <summary>
        /// Details action name.
        /// </summary>
        public const string DetailsActionName = nameof(Details);

        /// <summary>
        /// SetAsFinal action name.
        /// </summary>
        public const string SetAsFinalActionName = nameof(SetAsFinal);

        /// <summary>
        /// Html action name.
        /// </summary>
        public const string HtmlActionName = nameof(Html);

        /// <summary>
        /// GetXml action name.
        /// </summary>
        public const string GetXmlActionName = nameof(GetXml);

        /// <summary>
        /// GetHtml action name.
        /// </summary>
        public const string GetHtmlActionName = nameof(GetHtml);

        /// <summary>
        /// SetXml action name.
        /// </summary>
        public const string SetXmlActionName = nameof(SetXml);

        /// <summary>
        /// SetHtml action name.
        /// </summary>
        public const string SetHtmlActionName = nameof(SetHtml);

        /// <summary>
        /// Xml action name.
        /// </summary>
        public const string XmlActionName = nameof(Xml);

        private readonly IDocumentsService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IDocumentsService"/>.</param>
        /// <param name="logger">Logger.</param>
        public DocumentsController(IDocumentsService service, ILogger<DocumentsController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Documents/Index
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index(string articleId, string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            if (!string.IsNullOrWhiteSpace(articleId))
            {
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId, returnUrl });
            }

            return this.RedirectToAction(ArticlesController.IndexActionName, ArticlesController.ControllerName, new { returnUrl });
        }

        /// <summary>
        /// GET /Documents/Documents/Upload
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(UploadActionName)]
        public async Task<IActionResult> Upload(string articleId, string returnUrl = null)
        {
            const string LogMessage = "GET Upload Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentUploadViewModelAsync(articleId).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/Upload
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(UploadActionName)]
        public async Task<IActionResult> Upload(IFormFile file, string articleId, string returnUrl = null)
        {
            const string LogMessage = "POST Upload Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var ok = await this.service.UploadDocumentAsync(file, articleId).ConfigureAwait(false);
                if (ok)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction(IndexActionName);
                }

                this.ModelState.AddModelError(string.Empty, "Document is not uploaded.");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            var viewModel = await this.service.GetDocumentUploadViewModelAsync(articleId).ConfigureAwait(false);
            viewModel.ReturnUrl = returnUrl;

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View(model: viewModel);
        }

        /// <summary>
        /// /Documents/Documents/Download/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DownloadActionName)]
        public async Task<IActionResult> Download(string id, string articleId)
        {
            const string LogMessage = "Download Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id))
            {
                string modelError = "Invalid document";
                this.logger.LogError(modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            try
            {
                var model = await this.service.DownloadDocumentAsync(id, articleId).ConfigureAwait(false);
                if (model == null || model.Stream == null)
                {
                    this.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                    return new EmptyResult();
                }

                return this.File(model.Stream, model.ContentType, model.FileName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return new EmptyResult();
        }

        /// <summary>
        /// /Documents/Documents/SetAsFinal/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(SetAsFinalActionName)]
        public async Task<IActionResult> SetAsFinal(string id, string articleId)
        {
            const string LogMessage = "Document SetAsFinal";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id))
            {
                string modelError = "Invalid document";
                this.logger.LogError(modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            try
            {
                var ok = await this.service.SetAsFinalAsync(id, articleId).ConfigureAwait(false);
                if (!ok)
                {
                    this.logger.LogError("SetAsFinal is not OK");
                }

                return this.RedirectToAction(IndexActionName, new { articleId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return new EmptyResult();
        }

        /// <summary>
        /// GET /Documents/Documents/Edit/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id, string articleId, string returnUrl = null)
        {
            const string LogMessage = "GET Edit Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentEditViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    string modelError = "Document-article mismatch";
                    this.logger.LogError(modelError);
                    this.ModelState.AddModelError(string.Empty, modelError);
                    this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/Edit
        /// </summary>
        /// <param name="model"><see cref="DocumentUpdateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(DocumentUpdateRequestModel model)
        {
            const string LogMessage = "POST Edit Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(model?.ArticleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateDocumentAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Document is not updated.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, LogMessage);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Delete/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id, string articleId, string returnUrl = null)
        {
            const string LogMessage = "GET Delete Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentDeleteViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    string modelError = "Document-article mismatch";
                    this.logger.LogError(modelError);
                    this.ModelState.AddModelError(string.Empty, modelError);
                    this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/Delete
        /// </summary>
        /// <param name="model"><see cref="DocumentDeleteRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(DocumentDeleteRequestModel model)
        {
            const string LogMessage = "POST Delete Document";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(model?.ArticleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteDocumentAsync(model.Id, model.ArticleId).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Document is not deleted.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, LogMessage);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Details/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id, string articleId, string returnUrl = null)
        {
            const string LogMessage = "Fetch Document Details";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentDetailsViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    string modelError = "Document-article mismatch";
                    this.logger.LogError(modelError);
                    this.ModelState.AddModelError(string.Empty, modelError);
                    this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Html/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(HtmlActionName)]
        public async Task<IActionResult> Html(string id, string articleId, string returnUrl = null)
        {
            const string LogMessage = "Fetch Document Html";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentHtmlViewModelAsync(id, articleId).ConfigureAwait(false);

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Xml/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(XmlActionName)]
        public async Task<IActionResult> Xml(string id, string articleId, string returnUrl = null)
        {
            const string LogMessage = "Fetch Document Xml";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(articleId))
            {
                string modelError = "Invalid article";
                this.logger.LogError(modelError);
                this.ModelState.AddModelError(string.Empty, modelError);
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentXmlViewModelAsync(id, articleId).ConfigureAwait(false);

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            this.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/GetXml/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(GetXmlActionName)]
        public async Task<IActionResult> GetXml(string id, string articleId)
        {
            const string LogMessage = "Get XML";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            try
            {
                string content = await this.service.GetXmlAsync(id, articleId).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentContentResponseModel(id, articleId, content))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// POST /Documents/Documents/GetHtml/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ActionName(GetHtmlActionName)]
        public async Task<IActionResult> GetHtml(string id, string articleId)
        {
            const string LogMessage = "Get HTML";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            try
            {
                string content = await this.service.GetHtmlAsync(id, articleId).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentContentResponseModel(id, articleId, content))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// PUT /Documents/Documents/SetXml/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// /// <param name="content">XML content.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPut]
        [ActionName(SetXmlActionName)]
        public async Task<IActionResult> SetXml(string id, string articleId, string content)
        {
            const string LogMessage = "Set XML";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            try
            {
                var result = await this.service.SetXmlAsync(id, articleId, content).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentSavedResponseModel(result))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// PUT /Documents/Documents/SetHtml/id
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="content">HTML content.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPut]
        [ActionName(SetHtmlActionName)]
        public async Task<IActionResult> SetHtml(string id, string articleId, string content)
        {
            const string LogMessage = "Set HTML";

            this.logger.LogTrace(LogMessage);

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            try
            {
                var result = await this.service.SetHtmlAsync(id, articleId, content).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentSavedResponseModel(result))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }
    }
}
