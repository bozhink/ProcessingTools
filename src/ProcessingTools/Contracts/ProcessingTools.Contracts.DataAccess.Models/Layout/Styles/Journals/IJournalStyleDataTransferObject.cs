// <copyright file="IJournalStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals
{
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal style data transfer object (DTO).
    /// </summary>
    public interface IJournalStyleDataTransferObject : IIdentifiedStyleDataTransferObject, IDataTransferObject, IJournalStyleModel
    {
    }
}
