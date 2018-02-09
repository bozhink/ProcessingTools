// <copyright file="PublishersDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;

    /// <summary>
    /// Publishers data service.
    /// </summary>
    public class PublishersDataService : IPublishersDataService
    {
        private readonly IPublishersDataAccessObject dao;
        private readonly IObjectHistoryDataService history;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersDataService"/> class.
        /// </summary>
        /// <param name="dao">Data access object.</param>
        /// <param name="history">Object history data service.</param>
        public PublishersDataService(IPublishersDataAccessObject dao, IObjectHistoryDataService history)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
            this.history = history ?? throw new ArgumentNullException(nameof(history));
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
        public Task<IPublisherDetailsModel> GetById(object id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPublisherModel[]> SelectAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> SelectCountAsync() => this.dao.SelectCountAsync();

        /// <inheritdoc/>
        public Task<long> SelectLongCountAsync() => this.dao.SelectLongCountAsync();
    }
}
