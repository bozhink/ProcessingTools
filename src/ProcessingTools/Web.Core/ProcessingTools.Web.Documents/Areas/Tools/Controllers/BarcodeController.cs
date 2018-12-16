// <copyright file="BarcodeController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Processors.Imaging.Contracts;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.Barcode;

    /// <summary>
    /// Barcode
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class BarcodeController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Barcode";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        private readonly IBarcodeEncoder encoder;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeController"/> class.
        /// </summary>
        /// <param name="encoder">Instance of <see cref="IBarcodeEncoder"/>.</param>
        /// <param name="logger">Logger.</param>
        public BarcodeController(IBarcodeEncoder encoder, ILogger<BarcodeController> logger)
        {
            this.encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Index
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            var viewModel = new BarcodeViewModel
            {
                Width = ImagingConstants.DefaultBarcodeWidth,
                Height = ImagingConstants.DefaultBarcodeHeight
            };

            return this.View(viewModel);
        }

        /// <summary>
        /// POST Index
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(IndexActionName)]
        public async Task<ActionResult> Index([Bind(nameof(BarcodeRequestModel.Width), nameof(BarcodeRequestModel.Height), nameof(BarcodeRequestModel.Type), nameof(BarcodeRequestModel.Content))]BarcodeRequestModel model)
        {
            var viewModel = new BarcodeViewModel(model.Type)
            {
                Content = model.Content,
                Width = model.Width,
                Height = model.Height
            };

            try
            {
                if (this.ModelState.IsValid)
                {
                    viewModel.Image = await this.encoder.EncodeBase64Async((BarcodeType)model.Type, model.Content, model.Width, model.Height).ConfigureAwait(false);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(model.Content), e.Message);
                this.logger.LogError(e, "POST QRCode/Index");
            }

            return this.View(viewModel);
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
