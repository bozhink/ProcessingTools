// <copyright file="DatabasesController.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Admin;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// /Admin/Databases.
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
        public const string IndexActionName = ActionNames.Index;

        /// <summary>
        /// InitializeAll action name.
        /// </summary>
        public const string InitializeAllActionName = nameof(InitializeAll);

        private readonly IDatabasesWebService databasesService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesController"/> class.
        /// </summary>
        /// <param name="databasesService">Instance of <see cref="IDatabasesWebService"/>.</param>
        /// <param name="logger">Logger.</param>
        public DatabasesController(IDatabasesWebService databasesService, ILogger<DatabasesController> logger)
        {
            this.databasesService = databasesService ?? throw new ArgumentNullException(nameof(databasesService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Admin/Databases/Index.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(IndexActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public IActionResult Index(Uri returnUrl = null)
        {
            if (returnUrl != null)
            {
                return this.Redirect(returnUrl.ToString());
            }

            return this.View();
        }

        /// <summary>
        /// /Admin/Databases/InitializeAll.
        /// </summary>
        /// <param name="returnUrl">Return URL.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [ActionName(InitializeAllActionName)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Endpoint")]
        public async Task<IActionResult> InitializeAll(Uri returnUrl = null)
        {
            try
            {
                var response = await this.databasesService.InitializeAllAsync().ConfigureAwait(false);

                var viewModel = await this.databasesService.MapToViewModelAsync(response).ConfigureAwait(false);
                viewModel.ReturnUrl = returnUrl;

                if (viewModel.Exceptions?.Any() == true)
                {
                    this.logger.LogError(new AggregateException(viewModel.Exceptions), string.Empty);
                }

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.logger.LogError(ex, string.Empty);
            }

            return this.View();
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
