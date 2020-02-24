// <copyright file="EncodeController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Tools;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.Encode;

    /// <summary>
    /// Encode controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class EncodeController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Encode";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Base64 action name.
        /// </summary>
        public const string Base64ActionName = nameof(Base64);

        /// <summary>
        /// Base64Url action name.
        /// </summary>
        public const string Base64UrlActionName = nameof(Base64Url);

        private readonly IEncodeWebService encodeWebService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeController"/> class.
        /// </summary>
        /// <param name="encodeWebService">Instance of <see cref="IEncodeWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public EncodeController(IEncodeWebService encodeWebService, ILogger<EncodeController> logger)
        {
            this.encodeWebService = encodeWebService ?? throw new ArgumentNullException(nameof(encodeWebService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Encode.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public IActionResult Index(Uri returnUrl)
        {
            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// GET Encode/Base64.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(Base64ActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Base64(Uri returnUrl)
        {
            EncodeBase64ViewModel viewModel = await this.encodeWebService.GetEncodeBase64ViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64ActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Base64([Bind(nameof(EncodeBase64RequestModel.Content))]EncodeBase64RequestModel model, Uri returnUrl)
        {
            EncodeBase64ViewModel viewModel = null;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.encodeWebService.EncodeBase64Async(model).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, string.Empty);
                }
            }

            viewModel ??= await this.encodeWebService.MapToViewModelAsync(model).ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// GET Encode/Base64Url.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(Base64UrlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Base64Url(Uri returnUrl)
        {
            EncodeBase64UrlViewModel viewModel = await this.encodeWebService.GetEncodeBase64UrlViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64Url.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64UrlActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> Base64Url([Bind(nameof(EncodeBase64UrlRequestModel.Content))]EncodeBase64UrlRequestModel model, Uri returnUrl)
        {
            EncodeBase64UrlViewModel viewModel = null;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.encodeWebService.EncodeBase64UrlAsync(model).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, string.Empty);
                }
            }

            viewModel ??= await this.encodeWebService.MapToViewModelAsync(model).ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
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
