// <copyright file="IJournalStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;

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
        Task<IFloatObjectParseStyleModel[]> GetFloatObjectParseStylesAsync(object id);

        /// <summary>
        /// Get referenced float object tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of float object tag styles.</returns>
        Task<IFloatObjectTagStyleModel[]> GetFloatObjectTagStylesAsync(object id);

        /// <summary>
        /// Get referenced reference parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference parse styles.</returns>
        Task<IReferenceParseStyleModel[]> GetReferenceParseStylesAsync(object id);

        /// <summary>
        /// Get referenced reference tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference tag styles.</returns>
        Task<IReferenceTagStyleModel[]> GetReferenceTagStylesAsync(object id);
    }
}
