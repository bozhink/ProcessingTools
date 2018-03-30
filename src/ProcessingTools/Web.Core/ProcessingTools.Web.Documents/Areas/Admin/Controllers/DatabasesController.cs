// <copyright file="DatabasesController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Services.Contracts.Admin;

    /// <summary>
    /// /Admin/Databases
    /// </summary>
    [Authorize]
    [Area(AreaNames.Admin)]
    public class DatabasesController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "Databases";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// InitializeAll action name.
        /// </summary>
        public const string InitializeAllActionName = nameof(InitializeAll);

        private readonly IDatabasesService databasesService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesController"/> class.
        /// </summary>
        /// <param name="databasesService">Instance of <see cref="IDatabasesService"/>.</param>
        /// <param name="logger">Logger.</param>
        public DatabasesController(IDatabasesService databasesService, ILogger<DatabasesController> logger)
        {
            this.databasesService = databasesService ?? throw new ArgumentNullException(nameof(databasesService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Admin/Databases/Index
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index(string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.View();
        }

        /// <summary>
        /// /Admin/Databases/InitializeAll
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(InitializeAllActionName)]
        public async Task<IActionResult> InitializeAll(string returnUrl = null)
        {
            try
            {
                var response = await this.databasesService.InitializeAllAsync().ConfigureAwait(false);

                var viewModel = await this.databasesService.MapToViewModelAsync(response).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                if (viewModel.Exceptions?.Any() == true)
                {
                    this.logger.LogError(new AggregateException(viewModel.Exceptions), "Database initialization exceptions.");
                }

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, "Error in database initialization.");
            }

            return this.View();
        }
    }
}
