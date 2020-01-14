// <copyright file="BarcodeController.cs" company="ProcessingTools">
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
    using ProcessingTools.Web.Models.Tools.Barcode;

    /// <summary>
    /// Barcode.
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

        private readonly IBarcodeWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IBarcodeWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public BarcodeController(IBarcodeWebService service, ILogger<BarcodeController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET Index.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.service.GetBarcodeViewModelAsync().ConfigureAwait(false);

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
        public async Task<ActionResult> Index([Bind(nameof(BarcodeRequestModel.Width), nameof(BarcodeRequestModel.Height), nameof(BarcodeRequestModel.Type), nameof(BarcodeRequestModel.Content))]BarcodeRequestModel model)
        {
            var viewModel = await this.service.MapToViewModel(model).ConfigureAwait(false);
            try
            {
                if (this.ModelState.IsValid)
                {
                    viewModel.Image = await this.service.EncodeAsync(model).ConfigureAwait(false);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(model.Content), e.Message);
                this.logger.LogError(e, "POST Barcode/Index");
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
