// <copyright file="HashesController.cs" company="ProcessingTools">
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
    using ProcessingTools.Web.Models.Tools.Hashes;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Hashes controller.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class HashesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Hashes";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// All action name.
        /// </summary>
        public const string AllActionName = nameof(All);

        private readonly IHashesWebService hashesWebService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashesController"/> class.
        /// </summary>
        /// <param name="hashesWebService">Instance of <see cref="IHashesWebService"/>.</param>
        /// <param name="logger">Logger</param>
        public HashesController(IHashesWebService hashesWebService, ILogger<HashesController> logger)
        {
            this.hashesWebService = hashesWebService ?? throw new ArgumentNullException(nameof(hashesWebService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Hash
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl)
        {
            const string LogMessage = "GET Hash/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home, new { returnUrl });
        }

        /// <summary>
        /// GET Hash/All
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(AllActionName)]
        public async Task<IActionResult> All(string returnUrl)
        {
            const string LogMessage = "GET Hash/All";

            this.logger.LogTrace(LogMessage);

            HashesViewModel viewModel = await this.hashesWebService.GetHashesViewModelAsync().ConfigureAwait(false);

            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Hash/All
        /// </summary>
        /// <param name="model">Request model.</param>
        /// /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(AllActionName)]
        public async Task<IActionResult> All([Bind(nameof(HashContentRequestModel.Content))]HashContentRequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Hash/All";

            this.logger.LogTrace(LogMessage);

            HashesViewModel viewModel = null;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.hashesWebService.HashAsync(model).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = viewModel ?? await this.hashesWebService.MapToViewModelAsync(model).ConfigureAwait(false);

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
