// <copyright file="ReferenceTagStylesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Layout.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Layout.Styles.References;
    using ProcessingTools.Web.Services.Contracts.Layout.Styles;

    /// <summary>
    /// /Layout/ReferenceTagStyles
    /// </summary>
    [Authorize]
    [Area(AreaNames.Layout)]
    public class ReferenceTagStylesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "ReferenceTagStyles";

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

        private readonly IReferenceTagStylesWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IReferenceTagStylesWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public ReferenceTagStylesController(IReferenceTagStylesWebService service, ILogger<ReferenceTagStylesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Layout/ReferenceTagStyles/Index
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of styles per page.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(int? p, int? n, string returnUrl = null)
        {
            const string LogMessage = "Fetch Reference Tag Styles";

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
                var viewModel = await this.service.GetReferenceTagStylesIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
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
        /// GET /Layout/ReferenceTagStyles/Create
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            const string LogMessage = "GET Create Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceTagStyleCreateViewModelAsync().ConfigureAwait(false);
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
        /// POST /Layout/ReferenceTagStyles/Create
        /// </summary>
        /// <param name="model"><see cref="ReferenceTagStyleCreateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(ReferenceTagStyleCreateRequestModel model)
        {
            const string LogMessage = "POST Create Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreateReferenceTagStyleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Tag Style is not created.");
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
        /// GET /Layout/ReferenceTagStyles/Edit/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Edit Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceTagStyleEditViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Layout/ReferenceTagStyles/Edit
        /// </summary>
        /// <param name="model"><see cref="ReferenceTagStyleUpdateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(ReferenceTagStyleUpdateRequestModel model)
        {
            const string LogMessage = "POST Edit Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateReferenceTagStyleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Tag Style is not updated.");
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
        /// GET /Layout/ReferenceTagStyles/Delete/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Delete Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceTagStyleDeleteViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Layout/ReferenceTagStyles/Delete
        /// </summary>
        /// <param name="model"><see cref="ReferenceTagStyleDeleteRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(ReferenceTagStyleDeleteRequestModel model)
        {
            const string LogMessage = "POST Delete Reference Tag Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteReferenceTagStyleAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Tag Style is not deleted.");
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
        /// GET /Layout/ReferenceTagStyles/Details/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            const string LogMessage = "Fetch Reference Tag Style Details";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceTagStyleDetailsViewModelAsync(id).ConfigureAwait(false);
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
