// <copyright file="EncodeWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Tools
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Tools;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Models.Tools.Encode;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Encode web service.
    /// </summary>
    public class EncodeWebService : IEncodeWebService
    {
        private readonly IEncodeService encodeService;
        private readonly Func<Task<UserContext>> userContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeWebService"/> class.
        /// </summary>
        /// <param name="encodeService">Instance of <see cref="IEncodeService"/>.</param>
        /// <param name="userContext">User context.</param>
        public EncodeWebService(IEncodeService encodeService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.encodeService = encodeService ?? throw new ArgumentNullException(nameof(encodeService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<EncodeBase64ViewModel> EncodeBase64Async(EncodeBase64RequestModel model)
        {
            string result = await this.encodeService.EncodeBase64Async(model?.Content).ConfigureAwait(false);
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64ViewModel(userContext)
            {
                Content = model?.Content,
                Base64EncodedString = result
            };
        }

        /// <inheritdoc/>
        public async Task<EncodeBase64UrlViewModel> EncodeBase64UrlAsync(EncodeBase64UrlRequestModel model)
        {
            string result = await this.encodeService.EncodeBase64UrlAsync(model?.Content).ConfigureAwait(false);
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64UrlViewModel(userContext)
            {
                Content = model?.Content,
                Base64EncodedString = result
            };
        }

        /// <inheritdoc/>
        public async Task<EncodeBase64UrlViewModel> GetEncodeBase64UrlViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64UrlViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<EncodeBase64ViewModel> GetEncodeBase64ViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64ViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<EncodeBase64ViewModel> MapToViewModelAsync(EncodeBase64RequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64ViewModel(userContext)
            {
                Content = model.Content
            };
        }

        /// <inheritdoc/>
        public async Task<EncodeBase64UrlViewModel> MapToViewModelAsync(EncodeBase64UrlRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new EncodeBase64UrlViewModel(userContext)
            {
                Content = model.Content
            };
        }
    }
}
