// <copyright file="IJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Journals
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Journal model.
    /// </summary>
    public interface IJournalModel : IJournalBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
