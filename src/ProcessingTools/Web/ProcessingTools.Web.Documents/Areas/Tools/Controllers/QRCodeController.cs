// <copyright file="QRCodeController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Imaging;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.QRCode;

    /// <summary>
    /// QRCode.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class QRCodeController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "QRCode";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        private readonly IQRCodeEncoder encoder;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QRCodeController"/> class.
        /// </summary>
        /// <param name="encoder">Instance of <see cref="IQRCodeEncoder"/>.</param>
        /// <param name="logger">Logger.</param>
        public QRCodeController(IQRCodeEncoder encoder, ILogger<QRCodeController> logger)
        {
            this.encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Index.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public IActionResult Index()
        {
            var viewModel = new QRCodeViewModel
            {
                PixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule,
            };

            return this.View(viewModel);
        }

        /// <summary>
        /// POST Index.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<ActionResult> Index([Bind(nameof(QRCodeRequestModel.PixelPerModule), nameof(QRCodeRequestModel.Content))]QRCodeRequestModel model)
        {
            var viewModel = new QRCodeViewModel
            {
                Content = model?.Content ?? string.Empty,
                PixelPerModule = model?.PixelPerModule ?? ImagingConstants.MinimalQRCodePixelsPerModule,
            };

            try
            {
                if (model != null && this.ModelState.IsValid)
                {
                    viewModel.Image = await this.encoder.EncodeBase64Async(model.Content, viewModel.PixelPerModule).ConfigureAwait(false);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(model.Content), e.Message);
                this.logger.LogError(e, string.Empty);
            }

            return this.View(viewModel);
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
