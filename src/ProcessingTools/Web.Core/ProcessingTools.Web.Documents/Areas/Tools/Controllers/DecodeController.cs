// <copyright file="DecodeController.cs" company="ProcessingTools">
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
    using ProcessingTools.Web.Models.Tools.Decode;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Decode controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class DecodeController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Decode";

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

        private readonly IDecodeWebService decodeWebService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeController"/> class.
        /// </summary>
        /// <param name="decodeWebService">Decoding service</param>
        /// <param name="logger">Logger</param>
        public DecodeController(IDecodeWebService decodeWebService, ILogger<DecodeController> logger)
        {
            this.decodeWebService = decodeWebService ?? throw new ArgumentNullException(nameof(decodeWebService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Decode
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl)
        {
            const string LogMessage = "GET Decode/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// GET Decode/Base64
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64ActionName)]
        public async Task<IActionResult> Base64(string returnUrl)
        {
            const string LogMessage = "GET Decode/Base64";

            this.logger.LogTrace(LogMessage);

            DecodeBase64ViewModel viewModel = await this.decodeWebService.GetDecodeBase64ViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Decode/Base64
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64ActionName)]
        public async Task<IActionResult> Base64([Bind(nameof(DecodeBase64RequestModel.Content))]DecodeBase64RequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Decode/Base64";

            this.logger.LogTrace(LogMessage);

            DecodeBase64ViewModel viewModel = null;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.decodeWebService.DecodeBase64Async(model).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = viewModel ?? await this.decodeWebService.GetDecodeBase64ViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// GET Decode/Base64Url
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64UrlActionName)]
        public async Task<IActionResult> Base64Url(string returnUrl)
        {
            const string LogMessage = "GET Decode/Base64Url";

            this.logger.LogTrace(LogMessage);

            DecodeBase64UrlViewModel viewModel = await this.decodeWebService.GetBase64UrlViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Decode/Base64Url
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64UrlActionName)]
        public async Task<IActionResult> Base64Url([Bind(nameof(DecodeBase64UrlRequestModel.Content))]DecodeBase64UrlRequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Decode/Base64Url";

            this.logger.LogTrace(LogMessage);

            DecodeBase64UrlViewModel viewModel = null;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.decodeWebService.DecodeBase64UrlAsync(model).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = viewModel ?? await this.decodeWebService.GetBase64UrlViewModelAsync().ConfigureAwait(false);

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
