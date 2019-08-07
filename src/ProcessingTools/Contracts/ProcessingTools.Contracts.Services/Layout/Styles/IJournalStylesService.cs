// <copyright file="IJournalStylesService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles service.
    /// </summary>
    public interface IJournalStylesService
    {
        /// <summary>
        /// Inserts new journal style.
        /// </summary>
        /// <param name="model">Journal style model for insert operation.</param>
        /// <returns>Resultant object.</returns>
        Task<object> InsertAsync(IJournalInsertStyleModel model);

        /// <summary>
        /// Updates journal style.
        /// </summary>
        /// <param name="model">Journal style model for update operation.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UpdateAsync(IJournalUpdateStyleModel model);

        /// <summary>
        /// Deletes journal style specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Resultant object.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Gets journal style specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Journal style.</returns>
        Task<IJournalStyleModel> GetByIdAsync(object id);

        /// <summary>
        /// Gets journal style details specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Journal style details.</returns>
        Task<IJournalDetailsStyleModel> GetDetailsByIdAsync(object id);

        /// <summary>
        /// Select journal styles for pagination.
        /// </summary>
        /// <param name="skip">Number of journal styles to skip.</param>
        /// <param name="take">Number of journal styles to take.</param>
        /// <returns>Array of journal styles.</returns>
        Task<IList<IJournalStyleModel>> SelectAsync(int skip, int take);

        /// <summary>
        /// Select journal style details for pagination.
        /// </summary>
        /// <param name="skip">Number of journal styles to skip.</param>
        /// <param name="take">Number of journal styles to take.</param>
        /// <returns>Array of journal style details.</returns>
        Task<IList<IJournalDetailsStyleModel>> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Gets the number of journal styles.
        /// </summary>
        /// <returns>Number of journal styles.</returns>
        Task<long> SelectCountAsync();

        /// <summary>
        /// Gets float object parse styles for select.
        /// </summary>
        /// <returns>Array of style models.</returns>
        Task<IList<IIdentifiedStyleModel>> GetFloatObjectParseStylesForSelectAsync();

        /// <summary>
        /// Gets float object tag styles for select.
        /// </summary>
        /// <returns>Array of style models.</returns>
        Task<IList<IIdentifiedStyleModel>> GetFloatObjectTagStylesForSelectAsync();

        /// <summary>
        /// Gets reference parse styles for select.
        /// </summary>
        /// <returns>Array of style models.</returns>
        Task<IList<IIdentifiedStyleModel>> GetReferenceParseStylesForSelectAsync();

        /// <summary>
        /// Gets reference tag styles for select.
        /// </summary>
        /// <returns>Array of style models.</returns>
        Task<IList<IIdentifiedStyleModel>> GetReferenceTagStylesForSelectAsync();
    }
}
