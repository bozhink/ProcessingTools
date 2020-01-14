// <copyright file="ReferenceTagStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
    /// Reference tag styles data service.
    /// </summary>
    public class ReferenceTagStylesDataService : StylesDataService<IReferenceTagStyleModel, IReferenceDetailsTagStyleModel, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel, IReferenceTagStyleDataTransferObject, IReferenceDetailsTagStyleDataTransferObject, IReferenceTagStylesDataAccessObject>, IReferenceTagStylesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Instance of <see cref="IReferenceTagStylesDataAccessObject"/>.</param>
        /// <param name="objectHistoryDataService">Instance of <see cref="IObjectHistoryDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public ReferenceTagStylesDataService(IReferenceTagStylesDataAccessObject dataAccessObject, IObjectHistoryDataService objectHistoryDataService, IMapper mapper)
            : base(dataAccessObject, objectHistoryDataService, mapper)
        {
        }
    }
}
