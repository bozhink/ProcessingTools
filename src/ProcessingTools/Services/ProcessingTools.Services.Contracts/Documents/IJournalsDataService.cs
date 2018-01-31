// <copyright file="IJournalsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journals data service.
    /// </summary>
    public interface IJournalsDataService : IDataService<IJournalModel, IJournalDetailsModel, IJournalInsertModel, IJournalUpdateModel>
    {
    }
}
