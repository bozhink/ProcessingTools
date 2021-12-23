// <copyright file="MediatypesController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Files.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Web.Services.Files;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Files.Mediatypes;

    /// <summary>
    /// /Files/Mediatypes.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Files)]
    public class MediatypesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Mediatypes";

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

        private readonly IMediatypesWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IMediatypesWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public MediatypesController(IMediatypesWebService service, ILogger<MediatypesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Files/Mediatypes/Index.
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of styles per page.</param>
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
                var viewModel = await this.service.GetMediatypesIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
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
        /// GET /Files/Mediatypes/Create.
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
                var viewModel = await this.service.GetMediatypeCreateViewModelAsync().ConfigureAwait(false);
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
        /// POST /Files/Mediatypes/Create.
        /// </summary>
        /// <param name="model"><see cref="MediatypeCreateRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Create(MediatypeCreateRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreateMediatypeAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Mediatype is not created.");
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
        /// GET /Files/Mediatypes/Edit/id.
        /// </summary>
        /// <param name="id">ID of the style.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetMediatypeEditViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Files/Mediatypes/Edit.
        /// </summary>
        /// <param name="model"><see cref="MediatypeUpdateRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Edit(MediatypeUpdateRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateMediatypeAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Mediatype is not updated.");
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
        /// GET /Files/Mediatypes/Delete/id.
        /// </summary>
        /// <param name="id">ID of the style.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetMediatypeDeleteViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Files/Mediatypes/Delete.
        /// </summary>
        /// <param name="model"><see cref="MediatypeDeleteRequestModel"/>.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Delete(MediatypeDeleteRequestModel model)
        {
            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteMediatypeAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (model.ReturnUrl != null)
                            {
                                return this.Redirect(model.ReturnUrl.ToString());
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Mediatype is not deleted.");
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
        /// GET /Files/Mediatypes/Details/id.
        /// </summary>
        /// <param name="id">ID of the style.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(DetailsActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Details(string id, Uri returnUrl = null)
        {
            try
            {
                var viewModel = await this.service.GetMediatypeDetailsViewModelAsync(id).ConfigureAwait(false);
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
