// <copyright file="PublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
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
        private readonly IPublishersDataAccessObject dao;
        private readonly IObjectHistoryDataService history;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersDataService"/> class.
        /// </summary>
        /// <param name="dao">Data access object.</param>
        /// <param name="history">Object history data service.</param>
        public PublishersDataService(IPublishersDataAccessObject dao, IObjectHistoryDataService history)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
            this.history = history ?? throw new ArgumentNullException(nameof(history));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IPublisherDataModel, PublisherModel>();
                c.CreateMap<IPublisherDetailsDataModel, PublisherDetailsModel>();
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

            var entity = await this.dao.InsertAsync(model).ConfigureAwait(false);
            await this.history.AddAsync(entity.ObjectId, entity).ConfigureAwait(false);

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IPublisherUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = await this.dao.UpdateAsync(model).ConfigureAwait(false);
            await this.history.AddAsync(entity.ObjectId, entity).ConfigureAwait(false);

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.dao.DeleteAsync(id).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<IPublisherDetailsModel> GetById(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await this.dao.GetDetailsById(id).ConfigureAwait(false);
            if (entity == null)
            {
                return null;
            }

            var model = this.mapper.Map<IPublisherDetailsDataModel, PublisherDetailsModel>(entity);

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

            var entities = await this.dao.SelectAsync(skip, take).ConfigureAwait(false);
            if (entities == null || !entities.Any())
            {
                return new IPublisherModel[] { };
            }

            var items = entities.Select(this.mapper.Map<IPublisherDataModel, PublisherModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<int> SelectCountAsync() => this.dao.SelectCountAsync();

        /// <inheritdoc/>
        public Task<long> SelectLongCountAsync() => this.dao.SelectLongCountAsync();
    }
}
