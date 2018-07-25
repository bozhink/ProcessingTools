// <copyright file="CoordinatesCalculatorController.cs" company="ProcessingTools">
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
    using ProcessingTools.Web.Models.Tools.Coordinates;
    using ProcessingTools.Web.Services.Contracts.Geo.Coordinates;

    /// <summary>
    /// CoordinatesCalculator
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class CoordinatesCalculatorController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "CoordinatesCalculator";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        private readonly ICoordinatesCalculatorWebService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesCalculatorController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="ICoordinatesCalculatorWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public CoordinatesCalculatorController(ICoordinatesCalculatorWebService service, ILogger<CoordinatesCalculatorController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET CoordinatesCalculator
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(string returnUrl)
        {
            const string LogMessage = "GET Coordinates Calculator Parse Coordinates";

            this.logger.LogTrace(LogMessage);

            var viewModel = await this.service.GetCoordinatesViewModelAsync().ConfigureAwait(false);
            viewModel.ReturnUrl = returnUrl;

            return this.View(model: viewModel);
        }

        /// <summary>
        /// POST CoordinatesCalculator
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index([Bind(nameof(CoordinatesRequestModel.Coordinates))]CoordinatesRequestModel model, string returnUrl)
        {
            const string LogMessage = "POST Coordinates Calculator Parse Coordinates";

            this.logger.LogTrace(LogMessage);

            CoordinatesViewModel viewModel;

            if (this.ModelState.IsValid)
            {
                try
                {
                    viewModel = await this.service.ParseCoordinatesAsync(model).ConfigureAwait(false);
                    viewModel.ReturnUrl = returnUrl;

                    return this.View(model: viewModel);
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    this.logger.LogError(ex, LogMessage);
                }
            }

            viewModel = await this.service.GetCoordinatesViewModelAsync().ConfigureAwait(false);
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
