// <copyright file="ReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data service.
    /// </summary>
    public class ReferenceTagStylesDataService : IReferenceTagStylesDataService
    {
        private readonly IReferenceTagStylesDataAccessObject dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

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
                c.CreateMap<IReferenceTagStyleDataModel, ReferenceTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IReferenceDetailsTagStyleDataModel, ReferenceDetailsTagStyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataModel, StyleModel>()
                    .ForMember(sm => sm.Id, o => o.ResolveUsing(dm => dm.ObjectId.ToString()));
                c.CreateMap<IIdentifiedStyleDataModel, IIdentifiedStyleModel>().As<StyleModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IReferenceInsertTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var tagStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IReferenceUpdateTagStyleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var tagStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (tagStyle == null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(tagStyle.ObjectId, tagStyle).ConfigureAwait(false);

            return tagStyle.ObjectId;
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
        public async Task<IReferenceTagStyleModel> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var tagStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceTagStyleDataModel, ReferenceTagStyleModel>(tagStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IReferenceDetailsTagStyleModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var tagStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (tagStyle == null)
            {
                return null;
            }

            var model = this.mapper.Map<IReferenceDetailsTagStyleDataModel, ReferenceDetailsTagStyleModel>(tagStyle);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IReferenceTagStyleModel[]> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var tagStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return new IReferenceTagStyleModel[] { };
            }

            var items = tagStyles.Select(this.mapper.Map<IReferenceTagStyleDataModel, ReferenceTagStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public async Task<IReferenceDetailsTagStyleModel[]> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalPageNumber)
            {
                throw new InvalidPageNumberException();
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var tagStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return new IReferenceDetailsTagStyleModel[] { };
            }

            var items = tagStyles.Select(this.mapper.Map<IReferenceDetailsTagStyleDataModel, ReferenceDetailsTagStyleModel>).ToArray();
            return items;
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleModel[]> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleDataModel, IIdentifiedStyleModel>).ToArray();
        }
    }
}
