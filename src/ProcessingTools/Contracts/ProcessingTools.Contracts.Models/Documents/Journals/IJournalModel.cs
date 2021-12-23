// <copyright file="IJournalModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Journals
{
    /// <summary>
    /// Journal model.
    /// </summary>
    public interface IJournalModel : IJournalBaseModel, IStringIdentified, ICreated, IModified
    {
    }
}
