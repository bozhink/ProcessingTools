// <copyright file="DocumentsController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Contracts.Web.Services.Documents;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Documents;

    /// <summary>
    /// /Documents/Documents.
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

        private readonly IDocumentsWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IDocumentsWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public DocumentsController(IDocumentsWebService service, ILogger<DocumentsController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Documents/Index.
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public IActionResult Index(string articleId, Uri returnUrl)
        {
            if (returnUrl != null)
            {
                return this.Redirect(returnUrl.ToString());
            }

            if (!string.IsNullOrWhiteSpace(articleId))
            {
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId, returnUrl });
            }

            return this.RedirectToAction(ArticlesController.IndexActionName, ArticlesController.ControllerName, new { returnUrl });
        }

        /// <summary>
        /// GET /Documents/Documents/Upload.
        /// </summary>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(UploadActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Upload(string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/Upload.
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(UploadActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Upload(IFormFile file, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var ok = await this.service.UploadDocumentAsync(file, articleId).ConfigureAwait(false);
                if (ok)
                {
                    if (returnUrl != null)
                    {
                        return this.Redirect(returnUrl.ToString());
                    }

                    return this.RedirectToAction(IndexActionName);
                }

                this.ModelState.AddModelError(string.Empty, "Document is not uploaded.");
                this.logger.LogError($"{this.ModelState}");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            var viewModel = await this.service.GetDocumentUploadViewModelAsync(articleId).ConfigureAwait(false);
            viewModel.ReturnUrl = returnUrl;

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View(model: viewModel);
        }

        /// <summary>
        /// /Documents/Documents/Download/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(DownloadActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Download(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            try
            {
                var model = await this.service.DownloadDocumentAsync(id, articleId).ConfigureAwait(false);
                if (model is null || model.Stream is null)
                {
                    this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return new EmptyResult();
                }

                return this.File(model.Stream, model.ContentType, model.FileName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new EmptyResult();
        }

        /// <summary>
        /// /Documents/Documents/SetAsFinal/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(SetAsFinalActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> SetAsFinal(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            try
            {
                var ok = await this.service.SetAsFinalAsync(id, articleId).ConfigureAwait(false);
                if (!ok)
                {
                    this.logger.LogError($"{articleId} - {ok}");
                }

                return this.RedirectToAction(IndexActionName, new { articleId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new EmptyResult();
        }

        /// <summary>
        /// GET /Documents/Documents/Edit/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(string id, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentEditViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    this.ModelState.AddModelError(string.Empty, "Document-article mismatch");
                    this.logger.LogError($"{this.ModelState}");
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/Edit.
        /// </summary>
        /// <param name="model"><see cref="DocumentUpdateRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(DocumentUpdateRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.ArticleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id, model.ArticleId });
                        }

                        this.ModelState.AddModelError(string.Empty, "Document is not updated.");
                        this.logger.LogError($"{this.ModelState}");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, string.Empty);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Delete/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(string id, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentDeleteViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    this.ModelState.AddModelError(string.Empty, "Document-article mismatch");
                    this.logger.LogError($"{this.ModelState}");
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/Delete.
        /// </summary>
        /// <param name="model"><see cref="DocumentDeleteRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(DocumentDeleteRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.ArticleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Document is not deleted.");
                        this.logger.LogError($"{this.ModelState}");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, string.Empty);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Details/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(DetailsActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Details(string id, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            try
            {
                var viewModel = await this.service.GetDocumentDetailsViewModelAsync(id, articleId).ConfigureAwait(false);
                if (viewModel.ArticleId != articleId)
                {
                    this.ModelState.AddModelError(string.Empty, "Document-article mismatch");
                    this.logger.LogError($"{this.ModelState}");
                    this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.View();
                }

                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Html/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(HtmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Html(string id, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// GET /Documents/Documents/Xml/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(XmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Xml(string id, string articleId, Uri returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid article");
                this.logger.LogError($"{this.ModelState}");
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
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
                this.logger.LogError(ex, string.Empty);
            }

            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }

        /// <summary>
        /// POST /Documents/Documents/GetXml/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ActionName(GetXmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> GetXml(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            try
            {
                string content = await this.service.GetXmlAsync(id, articleId).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentContentResponseModel(id, articleId, content))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// POST /Documents/Documents/GetHtml/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ActionName(GetHtmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> GetHtml(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            try
            {
                string content = await this.service.GetHtmlAsync(id, articleId).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentContentResponseModel(id, articleId, content))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// PUT /Documents/Documents/SetXml/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="model">Content model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPut]
        [ActionName(SetXmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> SetXml(string id, string articleId, [FromBody]DocumentContentRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId) || string.IsNullOrWhiteSpace(model?.Content))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            try
            {
                var result = await this.service.SetXmlAsync(id, articleId, model.Content).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentSavedResponseModel(result))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// PUT /Documents/Documents/SetHtml/id.
        /// </summary>
        /// <param name="id">Object ID of the document.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <param name="model">Content model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPut]
        [ActionName(SetHtmlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> SetHtml(string id, string articleId, [FromBody]DocumentContentRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(articleId) || string.IsNullOrWhiteSpace(model?.Content))
            {
                return new JsonResult(this.service.GetBadRequestResponseModel(id, articleId))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            try
            {
                var result = await this.service.SetHtmlAsync(id, articleId, model.Content).ConfigureAwait(false);

                return new JsonResult(this.service.GetDocumentSavedResponseModel(result))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);

                return new JsonResult(this.service.GetInternalServerErrorResponseModel(ex))
                {
                    ContentType = ContentTypes.Json,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// Help.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }
    }
}
