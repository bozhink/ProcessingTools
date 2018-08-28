// <copyright file="EncodeController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.Encode;
    using ProcessingTools.Web.Services.Contracts.Tools;

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
        /// <param name="logger">Logger</param>
        public EncodeController(IEncodeWebService encodeWebService, ILogger<EncodeController> logger)
        {
            this.encodeWebService = encodeWebService ?? throw new ArgumentNullException(nameof(encodeWebService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Encode
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl)
        {
            const string LogMessage = "GET Encode/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// GET Encode/Base64
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64ActionName)]
        public async Task<IActionResult> Base64(string returnUrl)
        {
            const string LogMessage = "GET Encode/Base64";

            this.logger.LogTrace(LogMessage);

            this.logger.LogTrace(LogMessage);

            EncodeBase64ViewModel viewModel = await this.encodeWebService.GetEncodeBase64ViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64ActionName)]
        public async Task<IActionResult> Base64([Bind(nameof(EncodeBase64RequestModel.Content))]EncodeBase64RequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Encode/Base64";

            this.logger.LogTrace(LogMessage);

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
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = viewModel ?? await this.encodeWebService.MapToViewModelAsync(model).ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// GET Encode/Base64Url
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64UrlActionName)]
        public async Task<IActionResult> Base64Url(string returnUrl)
        {
            const string LogMessage = "GET Encode/Base64Url";

            this.logger.LogTrace(LogMessage);

            EncodeBase64UrlViewModel viewModel = await this.encodeWebService.GetEncodeBase64UrlViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64Url
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64UrlActionName)]
        public async Task<IActionResult> Base64Url([Bind(nameof(EncodeBase64UrlRequestModel.Content))]EncodeBase64UrlRequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Encode/Base64Url";

            this.logger.LogTrace(LogMessage);

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
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = viewModel ?? await this.encodeWebService.MapToViewModelAsync(model).ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
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
