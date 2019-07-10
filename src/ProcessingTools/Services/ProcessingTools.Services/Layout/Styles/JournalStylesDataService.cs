// <copyright file="JournalStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.History;
using ProcessingTools.Contracts.Services.Layout.Styles;
using ProcessingTools.Contracts.Services.Models.Layout.Styles;
using ProcessingTools.Contracts.Services.Models.Layout.Styles.Floats;
using ProcessingTools.Contracts.Services.Models.Layout.Styles.Journals;
using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles.Journals;
    using ProcessingTools.Services.Models.Layout.Styles.References;

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
        public JournalStylesDataService(IJournalStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IJournalStyleDataTransferObject, JournalStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IJournalDetailsStyleDataTransferObject, JournalDetailsStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, StyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>().As<StyleModel>();

                c.CreateMap<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IFloatObjectParseStyleDataTransferObject, IFloatObjectParseStyleModel>().As<FloatObjectParseStyleModel>();

                c.CreateMap<IFloatObjectTagStyleDataTransferObject, FloatObjectTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IFloatObjectTagStyleDataTransferObject, IFloatObjectTagStyleModel>().As<FloatObjectTagStyleModel>();

                c.CreateMap<IReferenceParseStyleDataTransferObject, ReferenceParseStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IReferenceParseStyleDataTransferObject, IReferenceParseStyleModel>().As<ReferenceParseStyleModel>();

                c.CreateMap<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IReferenceTagStyleDataTransferObject, IReferenceTagStyleModel>().As<ReferenceTagStyleModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IJournalInsertStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var journalStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journalStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journalStyle.ObjectId, journalStyle).ConfigureAwait(false);

            return journalStyle.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IJournalUpdateStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var journalStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (journalStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(journalStyle.ObjectId, journalStyle).ConfigureAwait(false);

            return journalStyle.ObjectId;
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
        public async Task<IJournalStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var journalStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (journalStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalStyleDataTransferObject, JournalStyleModel>(journalStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var journalStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (journalStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IJournalDetailsStyleDataTransferObject, JournalDetailsStyleModel>(journalStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IJournalStyleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var journalStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return Array.Empty<IJournalStyleModel>();
            }

            var items = journalStyles.Select(this.mapper.Map<IJournalStyleDataTransferObject, JournalStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsStyleModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var journalStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return Array.Empty<IJournalDetailsStyleModel>();
            }

            var items = journalStyles.Select(this.mapper.Map<IJournalDetailsStyleDataTransferObject, JournalDetailsStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id)
        {
            var style = await this.dataAccessObject.GetStyleByIdAsync(id).ConfigureAwait(false);
            return this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>(style);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel[]> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectParseStyleModel[]> GetFloatObjectParseStylesAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var styles = await this.dataAccessObject.GetFloatObjectParseStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IFloatObjectParseStyleDataTransferObject, IFloatObjectParseStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectTagStyleModel[]> GetFloatObjectTagStylesAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var styles = await this.dataAccessObject.GetFloatObjectTagStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IFloatObjectTagStyleDataTransferObject, IFloatObjectTagStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IReferenceParseStyleModel[]> GetReferenceParseStylesAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var styles = await this.dataAccessObject.GetReferenceParseStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IReferenceParseStyleDataTransferObject, IReferenceParseStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IReferenceTagStyleModel[]> GetReferenceTagStylesAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var styles = await this.dataAccessObject.GetReferenceTagStylesAsync(id).ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IReferenceTagStyleDataTransferObject, IReferenceTagStyleModel>).ToArray();
        }
    }
}
