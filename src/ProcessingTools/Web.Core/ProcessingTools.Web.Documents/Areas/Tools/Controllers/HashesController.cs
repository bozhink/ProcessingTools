// <copyright file="HashesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Services.Contracts.Tools;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.Hashes;

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

        private readonly IHashService hashService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashesController"/> class.
        /// </summary>
        /// <param name="hashService">Instance of <see cref="IHashService"/>.</param>
        /// <param name="logger">Logger</param>
        public HashesController(IHashService hashService, ILogger<HashesController> logger)
        {
            this.hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Hash
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            const string LogMessage = "GET Hash/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home);
        }

        /// <summary>
        /// GET Hash/All
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(AllActionName)]
        public async Task<IActionResult> All()
        {
            const string LogMessage = "GET Hash/All";

            this.logger.LogTrace(LogMessage);

            HashesViewModel viewModel = new HashesViewModel();
            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Hash/All
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(AllActionName)]
        public async Task<IActionResult> All([Bind(nameof(HashContentRequestModel.Content))]HashContentRequestModel model)
        {
            const string LogMessage = "POST Hash/All";

            this.logger.LogTrace(LogMessage);

            HashesViewModel viewModel = new HashesViewModel();

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel.Content = model.Content;

                    viewModel.MD5String = await this.hashService.GetMD5HashAsStringAsync(model.Content).ConfigureAwait(false);
                    viewModel.MD5Base64String = await this.hashService.GetMD5HashAsBase64StringAsync(model.Content).ConfigureAwait(false);

                    viewModel.SHA1String = await this.hashService.GetSHA1HashAsStringAsync(model.Content).ConfigureAwait(false);
                    viewModel.SHA1Base64String = await this.hashService.GetSHA1HashAsBase64StringAsync(model.Content).ConfigureAwait(false);

                    viewModel.SHA256String = await this.hashService.GetSHA256HashAsStringAsync(model.Content).ConfigureAwait(false);
                    viewModel.SHA256Base64String = await this.hashService.GetSHA256HashAsBase64StringAsync(model.Content).ConfigureAwait(false);

                    viewModel.SHA384String = await this.hashService.GetSHA384HashAsStringAsync(model.Content).ConfigureAwait(false);
                    viewModel.SHA384Base64String = await this.hashService.GetSHA384HashAsBase64StringAsync(model.Content).ConfigureAwait(false);

                    viewModel.SHA512String = await this.hashService.GetSHA512HashAsStringAsync(model.Content).ConfigureAwait(false);
                    viewModel.SHA512Base64String = await this.hashService.GetSHA512HashAsBase64StringAsync(model.Content).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, LogMessage);
                }
            }

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
