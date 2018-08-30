// <copyright file="HashesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Tools
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Tools;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Models.Tools.Hashes;
    using ProcessingTools.Web.Services.Contracts.Tools;

    /// <summary>
    /// Hashes web service.
    /// </summary>
    public class HashesWebService : IHashesWebService
    {
        private readonly IHashService hashService;
        private readonly Func<Task<UserContext>> userContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashesWebService"/> class.
        /// </summary>
        /// <param name="hashService">Instance of <see cref="IHashService"/>.</param>
        /// <param name="userContext">User context.</param>
        public HashesWebService(IHashService hashService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<HashesViewModel> HashAsync(HashContentRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model == null)
            {
                return new HashesViewModel(userContext);
            }

            return new HashesViewModel(userContext)
            {
                Content = model.Content,
                MD5String = await this.hashService.GetMD5HashAsStringAsync(model.Content).ConfigureAwait(false),
                MD5Base64String = await this.hashService.GetMD5HashAsBase64StringAsync(model.Content).ConfigureAwait(false),
                SHA1String = await this.hashService.GetSHA1HashAsStringAsync(model.Content).ConfigureAwait(false),
                SHA1Base64String = await this.hashService.GetSHA1HashAsBase64StringAsync(model.Content).ConfigureAwait(false),
                SHA256String = await this.hashService.GetSHA256HashAsStringAsync(model.Content).ConfigureAwait(false),
                SHA256Base64String = await this.hashService.GetSHA256HashAsBase64StringAsync(model.Content).ConfigureAwait(false),
                SHA384String = await this.hashService.GetSHA384HashAsStringAsync(model.Content).ConfigureAwait(false),
                SHA384Base64String = await this.hashService.GetSHA384HashAsBase64StringAsync(model.Content).ConfigureAwait(false),
                SHA512String = await this.hashService.GetSHA512HashAsStringAsync(model.Content).ConfigureAwait(false),
                SHA512Base64String = await this.hashService.GetSHA512HashAsBase64StringAsync(model.Content).ConfigureAwait(false)
            };
        }

        /// <inheritdoc/>
        public async Task<HashesViewModel> GetHashesViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new HashesViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<HashesViewModel> MapToViewModelAsync(HashContentRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);
            return new HashesViewModel(userContext)
            {
                Content = model?.Content
            };
        }
    }
}
