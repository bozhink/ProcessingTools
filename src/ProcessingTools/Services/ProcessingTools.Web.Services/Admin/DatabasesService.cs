// <copyright file="DatabasesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Admin
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Admin;
    using ProcessingTools.Services.Models.Contracts.Admin.Databases;
    using ProcessingTools.Web.Models.Admin.Databases;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Databases service.
    /// </summary>
    public class DatabasesService : ProcessingTools.Web.Services.Contracts.Admin.IDatabasesService
    {
        private readonly IDatabasesService databasesService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesService"/> class.
        /// </summary>
        /// <param name="databasesService">Databases service.</param>
        /// <param name="userContext">User context.</param>
        public DatabasesService(IDatabasesService databasesService, IUserContext userContext)
        {
            this.databasesService = databasesService ?? throw new ArgumentNullException(nameof(databasesService));

            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IInitializeModel, InitializeResponseModel>();
                c.CreateMap<InitializeResponseModel, InitializeViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<InitializeResponseModel> InitializeAllAsync()
        {
            var result = await this.databasesService.InitializeAllAsync().ConfigureAwait(false);

            var response = this.mapper.Map<IInitializeModel, InitializeResponseModel>(result);

            return response;
        }

        /// <inheritdoc/>
        public async Task<InitializeViewModel> MapToViewModelAsync(InitializeResponseModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new InitializeViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new InitializeViewModel(userContext);
        }
    }
}
