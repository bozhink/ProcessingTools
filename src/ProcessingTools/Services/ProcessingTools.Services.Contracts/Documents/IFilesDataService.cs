// <copyright file="IFilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using ProcessingTools.Services.Models.Contracts.Documents.Files;

    /// <summary>
    /// Files data service.
    /// </summary>
    public interface IFilesDataService : IDataService<IFileModel, IFileDetailsModel, IFileInsertModel, IFileUpdateModel>
    {
    }
}
