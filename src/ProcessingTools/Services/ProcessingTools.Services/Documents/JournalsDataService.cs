﻿// <copyright file="JournalsDataService.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Journals;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;
    using ProcessingTools.Services.Models.Documents.Journals;

    /// <summary>
    /// Journals data service.
    /// </summary>
    public class JournalsDataService : IJournalsDataService
    {
        private readonly IJournalsDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

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
        public async Task<object> InsertAsync(IJournalInsertModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var journal = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journal == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journal.ObjectId, journal).ConfigureAwait(false);

            return journal.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IJournalUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var journal = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journal == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journal.ObjectId, journal).ConfigureAwait(false);

            return journal.ObjectId;
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
        public async Task<IJournalModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var journal = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (journal == null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalDataTransferObject, JournalModel>(journal);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var journal = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (journal == null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalDetailsDataTransferObject, JournalDetailsModel>(journal);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IJournalModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var journals = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (journals == null || !journals.Any())
            {
                return Array.Empty<IJournalModel>();
            }

            var items = journals.Select(this.mapper.Map<IJournalDataTransferObject, JournalModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var journals = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            if (journals == null || !journals.Any())
            {
                return Array.Empty<IJournalDetailsModel>();
            }

            var items = journals.Select(this.mapper.Map<IJournalDetailsDataTransferObject, JournalDetailsModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IJournalPublisherModel[]> GetJournalPublishersAsync()
        {
            var publishers = await this.dataAccessObject.GetJournalPublishersAsync().ConfigureAwait(false);
            if (publishers == null || !publishers.Any())
            {
                return Array.Empty<IJournalPublisherModel>();
            }

            return publishers.Select(this.mapper.Map<IJournalPublisherDataTransferObject, JournalPublisherModel>).ToArray();
        }
    }
}
