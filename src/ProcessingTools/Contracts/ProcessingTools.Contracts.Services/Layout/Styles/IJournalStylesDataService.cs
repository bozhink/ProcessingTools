// <copyright file="IJournalStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Journal styles data service.
    /// </summary>
    public interface IJournalStylesDataService : IStylesDataService, IDataService<IJournalStyleModel, IJournalDetailsStyleModel, IJournalInsertStyleModel, IJournalUpdateStyleModel>
    {
        /// <summary>
        /// Get referenced float object parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of float object parse styles.</returns>
        Task<IList<IFloatObjectParseStyleModel>> GetFloatObjectParseStylesAsync(object id);

        /// <summary>
        /// Get referenced float object tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of float object tag styles.</returns>
        Task<IList<IFloatObjectTagStyleModel>> GetFloatObjectTagStylesAsync(object id);

        /// <summary>
        /// Get referenced reference parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference parse styles.</returns>
        Task<IList<IReferenceParseStyleModel>> GetReferenceParseStylesAsync(object id);

        /// <summary>
        /// Get referenced reference tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference tag styles.</returns>
        Task<IList<IReferenceTagStyleModel>> GetReferenceTagStylesAsync(object id);
    }
}
