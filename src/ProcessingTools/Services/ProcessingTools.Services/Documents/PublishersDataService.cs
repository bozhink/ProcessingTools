// <copyright file="PublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Services.Models.Documents.Publishers;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public class PublishersDataService : IPublishersDataService
    {
        private readonly IPublishersDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

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
                c.CreateMap<IPublisherDataModel, PublisherModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IPublisherDetailsDataModel, PublisherDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IPublisherInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var publisher = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (publisher == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(publisher.ObjectId, publisher).ConfigureAwait(false);

            return publisher.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IPublisherUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var publisher = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (publisher == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(publisher.ObjectId, publisher).ConfigureAwait(false);

            return publisher.ObjectId;
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
        public async Task<IPublisherModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var publisher = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (publisher == null)
            {
                return null;
            }

            var model = this.mapper.Map<IPublisherDataModel, PublisherModel>(publisher);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IPublisherDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var publisher = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (publisher == null)
            {
                return null;
            }

            var model = this.mapper.Map<IPublisherDetailsDataModel, PublisherDetailsModel>(publisher);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IPublisherModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var publishers = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return Array.Empty<IPublisherModel>();
            }

            var items = publishers.Select(this.mapper.Map<IPublisherDataModel, PublisherModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IPublisherDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var publishers = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return Array.Empty<IPublisherDetailsModel>();
            }

            var items = publishers.Select(this.mapper.Map<IPublisherDetailsDataModel, PublisherDetailsModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();
    }
}
