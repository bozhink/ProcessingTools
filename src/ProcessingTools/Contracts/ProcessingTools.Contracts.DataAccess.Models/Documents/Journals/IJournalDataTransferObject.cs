// <copyright file="IJournalDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Journals
{
    using ProcessingTools.Contracts.Models.Documents.Journals;

    /// <summary>
    /// Journal data transfer object (DTO).
    /// </summary>
    public interface IJournalDataTransferObject : IDataTransferObject, IJournalModel
    {
    }
}
