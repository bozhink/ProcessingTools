// <copyright file="DatabasesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Services.Contracts.Admin;

    /// <summary>
    /// Databases web service.
    /// </summary>
    public class DatabasesWebService : IDatabasesWebService
    {
        private readonly IDatabasesService databasesService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesWebService"/> class.
        /// </summary>
        /// <param name="databasesService">Databases service.</param>
        /// <param name="userContext">User context.</param>
        public DatabasesWebService(IDatabasesService databasesService, IUserContext userContext)
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
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

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
