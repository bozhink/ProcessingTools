// <copyright file="ReferenceParseStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Services.History;
    using ProcessingTools.Contracts.Services.Layout.Styles;

    /// <summary>
    /// Reference parse styles data service.
    /// </summary>
    public class ReferenceParseStylesDataService : StylesDataService<IReferenceParseStyleModel, IReferenceDetailsParseStyleModel, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel, IReferenceParseStyleDataTransferObject, IReferenceDetailsParseStyleDataTransferObject, IReferenceParseStylesDataAccessObject>, IReferenceParseStylesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of <see cref="IReferenceParseStylesDataAccessObject"/>.</param>
        /// <param name="objectHistoryDataService">Instance of <see cref="IObjectHistoryDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public ReferenceParseStylesDataService(IReferenceParseStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService, IMapper mapper)
            : base(dataAccessObject, objectHistoryDataService, mapper)
        {
        }
    }
}
