// <copyright file="IJournalStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles data access object (DAO).
    /// </summary>
    public interface IJournalStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IJournalStyleDataTransferObject, IJournalDetailsStyleDataTransferObject, IJournalInsertStyleModel, IJournalUpdateStyleModel>
    {
        /// <summary>
        /// Get referenced float object parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>List of float object parse styles.</returns>
        Task<IList<IFloatObjectParseStyleDataTransferObject>> GetFloatObjectParseStylesAsync(object id);

        /// <summary>
        /// Get referenced float object tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>List of float object tag styles.</returns>
        Task<IList<IFloatObjectTagStyleDataTransferObject>> GetFloatObjectTagStylesAsync(object id);

        /// <summary>
        /// Get referenced reference parse styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>List of reference parse styles.</returns>
        Task<IList<IReferenceParseStyleDataTransferObject>> GetReferenceParseStylesAsync(object id);

        /// <summary>
        /// Get referenced reference tag styles for specified journal style.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>List of reference tag styles.</returns>
        Task<IList<IReferenceTagStyleDataTransferObject>> GetReferenceTagStylesAsync(object id);
    }
}
