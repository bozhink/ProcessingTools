// <copyright file="IFilesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using ProcessingTools.Data.Models.Contracts.Documents.Files;
    using ProcessingTools.Models.Contracts.Documents.Files;

    /// <summary>
    /// Files data access object.
    /// </summary>
    public interface IFilesDataAccessObject : IDataAccessObject<IFileDataModel, IFileDetailsDataModel, IFileInsertModel, IFileUpdateModel>
    {
    }
}
