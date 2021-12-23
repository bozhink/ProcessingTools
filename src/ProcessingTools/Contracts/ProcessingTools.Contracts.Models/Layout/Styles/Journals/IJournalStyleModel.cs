// <copyright file="IJournalStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles.Journals
{
    /// <summary>
    /// Journal style model.
    /// </summary>
    public interface IJournalStyleModel : IJournalBaseStyleModel, IIdentifiedStyleModel, ICreated, IModified
    {
    }
}
