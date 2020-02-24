// <copyright file="JournalsController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Web.Services.Documents;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Journals;

    /// <summary>
    /// /Documents/Journals.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class JournalsController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Journals";

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

        private readonly IJournalsWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IJournalsWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public JournalsController(IJournalsWebService service, ILogger<JournalsController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Journals/Index.
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of journals per page.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Index(int? p, int? n, Uri returnUrl = null)
        {
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
                var viewModel = await this.service.GetJournalsIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Journals/Create.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(CreateActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Create(Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetJournalCreateViewModelAsync().ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Journals/Create.
        /// </summary>
        /// <param name="model"><see cref="JournalCreateRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Create(JournalCreateRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreateJournalAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Journal is not created.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, string.Empty);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model?.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Journals/Edit/id.
        /// </summary>
        /// <param name="id">ID of the journal.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetJournalEditViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Journals/Edit.
        /// </summary>
        /// <param name="model"><see cref="JournalUpdateRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(JournalUpdateRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateJournalAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Journal is not updated.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, string.Empty);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model?.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Journals/Delete/id.
        /// </summary>
        /// <param name="id">ID of the journal.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetJournalDeleteViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// POST /Documents/Journals/Delete.
        /// </summary>
        /// <param name="model"><see cref="JournalDeleteRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(JournalDeleteRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteJournalAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Journal is not deleted.");
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.logger.LogError(ex, string.Empty);
                    }
                }

                var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
                viewModel.ReturnUrl = model?.ReturnUrl;

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
        }

        /// <summary>
        /// GET /Documents/Journals/Details/id.
        /// </summary>
        /// <param name="id">ID of the journal.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(DetailsActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Details(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetJournalDetailsViewModelAsync(id).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                return this.View(model: viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
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
