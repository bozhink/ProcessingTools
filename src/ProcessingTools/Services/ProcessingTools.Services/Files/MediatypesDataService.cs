// <copyright file="MediatypesDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.DataAccess.Files;
    using ProcessingTools.Contracts.DataAccess.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Services.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatypes data service.
    /// </summary>
    public class MediatypesDataService : IMediatypesDataService
    {
        private readonly IMediatypesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public MediatypesDataService(IMediatypesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ProcessingTools.Contracts.Models.Files.Mediatypes.IMediatypeMetaModel, MediatypeMetaModel>();
                c.CreateMap<ProcessingTools.Contracts.Models.Files.Mediatypes.IMediatypeMetaModel, IMediatypeMetaModel>().As<MediatypeMetaModel>();

                c.CreateMap<IMediatypeDataTransferObject, MediatypeModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IMediatypeDetailsDataTransferObject, MediatypeDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DeleteInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IMediatypeModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IMediatypeDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IMediatypeMetaModel> GetMediatypeByExtensionAsync(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return null;
            }

            var mediatype = await this.dataAccessObject.GetMediatypeByExtensionAsync(extension).ConfigureAwait(false);

            if (mediatype == null)
            {
                return null;
            }

            return this.mapper.Map<IMediatypeMetaModel>(mediatype);
        }

        /// <inheritdoc/>
        public async Task<IList<IMediatypeMetaModel>> GetMediatypesByExtensionAsync(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return Array.Empty<IMediatypeMetaModel>();
            }

            var mediatypes = await this.dataAccessObject.GetMediatypesByExtensionAsync(extension).ConfigureAwait(false);

            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeMetaModel>();
            }

            return mediatypes.Select(this.mapper.Map<IMediatypeMetaModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IList<string>> GetMimeSubtypesAsync()
        {
            return await this.dataAccessObject.GetMimeSubtypesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<string>> GetMimeTypesAsync()
        {
            return await this.dataAccessObject.GetMimeTypesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IMediatypeInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IList<IMediatypeModel>> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidSkipValuePagingException(StringResources.InvalidSkipValue);
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(StringResources.InvalidItemsPerPage);
            }

            return this.SelectInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IList<IMediatypeDetailsModel>> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidSkipValuePagingException(StringResources.InvalidSkipValue);
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(StringResources.InvalidItemsPerPage);
            }

            return this.SelectDetailsInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IMediatypeUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
        }

        private async Task<object> DeleteInternalAsync(object id)
        {
            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<IMediatypeModel> GetByIdInternalAsync(object id)
        {
            var mediatype = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (mediatype == null)
            {
                return null;
            }

            var model = this.mapper.Map<IMediatypeDataTransferObject, MediatypeModel>(mediatype);

            return model;
        }

        private async Task<IMediatypeDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var mediatype = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (mediatype == null)
            {
                return null;
            }

            var model = this.mapper.Map<IMediatypeDetailsDataTransferObject, MediatypeDetailsModel>(mediatype);

            return model;
        }

        private async Task<object> InsertInternalAsync(IMediatypeInsertModel model)
        {
            var mediatype = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (mediatype == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(mediatype.ObjectId, mediatype).ConfigureAwait(false);

            return mediatype.ObjectId;
        }

        private async Task<IList<IMediatypeDetailsModel>> SelectDetailsInternalAsync(int skip, int take)
        {
            var mediatypes = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDetailsModel>();
            }

            var items = mediatypes.Select(this.mapper.Map<IMediatypeDetailsDataTransferObject, MediatypeDetailsModel>).ToArray();
            return items;
        }

        private async Task<IList<IMediatypeModel>> SelectInternalAsync(int skip, int take)
        {
            var mediatypes = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeModel>();
            }

            var items = mediatypes.Select(this.mapper.Map<IMediatypeDataTransferObject, MediatypeModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IMediatypeUpdateModel model)
        {
            var mediatype = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (mediatype == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(mediatype.ObjectId, mediatype).ConfigureAwait(false);

            return mediatype.ObjectId;
        }
    }
}
