// <copyright file="FloatObjectParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;

    /// <summary>
    /// Float object parse styles data service.
    /// </summary>
    public class FloatObjectParseStylesDataService : StylesDataService<IFloatObjectParseStyleModel, IFloatObjectDetailsParseStyleModel, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel, IFloatObjectParseStyleDataTransferObject, IFloatObjectDetailsParseStyleDataTransferObject, IFloatObjectParseStylesDataAccessObject>, IFloatObjectParseStylesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of <see cref="IFloatObjectParseStylesDataAccessObject"/>.</param>
        /// <param name="objectHistoryDataService">Instance of <see cref="IObjectHistoryDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public FloatObjectParseStylesDataService(IFloatObjectParseStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService, IMapper mapper)
            : base(dataAccessObject, objectHistoryDataService, mapper)
        {
        }
    }
}
