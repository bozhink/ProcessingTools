// <copyright file="JournalStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;

    /// <summary>
    /// Journal styles data service.
    /// </summary>
    public class JournalStylesDataService : IJournalStylesDataService
    {
        private readonly IJournalStylesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public JournalStylesDataService(IJournalStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService, IMapper mapper)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DeleteInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IJournalStyleModel> GetByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IJournalDetailsStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IList<IFloatObjectParseStyleModel>> GetFloatObjectParseStylesAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetFloatObjectParseStylesInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IList<IFloatObjectTagStyleModel>> GetFloatObjectTagStylesAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetFloatObjectTagStylesInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IList<IReferenceParseStyleModel>> GetReferenceParseStylesAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetReferenceParseStylesInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IList<IReferenceTagStyleModel>> GetReferenceTagStylesAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetReferenceTagStylesInternalAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id)
        {
            var style = await this.dataAccessObject.GetStyleByIdAsync(id).ConfigureAwait(false);
            return this.mapper.Map<IIdentifiedStyleModel>(style);
        }

        /// <inheritdoc/>
        public async Task<IList<IIdentifiedStyleModel>> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IJournalInsertStyleModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IList<IJournalStyleModel>> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException(string.Empty, nameof(skip));
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(string.Empty, nameof(take));
            }

            return this.SelectInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IList<IJournalDetailsStyleModel>> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException(string.Empty, nameof(skip));
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(string.Empty, nameof(take));
            }

            return this.SelectDetailsInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IJournalUpdateStyleModel model)
        {
            if (model is null)
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

        private async Task<IJournalStyleModel> GetByIdInternalAsync(object id)
        {
            var journalStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (journalStyle is null)
            {
                return null;
            }

            return this.mapper.Map<IJournalStyleModel>(journalStyle);
        }

        private async Task<IJournalDetailsStyleModel> GetDetailsByIdInternalAsync(object id)
        {
            var journalStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (journalStyle is null)
            {
                return null;
            }

            return this.mapper.Map<IJournalDetailsStyleModel>(journalStyle);
        }

        private async Task<IList<IFloatObjectParseStyleModel>> GetFloatObjectParseStylesInternalAsync(object id)
        {
            var styles = await this.dataAccessObject.GetFloatObjectParseStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IFloatObjectParseStyleModel>).ToArray();
        }

        private async Task<IList<IFloatObjectTagStyleModel>> GetFloatObjectTagStylesInternalAsync(object id)
        {
            var styles = await this.dataAccessObject.GetFloatObjectTagStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IFloatObjectTagStyleModel>).ToArray();
        }

        private async Task<IList<IReferenceParseStyleModel>> GetReferenceParseStylesInternalAsync(object id)
        {
            var styles = await this.dataAccessObject.GetReferenceParseStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IReferenceParseStyleModel>).ToArray();
        }

        private async Task<IList<IReferenceTagStyleModel>> GetReferenceTagStylesInternalAsync(object id)
        {
            var styles = await this.dataAccessObject.GetReferenceTagStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IReferenceTagStyleModel>).ToArray();
        }

        private async Task<object> InsertInternalAsync(IJournalInsertStyleModel model)
        {
            var journalStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journalStyle is null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journalStyle.ObjectId, journalStyle).ConfigureAwait(false);

            return journalStyle.ObjectId;
        }

        private async Task<IList<IJournalDetailsStyleModel>> SelectDetailsInternalAsync(int skip, int take)
        {
            var journalStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (journalStyles is null || !journalStyles.Any())
            {
                return Array.Empty<IJournalDetailsStyleModel>();
            }

            var items = journalStyles.Select(this.mapper.Map<IJournalDetailsStyleModel>).ToArray();
            return items;
        }

        private async Task<IList<IJournalStyleModel>> SelectInternalAsync(int skip, int take)
        {
            var journalStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (journalStyles is null || !journalStyles.Any())
            {
                return Array.Empty<IJournalStyleModel>();
            }

            var items = journalStyles.Select(this.mapper.Map<IJournalStyleModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IJournalUpdateStyleModel model)
        {
            var journalStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journalStyle is null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journalStyle.ObjectId, journalStyle).ConfigureAwait(false);

            return journalStyle.ObjectId;
        }
    }
}
