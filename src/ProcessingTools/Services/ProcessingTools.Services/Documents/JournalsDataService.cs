// <copyright file="JournalsDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Journals;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Models.Documents.Journals;
    using ProcessingTools.Services.Models.Documents.Journals;

    /// <summary>
    /// Journals data service.
    /// </summary>
    public class JournalsDataService : IJournalsDataService
    {
        private readonly IJournalsDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public JournalsDataService(IJournalsDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IJournalPublisherDataTransferObject, JournalPublisherModel>();
                c.CreateMap<IJournalPublisherDataTransferObject, IJournalPublisherModel>().As<JournalPublisherModel>();

                c.CreateMap<IJournalDataTransferObject, JournalModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IJournalDetailsDataTransferObject, JournalDetailsModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()))
                    .ForMember(sm => sm.Publisher, o => o.MapFrom(dm => dm.Publisher));
            });
            this.mapper = mapperConfiguration.CreateMapper();
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
        public Task<IJournalModel> GetByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IJournalDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IList<IJournalPublisherModel>> GetJournalPublishersAsync()
        {
            var publishers = await this.dataAccessObject.GetJournalPublishersAsync().ConfigureAwait(false);
            if (publishers is null || !publishers.Any())
            {
                return Array.Empty<IJournalPublisherModel>();
            }

            return publishers.Select(this.mapper.Map<IJournalPublisherDataTransferObject, JournalPublisherModel>).ToArray();
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IJournalInsertModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IList<IJournalModel>> SelectAsync(int skip, int take)
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
        public Task<IList<IJournalDetailsModel>> SelectDetailsAsync(int skip, int take)
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
        public Task<object> UpdateAsync(IJournalUpdateModel model)
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

        private async Task<IJournalModel> GetByIdInternalAsync(object id)
        {
            var journal = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (journal is null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalDataTransferObject, JournalModel>(journal);

            return model;
        }

        private async Task<IJournalDetailsModel> GetDetailsByIdInternalAsync(object id)
        {
            var journal = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (journal is null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalDetailsDataTransferObject, JournalDetailsModel>(journal);

            return model;
        }

        private async Task<object> InsertInternalAsync(IJournalInsertModel model)
        {
            var journal = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journal is null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journal.ObjectId, journal).ConfigureAwait(false);

            return journal.ObjectId;
        }

        private async Task<IList<IJournalDetailsModel>> SelectDetailsInternalAsync(int skip, int take)
        {
            var journals = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (journals is null || !journals.Any())
            {
                return Array.Empty<IJournalDetailsModel>();
            }

            var items = journals.Select(this.mapper.Map<IJournalDetailsDataTransferObject, JournalDetailsModel>).ToArray();
            return items;
        }

        private async Task<IList<IJournalModel>> SelectInternalAsync(int skip, int take)
        {
            var journals = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (journals is null || !journals.Any())
            {
                return Array.Empty<IJournalModel>();
            }

            var items = journals.Select(this.mapper.Map<IJournalDataTransferObject, JournalModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateInternalAsync(IJournalUpdateModel model)
        {
            var journal = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journal is null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journal.ObjectId, journal).ConfigureAwait(false);

            return journal.ObjectId;
        }
    }
}
