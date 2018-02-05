// <copyright file="IJournalsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journals data access object.
    /// </summary>
    public interface IJournalsDataAccessObject : IDataAccessObject<IJournalDataModel, IJournalDetailsDataModel, IJournalInsertDataModel, IJournalUpdateDataModel>
    {
    }
}
