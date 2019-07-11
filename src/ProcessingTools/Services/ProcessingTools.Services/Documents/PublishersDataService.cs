// <copyright file="PublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Publishers;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Models.Documents.Publishers;
    using ProcessingTools.Services.Models.Documents.Publishers;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public class PublishersDataService : IPublishersDataService
    {
        private readonly IPublishersDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public PublishersDataService(IPublishersDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IPublisherDataTransferObject, PublisherModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IPublisherDetailsDataTransferObject, PublisherDetailsModel>()
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
        public Task<IPublisherModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IPublisherDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IPublisherInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IPublisherModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            return this.SelectInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IPublisherDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            return this.SelectDetailsInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IPublisherUpdateModel model)
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

        private async Task<IPublisherModel> GetByIdInternalAsync(object id)
        {
            var publisher = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (publisher == null)
            {
                return null;
            }

            var model = this.mapper.Map<IPublisherDataTransferObject, PublisherModel>(publisher);

            return model;
        }

        private async Task<IPublisherDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var publisher = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (publisher == null)
            {
                return null;
            }

            var model = this.mapper.Map<IPublisherDetailsDataTransferObject, PublisherDetailsModel>(publisher);

            return model;
        }

        private async Task<object> InsertInternalAsync(IPublisherInsertModel model)
        {
            var publisher = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (publisher == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(publisher.ObjectId, publisher).ConfigureAwait(false);

            return publisher.ObjectId;
        }

        private async Task<IPublisherDetailsModel[]> SelectDetailsInternalAsync(int skip, int take)
        {
            var publishers = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return Array.Empty<IPublisherDetailsModel>();
            }

            var items = publishers.Select(this.mapper.Map<IPublisherDetailsDataTransferObject, PublisherDetailsModel>).ToArray();
            return items;
        }

        private async Task<IPublisherModel[]> SelectInternalAsync(int skip, int take)
        {
            var publishers = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return Array.Empty<IPublisherModel>();
            }

            var items = publishers.Select(this.mapper.Map<IPublisherDataTransferObject, PublisherModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IPublisherUpdateModel model)
        {
            var publisher = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (publisher == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(publisher.ObjectId, publisher).ConfigureAwait(false);

            return publisher.ObjectId;
        }
    }
}
