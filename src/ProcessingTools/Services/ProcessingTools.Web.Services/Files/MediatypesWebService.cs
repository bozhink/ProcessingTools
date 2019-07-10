// <copyright file="MediatypesWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Files;
using ProcessingTools.Contracts.Services.Models.Files.Mediatypes;
using ProcessingTools.Contracts.Web.Services.Files;

namespace ProcessingTools.Web.Services.Files
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Web.Models.Files.Mediatypes;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Mediatypes web service.
    /// </summary>
    public class MediatypesWebService : IMediatypesWebService
    {
        private readonly IMediatypesDataService mediatypesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesWebService"/> class.
        /// </summary>
        /// <param name="mediatypesDataService">Instance of <see cref="IMediatypesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public MediatypesWebService(IMediatypesDataService mediatypesDataService, IMapper mapper, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.mediatypesDataService = mediatypesDataService ?? throw new ArgumentNullException(nameof(mediatypesDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreateMediatypeAsync(MediatypeCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.mediatypesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateMediatypeAsync(MediatypeUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.mediatypesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteMediatypeAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.mediatypesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<MediatypesIndexViewModel> GetMediatypesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.mediatypesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.mediatypesDataService.SelectCountAsync().ConfigureAwait(false);

            var mmediatypes = data?.Select(this.mapper.Map<IMediatypeModel, MediatypeIndexViewModel>).ToArray() ?? Array.Empty<MediatypeIndexViewModel>();

            return new MediatypesIndexViewModel(userContext, count, take, skip / take, mmediatypes);
        }

        /// <inheritdoc/>
        public async Task<MediatypeCreateViewModel> GetMediatypeCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var viewModel = new MediatypeCreateViewModel(userContext);

            var mimeTypesTask = this.mediatypesDataService.GetMimeTypesAsync();
            var mimeSubtypesTask = this.mediatypesDataService.GetMimeSubtypesAsync();

            viewModel.MimeTypes = await mimeTypesTask.ConfigureAwait(false);
            viewModel.MimeSubtypes = await mimeSubtypesTask.ConfigureAwait(false);

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<MediatypeEditViewModel> GetMediatypeEditViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var mediatype = await this.mediatypesDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (mediatype != null)
                {
                    var viewModel = new MediatypeEditViewModel(userContext);
                    this.mapper.Map(mediatype, viewModel);

                    var mimeTypesTask = this.mediatypesDataService.GetMimeTypesAsync();
                    var mimeSubtypesTask = this.mediatypesDataService.GetMimeSubtypesAsync();

                    viewModel.MimeTypes = await mimeTypesTask.ConfigureAwait(false);
                    viewModel.MimeSubtypes = await mimeSubtypesTask.ConfigureAwait(false);

                    return viewModel;
                }
            }

            return new MediatypeEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<MediatypeDeleteViewModel> GetMediatypeDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var mediatype = await this.mediatypesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (mediatype != null)
                {
                    var viewModel = new MediatypeDeleteViewModel(userContext);
                    this.mapper.Map(mediatype, viewModel);

                    return viewModel;
                }
            }

            return new MediatypeDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<MediatypeDetailsViewModel> GetMediatypeDetailsViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var mediatype = await this.mediatypesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (mediatype != null)
                {
                    var viewModel = new MediatypeDetailsViewModel(userContext);
                    this.mapper.Map(mediatype, viewModel);

                    return viewModel;
                }
            }

            return new MediatypeDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<MediatypeCreateViewModel> MapToViewModelAsync(MediatypeCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new MediatypeCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                var mimeTypesTask = this.mediatypesDataService.GetMimeTypesAsync();
                var mimeSubtypesTask = this.mediatypesDataService.GetMimeSubtypesAsync();

                viewModel.MimeTypes = await mimeTypesTask.ConfigureAwait(false);
                viewModel.MimeSubtypes = await mimeSubtypesTask.ConfigureAwait(false);

                return viewModel;
            }

            return new MediatypeCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<MediatypeEditViewModel> MapToViewModelAsync(MediatypeUpdateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var mediatype = await this.mediatypesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (mediatype != null)
                {
                    var viewModel = new MediatypeEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = mediatype.CreatedBy;
                    viewModel.CreatedOn = mediatype.CreatedOn;
                    viewModel.ModifiedBy = mediatype.ModifiedBy;
                    viewModel.ModifiedOn = mediatype.ModifiedOn;

                    var mimeTypesTask = this.mediatypesDataService.GetMimeTypesAsync();
                    var mimeSubtypesTask = this.mediatypesDataService.GetMimeSubtypesAsync();

                    viewModel.MimeTypes = await mimeTypesTask.ConfigureAwait(false);
                    viewModel.MimeSubtypes = await mimeSubtypesTask.ConfigureAwait(false);

                    return viewModel;
                }
            }

            return new MediatypeEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<MediatypeDeleteViewModel> MapToViewModelAsync(MediatypeDeleteRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var mediatype = await this.mediatypesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (mediatype != null)
                {
                    var viewModel = new MediatypeDeleteViewModel(userContext);
                    this.mapper.Map(mediatype, viewModel);

                    return viewModel;
                }
            }

            return new MediatypeDeleteViewModel(userContext);
        }
    }
}
