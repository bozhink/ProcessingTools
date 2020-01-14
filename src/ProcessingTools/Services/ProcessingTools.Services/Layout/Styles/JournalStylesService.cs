// <copyright file="JournalStylesService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Services.Layout.Styles;

    /// <summary>
    /// Journal styles service.
    /// </summary>
    public class JournalStylesService : IJournalStylesService
    {
        /// <summary>
        /// Gets or sets <see cref="IJournalStylesDataService"/>.
        /// </summary>
        public IJournalStylesDataService JournalStylesDataService { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IFloatObjectParseStylesDataService"/>.
        /// </summary>
        public IFloatObjectParseStylesDataService FloatObjectParseStylesDataService { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IFloatObjectTagStylesDataService"/>.
        /// </summary>
        public IFloatObjectTagStylesDataService FloatObjectTagStylesDataService { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IReferenceParseStylesDataService"/>.
        /// </summary>
        public IReferenceParseStylesDataService ReferenceParseStylesDataService { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IReferenceTagStylesDataService"/>.
        /// </summary>
        public IReferenceTagStylesDataService ReferenceTagStylesDataService { get; set; }

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<object> InsertAsync(IJournalInsertStyleModel model) => this.JournalStylesDataService.InsertAsync(model);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<object> UpdateAsync(IJournalUpdateStyleModel model) => this.JournalStylesDataService.UpdateAsync(model);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<object> DeleteAsync(object id) => this.JournalStylesDataService.DeleteAsync(id);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<IJournalStyleModel> GetByIdAsync(object id) => this.JournalStylesDataService.GetByIdAsync(id);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<IJournalDetailsStyleModel> GetDetailsByIdAsync(object id) => this.JournalStylesDataService.GetDetailsByIdAsync(id);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<IList<IJournalStyleModel>> SelectAsync(int skip, int take) => this.JournalStylesDataService.SelectAsync(skip, take);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<IList<IJournalDetailsStyleModel>> SelectDetailsAsync(int skip, int take) => this.JournalStylesDataService.SelectDetailsAsync(skip, take);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<long> SelectCountAsync() => this.JournalStylesDataService.SelectCountAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(FloatObjectParseStylesDataService))]
        public Task<IList<IIdentifiedStyleModel>> GetFloatObjectParseStylesForSelectAsync() => this.FloatObjectParseStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(FloatObjectTagStylesDataService))]
        public Task<IList<IIdentifiedStyleModel>> GetFloatObjectTagStylesForSelectAsync() => this.FloatObjectTagStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(ReferenceParseStylesDataService))]
        public Task<IList<IIdentifiedStyleModel>> GetReferenceParseStylesForSelectAsync() => this.ReferenceParseStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(ReferenceTagStylesDataService))]
        public Task<IList<IIdentifiedStyleModel>> GetReferenceTagStylesForSelectAsync() => this.ReferenceTagStylesDataService.GetStylesForSelectAsync();
    }
}
