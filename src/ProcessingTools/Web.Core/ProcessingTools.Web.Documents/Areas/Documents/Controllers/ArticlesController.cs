// <copyright file="ArticlesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Articles;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// /Documents/Articles
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class ArticlesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Articles";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Create action name.
        /// </summary>
        public const string CreateActionName = nameof(Create);

        /// <summary>
        /// Create from file action name.
        /// </summary>
        public const string CreateFromFileActionName = nameof(CreateFromFile);

        /// <summary>
        /// Edit action name.
        /// </summary>
        public const string EditActionName = nameof(Edit);

        /// <summary>
        /// Delete action name.
        /// </summary>
        public const string DeleteActionName = nameof(Delete);

        /// <summary>
        /// Finalize action name.
        /// </summary>
        public const string FinalizeActionName = nameof(Finalize);

        /// <summary>
        /// Details action name.
        /// </summary>
        public const string DetailsActionName = nameof(Details);

        /// <summary>
        /// Documents action name.
        /// </summary>
        public const string DocumentsActionName = nameof(Documents);

        private readonly IArticlesWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IArticlesWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public ArticlesController(IArticlesWebService service, ILogger<ArticlesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Articles/Index
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of articles per page.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(int? p, int? n, string returnUrl = null)
        {
            const string LogMessage = "Fetch Articles";

            this.logger.LogTrace(LogMessage);

            int pageNumber = Math.Max(
                PaginationConstants.MinimalPageNumber,
                p ?? PaginationConstants.DefaultPageNumber);
            int numberOfItemsPerPage = Math.Max(
                PaginationConstants.MinimalItemsPerPage,
                Math.Min(
                    n ?? PaginationConstants.DefaultLargeNumberOfItemsPerPage,
                    PaginationConstants.MaximalItemsPerPageAllowed));

            try
            {
                var viewModel = await this.service.GetArticlesIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Articles/Create
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            const string LogMessage = "GET Create Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleCreateViewModelAsync().ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/Create
        /// </summary>
        /// <param name="model"><see cref="ArticleCreateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(ArticleCreateRequestModel model)
        {
            const string LogMessage = "POST Create Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreateArticleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Article is not created.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, LogMessage);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Articles/CreateFromFile
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(CreateFromFileActionName)]
        public async Task<IActionResult> CreateFromFile(string returnUrl = null)
        {
            const string LogMessage = "GET CreateFromFile Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleCreateFromFileViewModelAsync().ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/CreateFromFile
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <param name="journalId">Journal ID of the article.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateFromFileActionName)]
        public async Task<IActionResult> CreateFromFile(IFormFile file, string journalId, string returnUrl = null)
        {
            const string LogMessage = "POST CreateFromFile Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var ok = await this.service.CreateFromFileArticleAsync(file, journalId).ConfigureAwait(false);
                if (ok)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction(IndexActionName);
                }

                this.ModelState.AddModelError(string.Empty, "Article is not uploaded.");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            var viewModel = await this.service.GetArticleCreateFromFileViewModelAsync().ConfigureAwait(false);
            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// GET /Documents/Articles/Edit/id
        /// </summary>
        /// <param name="id">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Edit Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleEditViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/Edit
        /// </summary>
        /// <param name="model"><see cref="ArticleUpdateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(ArticleUpdateRequestModel model)
        {
            const string LogMessage = "POST Edit Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateArticleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Article is not updated.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, LogMessage);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Articles/Delete/id
        /// </summary>
        /// <param name="id">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Delete Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleDeleteViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Articles/Delete
        /// </summary>
        /// <param name="model"><see cref="ArticleDeleteRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(ArticleDeleteRequestModel model)
        {
            const string LogMessage = "POST Delete Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteArticleAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Article is not deleted.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, LogMessage);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// /Documents/Articles/Finalize/id
        /// </summary>
        /// <param name="id">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(FinalizeActionName)]
        public async Task<IActionResult> Finalize(string id, string returnUrl = null)
        {
            const string LogMessage = "Finalize Article";

            this.logger.LogTrace(LogMessage);

            try
            {
                var ok = await this.service.FinalizeArticleAsync(id).ConfigureAwait(false);
                if (ok)
                {
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }

                    return this.RedirectToAction(IndexActionName);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LogMessage);
            }

            return this.RedirectToAction(IndexActionName);
        }

        /// <summary>
        /// GET /Documents/Articles/Details/id
        /// </summary>
        /// <param name="id">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            const string LogMessage = "Fetch Article Details";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleDetailsViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Articles/Documents/id
        /// </summary>
        /// <param name="id">ID of the article</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DocumentsActionName)]
        public async Task<IActionResult> Documents(string id, string returnUrl = null)
        {
            const string LogMessage = "Fetch Article Documents";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetArticleDocumentsViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, LogMessage);
            }

            return this.View();
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
