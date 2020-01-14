// <copyright file="StylesDataService{TM,TD,TI,TU,TMA,TDA,TDAO}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.DataAccess;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;

    /// <summary>
    /// Styles data service.
    /// </summary>
    /// <typeparam name="TM">Type of the resultant service model.</typeparam>
    /// <typeparam name="TD">Type of the detailed resultant service model.</typeparam>
    /// <typeparam name="TI">Type of the insert model.</typeparam>
    /// <typeparam name="TU">Type of the update model.</typeparam>
    /// <typeparam name="TMA">Type of the data transfer object (DTO).</typeparam>
    /// <typeparam name="TDA">Type of the detailed data transfer object (DTO).</typeparam>
    /// <typeparam name="TDAO">Type of the data access object (DAO).</typeparam>
    public class StylesDataService<TM, TD, TI, TU, TMA, TDA, TDAO> : IStylesDataService, IDataService<TM, TD, TI, TU>
        where TM : class
        where TD : class
        where TI : class
        where TU : class
        where TMA : class, IDataTransferObject
        where TDA : class, IDataTransferObject
        where TDAO : class, IStylesDataAccessObject, IDataAccessObject<TMA, TDA, TI, TU>
    {
        private readonly TDAO dataAccessObject;
        private readonly IObjectHistoryDataService objectHistoryDataService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StylesDataService{TM,TD,TI,TU,TMA,TDA,TDAO}"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of data access object (DAO).</param>
        /// <param name="objectHistoryDataService">Instance of <see cref="IObjectHistoryDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public StylesDataService(TDAO dataAccessObject, IObjectHistoryDataService objectHistoryDataService, IMapper mapper)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.objectHistoryDataService = objectHistoryDataService ?? throw new ArgumentNullException(nameof(objectHistoryDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public virtual Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DeleteInternalAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<TM> GetByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<TD> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetDetailsByIdInternalAsync(id);
        }

        /// <inheritdoc/>
        public virtual async Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id)
        {
            var style = await this.dataAccessObject.GetStyleByIdAsync(id).ConfigureAwait(false);
            return this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>(style);
        }

        /// <inheritdoc/>
        public virtual async Task<IList<IIdentifiedStyleModel>> GetStylesForSelectAsync()
        {
            var styles = await this.dataAccessObject.GetStylesForSelectAsync().ConfigureAwait(false);
            return styles.Select(this.mapper.Map<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>).ToArray();
        }

        /// <inheritdoc/>
        public virtual Task<object> InsertAsync(TI model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public virtual Task<IList<TM>> SelectAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalSkipValue)
            {
                throw new InvalidSkipValuePagingException(StringResources.InvalidSkipValue, nameof(skip));
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(StringResources.InvalidItemsPerPage, nameof(take));
            }

            return this.SelectInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync() => this.dataAccessObject.SelectCountAsync();

        /// <inheritdoc/>
        public virtual Task<IList<TD>> SelectDetailsAsync(int skip, int take)
        {
            if (skip < PaginationConstants.MinimalSkipValue)
            {
                throw new InvalidSkipValuePagingException(StringResources.InvalidSkipValue, nameof(skip));
            }

            if (take < PaginationConstants.MinimalItemsPerPage || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException(StringResources.InvalidItemsPerPage, nameof(take));
            }

            return this.SelectDetailsInternalAsync(skip, take);
        }

        /// <inheritdoc/>
        public virtual Task<object> UpdateAsync(TU model)
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

        private async Task<TM> GetByIdInternalAsync(object id)
        {
            var parseStyle = await this.dataAccessObject.GetByIdAsync(id).ConfigureAwait(false);

            if (parseStyle is null)
            {
                return null;
            }

            return this.mapper.Map<TM>(parseStyle);
        }

        private async Task<TD> GetDetailsByIdInternalAsync(object id)
        {
            var parseStyle = await this.dataAccessObject.GetDetailsByIdAsync(id).ConfigureAwait(false);

            if (parseStyle is null)
            {
                return null;
            }

            return this.mapper.Map<TD>(parseStyle);
        }

        private async Task<object> InsertInternalAsync(TI model)
        {
            var parseStyle = await this.dataAccessObject.InsertAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle is null)
            {
                throw new InsertUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
        }

        private async Task<IList<TD>> SelectDetailsInternalAsync(int skip, int take)
        {
            var parseStyles = await this.dataAccessObject.SelectDetailsAsync(skip, take).ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<TD>();
            }

            return parseStyles.Select(this.mapper.Map<TD>).ToArray();
        }

        private async Task<IList<TM>> SelectInternalAsync(int skip, int take)
        {
            var parseStyles = await this.dataAccessObject.SelectAsync(skip, take).ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<TM>();
            }

            return parseStyles.Select(this.mapper.Map<TM>).ToArray();
        }

        private async Task<object> UpdateInternalAsync(TU model)
        {
            var parseStyle = await this.dataAccessObject.UpdateAsync(model).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            if (parseStyle is null)
            {
                throw new UpdateUnsuccessfulException();
            }

            await this.objectHistoryDataService.AddAsync(parseStyle.ObjectId, parseStyle).ConfigureAwait(false);

            return parseStyle.ObjectId;
        }
    }
}
