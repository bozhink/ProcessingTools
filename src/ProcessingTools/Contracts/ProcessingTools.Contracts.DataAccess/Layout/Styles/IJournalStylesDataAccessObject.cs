// <copyright file="IJournalStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles data access object.
    /// </summary>
    public interface IJournalStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IJournalStyleDataModel, IJournalDetailsStyleDataModel, IJournalInsertStyleModel, IJournalUpdateStyleModel>
    {
        /// <summary>
        /// Get referenced float object parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of float object parse styles.</returns>
        Task<IFloatObjectParseStyleDataModel[]> GetFloatObjectParseStylesAsync(object id);

        /// <summary>
        /// Get referenced float object tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of float object tag styles.</returns>
        Task<IFloatObjectTagStyleDataModel[]> GetFloatObjectTagStylesAsync(object id);

        /// <summary>
        /// Get referenced reference parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference parse styles.</returns>
        Task<IReferenceParseStyleDataModel[]> GetReferenceParseStylesAsync(object id);

        /// <summary>
        /// Get referenced reference tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Array of reference tag styles.</returns>
        Task<IReferenceTagStyleDataModel[]> GetReferenceTagStylesAsync(object id);
    }
}
