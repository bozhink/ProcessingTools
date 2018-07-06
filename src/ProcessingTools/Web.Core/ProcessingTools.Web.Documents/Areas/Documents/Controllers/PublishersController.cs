// <copyright file="PublishersController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// /Documents/Publishers
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class PublishersController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Publishers";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Create action name.
        /// </summary>
        public const string CreateActionName = nameof(Create);

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

        private readonly IPublishersService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IPublishersService"/>.</param>
        /// <param name="logger">Logger.</param>
        public PublishersController(IPublishersService service, ILogger<PublishersController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Publishers/Index
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of publishers per page.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(int? p, int? n, string returnUrl = null)
        {
            const string LogMessage = "Fetch Publishers";

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
                var viewModel = await this.service.GetPublishersIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
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
        /// GET /Documents/Publishers/Create
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            const string LogMessage = "GET Create Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetPublisherCreateViewModelAsync().ConfigureAwait(false);
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
        /// POST /Documents/Publishers/Create
        /// </summary>
        /// <param name="model"><see cref="PublisherCreateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(PublisherCreateRequestModel model)
        {
            const string LogMessage = "POST Create Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreatePublisherAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Publisher is not created.");
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
        /// GET /Documents/Publishers/Edit/id
        /// </summary>
        /// <param name="id">ID of the publisher</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Edit Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetPublisherEditViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Documents/Publishers/Edit
        /// </summary>
        /// <param name="model"><see cref="PublisherUpdateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(PublisherUpdateRequestModel model)
        {
            const string LogMessage = "POST Edit Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdatePublisherAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Publisher is not updated.");
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
        /// GET /Documents/Publishers/Delete/id
        /// </summary>
        /// <param name="id">ID of the publisher</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Delete Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetPublisherDeleteViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Documents/Publishers/Delete
        /// </summary>
        /// <param name="model"><see cref="PublisherDeleteRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(PublisherDeleteRequestModel model)
        {
            const string LogMessage = "POST Delete Publisher";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeletePublisherAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Publisher is not deleted.");
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
        /// GET /Documents/Publishers/Details/id
        /// </summary>
        /// <param name="id">ID of the publisher</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            const string LogMessage = "Fetch Publisher Details";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetPublisherDetailsViewModelAsync(id).ConfigureAwait(false);
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
