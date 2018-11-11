// <copyright file="MediatypesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Data.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Files.Mediatypes;
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
                c.CreateMap<ProcessingTools.Models.Contracts.Files.Mediatypes.IMediatypeMetaModel, MediatypeMetaModel>();
                c.CreateMap<ProcessingTools.Models.Contracts.Files.Mediatypes.IMediatypeMetaModel, IMediatypeMetaModel>().As<MediatypeMetaModel>();

                c.CreateMap<IMediatypeDataModel, MediatypeModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IMediatypeDetailsDataModel, MediatypeDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IMediatypeInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var mediatype = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (mediatype == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(mediatype.ObjectId, mediatype).ConfigureAwait(false);

            return mediatype.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IMediatypeUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var mediatype = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (mediatype == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(mediatype.ObjectId, mediatype).ConfigureAwait(false);

            return mediatype.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var mediatype = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (mediatype == null)
            {
                return null;
            }

            var model = this.mapper.Map<IMediatypeDataModel, MediatypeModel>(mediatype);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var mediatype = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (mediatype == null)
            {
                return null;
            }

            var model = this.mapper.Map<IMediatypeDetailsDataModel, MediatypeDetailsModel>(mediatype);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var mediatypes = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeModel>();
            }

            var items = mediatypes.Select(this.mapper.Map<IMediatypeDataModel, MediatypeModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var mediatypes = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDetailsModel>();
            }

            var items = mediatypes.Select(this.mapper.Map<IMediatypeDetailsDataModel, MediatypeDetailsModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<string[]> GetMimeTypesAsync()
        {
            return await this.dataAccessObject.GetMimeTypesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<string[]> GetMimeSubtypesAsync()
        {
            return await this.dataAccessObject.GetMimeSubtypesAsync().ConfigureAwait(false);
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
        public async Task<IMediatypeMetaModel[]> GetMediatypesByExtensionAsync(string extension)
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
    }
}
