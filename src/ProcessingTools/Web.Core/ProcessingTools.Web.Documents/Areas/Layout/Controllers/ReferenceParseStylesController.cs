// <copyright file="ReferenceParseStylesController.cs" company="ProcessingTools">
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
    /// /Layout/ReferenceParseStyles
    /// </summary>
    [Authorize]
    [Area(AreaNames.Layout)]
    public class ReferenceParseStylesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "ReferenceParseStyles";

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

        private readonly IReferenceParseStylesWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IReferenceParseStylesWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public ReferenceParseStylesController(IReferenceParseStylesWebService service, ILogger<ReferenceParseStylesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Layout/ReferenceParseStyles/Index
        /// </summary>
        /// <param name="p">Current page number. Zero-based.</param>
        /// <param name="n">Number of styles per page.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(int? p, int? n, string returnUrl = null)
        {
            const string LogMessage = "Fetch Reference Parse Styles";

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
                var viewModel = await this.service.GetReferenceParseStylesIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);
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
        /// GET /Layout/ReferenceParseStyles/Create
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            const string LogMessage = "GET Create Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceParseStyleCreateViewModelAsync().ConfigureAwait(false);
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
        /// POST /Layout/ReferenceParseStyles/Create
        /// </summary>
        /// <param name="model"><see cref="ReferenceParseStyleCreateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(ReferenceParseStyleCreateRequestModel model)
        {
            const string LogMessage = "POST Create Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.CreateReferenceParseStyleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Parse Style is not created.");
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
        /// GET /Layout/ReferenceParseStyles/Edit/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Edit Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceParseStyleEditViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Layout/ReferenceParseStyles/Edit
        /// </summary>
        /// <param name="model"><see cref="ReferenceParseStyleUpdateRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(ReferenceParseStyleUpdateRequestModel model)
        {
            const string LogMessage = "POST Edit Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.UpdateReferenceParseStyleAsync(model).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(EditActionName, new { model.Id });
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Parse Style is not updated.");
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
        /// GET /Layout/ReferenceParseStyles/Delete/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id, string returnUrl = null)
        {
            const string LogMessage = "GET Delete Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceParseStyleDeleteViewModelAsync(id).ConfigureAwait(false);
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
        /// POST /Layout/ReferenceParseStyles/Delete
        /// </summary>
        /// <param name="model"><see cref="ReferenceParseStyleDeleteRequestModel"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(ReferenceParseStyleDeleteRequestModel model)
        {
            const string LogMessage = "POST Delete Reference Parse Style";

            this.logger.LogTrace(LogMessage);

            try
            {
                if (this.ModelState.IsValid)
                {
                    try
                    {
                        var ok = await this.service.DeleteReferenceParseStyleAsync(model.Id).ConfigureAwait(false);
                        if (ok)
                        {
                            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                            {
                                return this.Redirect(model.ReturnUrl);
                            }

                            return this.RedirectToAction(IndexActionName);
                        }

                        this.ModelState.AddModelError(string.Empty, "Reference Parse Style is not deleted.");
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
        /// GET /Layout/ReferenceParseStyles/Details/id
        /// </summary>
        /// <param name="id">ID of the style</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            const string LogMessage = "Fetch Reference Parse Style Details";

            this.logger.LogTrace(LogMessage);

            try
            {
                var viewModel = await this.service.GetReferenceParseStyleDetailsViewModelAsync(id).ConfigureAwait(false);
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
