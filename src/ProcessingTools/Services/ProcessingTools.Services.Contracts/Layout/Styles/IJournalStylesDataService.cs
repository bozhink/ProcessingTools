// <copyright file="IJournalStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles data service.
    /// </summary>
    public interface IJournalStylesDataService : IDataService<IJournalStyleModel, IJournalDetailsStyleModel, IJournalInsertStyleModel, IJournalUpdateStyleModel>
    {
    }
}
