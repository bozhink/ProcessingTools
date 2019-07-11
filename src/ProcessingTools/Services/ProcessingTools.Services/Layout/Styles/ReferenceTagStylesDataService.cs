// <copyright file="ReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data service.
    /// </summary>
    public class ReferenceTagStylesDataService : IReferenceTagStylesDataService
    {
        private readonly IReferenceTagStylesDataAccessObject dataAccessObject;
        private readonly IMapper mapper;
        private readonly IObjectHistoryDataService objectHistoryDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="objectHistoryDataService">Object history data service.</param>
        public ReferenceTagStylesDataService(IReferenceTagStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IReferenceDetailsTagStyleDataTransferObject, ReferenceDetailsTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, StyleModel>()
                    .ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>().As<StyleModel>();
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
        public Task<IReferenceTagStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public Task<IReferenceDetailsTagStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

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
        public Task<object> InsertAsync(IReferenceInsertTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<IReferenceTagStyleModel[]> SelectAsync(int skip, int take)
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
        public Task<IReferenceDetailsTagStyleModel[]> SelectDetailsAsync(int skip, int take)
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
        public Task<object> UpdateAsync(IReferenceUpdateTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateIntenalAsync(model);
        }

        private async Task<object> DeleteInternalAsync(object id)
        {
            var result = await this.dataAccessObject.DeleteAsync(id).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result;
        }

        private async Task<IReferenceTagStyleModel> GetByIdInternalAsync(object id)
        {
            var tagStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>(tagStyle);

            return model;
        }

        private async Task<IReferenceDetailsTagStyleModel> GetDetailsByIdInternalAsync(object id)
        {
            var tagStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceDetailsTagStyleDataTransferObject, ReferenceDetailsTagStyleModel>(tagStyle);

            return model;
        }

        private async Task<object> InsertInternalAsync(IReferenceInsertTagStyleModel model)
        {
            var tagStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
        }

        private async Task<IReferenceDetailsTagStyleModel[]> SelectDetailsInternalAsync(int skip, int take)
        {
            var tagStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceDetailsTagStyleModel>();
            }

            var items = tagStyles.Select(this.mapper.Map<IReferenceDetailsTagStyleDataTransferObject, ReferenceDetailsTagStyleModel>).ToArray();
            return items;
        }

        private async Task<IReferenceTagStyleModel[]> SelectInternalAsync(int skip, int take)
        {
            var tagStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceTagStyleModel>();
            }

            var items = tagStyles.Select(this.mapper.Map<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>).ToArray();
            return items;
        }

        private async Task<object> UpdateIntenalAsync(IReferenceUpdateTagStyleModel model)
        {
            var tagStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
        }
    }
}
