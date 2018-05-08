// <copyright file="JournalStylesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout.Styles
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Attributes;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles service.
    /// </summary>
    public class JournalStylesService : IJournalStylesService
    {
        /// <summary>
        /// Gets or sets <see cref="IJournalStylesDataService"/>,
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
        public Task<IJournalStyleModel[]> SelectAsync(int skip, int take) => this.JournalStylesDataService.SelectAsync(skip, take);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<IJournalDetailsStyleModel[]> SelectDetailsAsync(int skip, int take) => this.JournalStylesDataService.SelectDetailsAsync(skip, take);

        /// <inheritdoc/>
        [InjectProperty(nameof(JournalStylesDataService))]
        public Task<long> SelectCountAsync() => this.JournalStylesDataService.SelectCountAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(FloatObjectParseStylesDataService))]
        public Task<IIdentifiedStyleModel[]> GetFloatObjectParseStylesForSelectAsync() => this.FloatObjectParseStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(FloatObjectTagStylesDataService))]
        public Task<IIdentifiedStyleModel[]> GetFloatObjectTagStylesForSelectAsync() => this.FloatObjectTagStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(ReferenceParseStylesDataService))]
        public Task<IIdentifiedStyleModel[]> GetReferenceParseStylesForSelectAsync() => this.ReferenceParseStylesDataService.GetStylesForSelectAsync();

        /// <inheritdoc/>
        [InjectProperty(nameof(ReferenceTagStylesDataService))]
        public Task<IIdentifiedStyleModel[]> GetReferenceTagStylesForSelectAsync() => this.ReferenceTagStylesDataService.GetStylesForSelectAsync();
    }
}
