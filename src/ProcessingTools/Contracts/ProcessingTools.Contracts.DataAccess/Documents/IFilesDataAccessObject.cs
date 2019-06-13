// <copyright file="IFilesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Documents
{
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Files;
    using ProcessingTools.Contracts.Models.Documents.Files;

    /// <summary>
    /// Files data access object.
    /// </summary>
    public interface IFilesDataAccessObject : IDataAccessObject<IFileDataTransferObject, IFileDetailsDataTransferObject, IFileInsertModel, IFileUpdateModel>
    {
    }
}
