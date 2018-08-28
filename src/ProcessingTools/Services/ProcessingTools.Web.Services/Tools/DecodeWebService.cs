// <copyright file="DecodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Tools
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Tools;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Models.Tools.Decode;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Decode web service.
    /// </summary>
    public class DecodeWebService : IDecodeWebService
    {
        private readonly IDecodeService decodeService;
        private readonly Func<Task<UserContext>> userContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeWebService"/> class.
        /// </summary>
        /// <param name="decodeService">Instance of <see cref="IDecodeService"/>.</param>
        /// <param name="userContext">User context.</param>
        public DecodeWebService(IDecodeService decodeService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.decodeService = decodeService ?? throw new ArgumentNullException(nameof(decodeService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<DecodeBase64ViewModel> DecodeBase64Async(DecodeBase64RequestModel model)
        {
            string result = await this.decodeService.DecodeBase64Async(model?.Content).ConfigureAwait(false);
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64ViewModel(userContext)
            {
                Content = model?.Content,
                Base64DecodedString = result
            };
        }

        /// <inheritdoc/>
        public async Task<DecodeBase64UrlViewModel> DecodeBase64UrlAsync(DecodeBase64UrlRequestModel model)
        {
            string result = await this.decodeService.DecodeBase64UrlAsync(model?.Content).ConfigureAwait(false);
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64UrlViewModel(userContext)
            {
                Content = model?.Content,
                Base64DecodedString = result
            };
        }

        /// <inheritdoc/>
        public async Task<DecodeBase64UrlViewModel> GetDecodeBase64UrlViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64UrlViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<DecodeBase64ViewModel> GetDecodeBase64ViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64ViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<DecodeBase64ViewModel> MapToViewModelAsync(DecodeBase64RequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64ViewModel(userContext)
            {
                Content = model?.Content
            };
        }

        /// <inheritdoc/>
        public async Task<DecodeBase64UrlViewModel> MapToViewModelAsync(DecodeBase64UrlRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new DecodeBase64UrlViewModel(userContext)
            {
                Content = model.Content
            };
        }
    }
}
