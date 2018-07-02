// <copyright file="IJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal model.
    /// </summary>
    public interface IJournalModel : IJournalBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
