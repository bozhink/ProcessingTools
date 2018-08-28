// <copyright file="EncodeController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
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

        private readonly Encoding encoding;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeController"/> class.
        /// </summary>
        /// <param name="encoding">Character encoding</param>
        /// <param name="logger">Logger</param>
        public EncodeController(Encoding encoding, ILogger<EncodeController> logger)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Encode
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            const string LogMessage = "GET Encode/Index";

            this.logger.LogTrace(LogMessage);

            return this.RedirectToAction(ActionNames.Index, ControllerNames.Home);
        }

        /// <summary>
        /// GET Encode/Base64
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64ActionName)]
        public IActionResult Base64()
        {
            const string LogMessage = "GET Encode/Base64";

            this.logger.LogTrace(LogMessage);

            EncodeBase64ViewModel viewModel = new EncodeBase64ViewModel();
            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64ActionName)]
        public IActionResult Base64([Bind(nameof(EncodeBase64RequestModel.Content))]EncodeBase64RequestModel model)
        {
            const string LogMessage = "POST Encode/Base64";

            this.logger.LogTrace(LogMessage);

            EncodeBase64ViewModel viewModel = new EncodeBase64ViewModel();

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel.Content = model.Content;

                    byte[] bytes = this.encoding.GetBytes(model.Content);
                    viewModel.Base64EncodedString = Convert.ToBase64String(bytes);
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
        /// GET Encode/Base64Url
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(Base64UrlActionName)]
        public IActionResult Base64Url()
        {
            const string LogMessage = "GET Encode/Base64Url";

            this.logger.LogTrace(LogMessage);

            EncodeBase64UrlViewModel viewModel = new EncodeBase64UrlViewModel();
            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST Encode/Base64Url
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(Base64UrlActionName)]
        public IActionResult Base64Url([Bind(nameof(EncodeBase64UrlRequestModel.Content))]EncodeBase64UrlRequestModel model)
        {
            const string LogMessage = "POST Encode/Base64Url";

            this.logger.LogTrace(LogMessage);

            EncodeBase64UrlViewModel viewModel = new EncodeBase64UrlViewModel();

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel.Content = model.Content;

                    byte[] bytes = this.encoding.GetBytes(model.Content);
                    viewModel.Base64EncodedString = ProcessingTools.Security.Utils.ToBase64Url(bytes);
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
