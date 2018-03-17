// <copyright file="PublishersController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Constants;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// Publishers
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class PublishersController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Publishers";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Create action name.
        /// </summary>
        public const string CreateActionName = nameof(Create);

        /// <summary>
        /// Edit action name.
        /// </summary>
        public const string EditActionName = nameof(Edit);

        /// <summary>
        /// Delete action name.
        /// </summary>
        public const string DeleteActionName = nameof(Delete);

        /// <summary>
        /// Details action name.
        /// </summary>
        public const string DetailsActionName = nameof(Details);

        private readonly IPublishersService service;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersController"/> class.
        /// </summary>
        /// <param name="service">Instance of <see cref="IPublishersService"/>.</param>
        /// <param name="logger">Logger.</param>
        public PublishersController(IPublishersService service, ILogger<PublishersController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [ActionName(IndexActionName)]
        public async Task<IActionResult> Index(int? p, int? n)
        {
            this.logger.LogTrace("Index?p={0}&n={1}", p, n);

            int pageNumber = Math.Max(
                PaginationConstants.MinimalPageNumber,
                p ?? PaginationConstants.DefaultPageNumber);
            int numberOfItemsPerPage = Math.Max(
                PaginationConstants.MinimalItemsPerPage,
                Math.Min(
                    n ?? PaginationConstants.DefaultLargeNumberOfItemsPerPage,
                    PaginationConstants.MaximalItemsPerPageAllowed));

            try
            {
                var model = await this.service.GetPublishersIndexViewModelAsync(pageNumber * numberOfItemsPerPage, numberOfItemsPerPage).ConfigureAwait(false);

                return this.View(model: model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"{AreaNames.Documents}/{ControllerName}/{IndexActionName}?p={p}&n={n}");
            }

            return this.View();
        }

        [HttpGet]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create()
        {
            this.logger.LogTrace("GET Create Publisher");

            try
            {
                var model = await this.service.GetPublisherCreateViewModelAsync().ConfigureAwait(false);
                return this.View(model: model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"{AreaNames.Documents}/{ControllerName}/{CreateActionName}");
            }

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(CreateActionName)]
        public async Task<IActionResult> Create(PublisherCreateRequestModel model, string returnUrl)
        {
            this.logger.LogTrace("POST Create Publisher");

            try
            {
                if (this.ModelState.IsValid)
                {
                    var ok = await this.service.CreatePublisherAsync(model).ConfigureAwait(false);
                    if (ok)
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return this.Redirect(returnUrl);
                        }

                        return this.RedirectToAction(IndexActionName);
                    }

                    this.ModelState.AddModelError(string.Empty, "Publisher is not created.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "POST Create publisher");
            }

            var viewModel = await this.service.MapToViewModelAsync(model).ConfigureAwait(false);
            return this.View(viewModel);
        }

        [HttpGet]
        [ActionName(EditActionName)]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = await this.service.GetPublisherEditViewModelAsync(id).ConfigureAwait(false);
                return this.View(model: model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"{AreaNames.Documents}/{ControllerName}/{EditActionName}");
            }

            return this.View();
        }

        [HttpGet]
        [ActionName(DeleteActionName)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var model = await this.service.GetPublisherDeleteViewModelAsync(id).ConfigureAwait(false);
                return this.View(model: model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"{AreaNames.Documents}/{ControllerName}/{DeleteActionName}");
            }

            return this.View();
        }

        [HttpGet]
        [ActionName(DetailsActionName)]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var model = await this.service.GetPublisherDetailsViewModelAsync(id).ConfigureAwait(false);
                return this.View(model: model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"{AreaNames.Documents}/{ControllerName}/{DetailsActionName}");
            }

            return this.View();
        }
    }
}
