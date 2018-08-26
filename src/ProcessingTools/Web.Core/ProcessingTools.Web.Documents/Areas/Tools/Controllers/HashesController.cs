// <copyright file="HashesController.cs" company="ProcessingTools">
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

        private readonly Encoding encoding;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashesController"/> class.
        /// </summary>
        /// <param name="encoding">Character encoding</param>
        /// <param name="logger">Logger</param>
        public HashesController(Encoding encoding, ILogger<HashesController> logger)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
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
        public IActionResult All()
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
        public IActionResult All([Bind(nameof(HashContentRequestModel.Content))]HashContentRequestModel model)
        {
            const string LogMessage = "POST Hash/All";

            this.logger.LogTrace(LogMessage);

            HashesViewModel viewModel = new HashesViewModel();

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel.Content = model.Content;

                    viewModel.MD5String = ProcessingTools.Security.Utils.GetMD5HashAsString(model.Content, this.encoding);
                    viewModel.MD5Base64String = ProcessingTools.Security.Utils.GetMD5HashAsBase64String(model.Content, this.encoding);

                    viewModel.SHA1String = ProcessingTools.Security.Utils.GetSHA1HashAsString(model.Content, this.encoding);
                    viewModel.SHA1Base64String = ProcessingTools.Security.Utils.GetSHA1HashAsBase64String(model.Content, this.encoding);

                    viewModel.SHA256String = ProcessingTools.Security.Utils.GetSHA256HashAsString(model.Content, this.encoding);
                    viewModel.SHA256Base64String = ProcessingTools.Security.Utils.GetSHA256HashAsBase64String(model.Content, this.encoding);

                    viewModel.SHA384String = ProcessingTools.Security.Utils.GetSHA384HashAsString(model.Content, this.encoding);
                    viewModel.SHA384Base64String = ProcessingTools.Security.Utils.GetSHA384HashAsBase64String(model.Content, this.encoding);

                    viewModel.SHA512String = ProcessingTools.Security.Utils.GetSHA512HashAsString(model.Content, this.encoding);
                    viewModel.SHA512Base64String = ProcessingTools.Security.Utils.GetSHA512HashAsBase64String(model.Content, this.encoding);
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
