// <copyright file="IJournalStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Models.Contracts.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles data access object.
    /// </summary>
    public interface IJournalStylesDataAccessObject : IStylesDataAccessObject, IDataAccessObject<IJournalStyleDataModel, IJournalDetailsStyleDataModel, IJournalInsertStyleModel, IJournalUpdateStyleModel>
    {
    }
}
