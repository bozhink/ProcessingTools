// <copyright file="IFilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Files;

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Files data service.
    /// </summary>
    public interface IFilesDataService : IDataService<IFileModel, IFileDetailsModel, IFileInsertModel, IFileUpdateModel>
    {
    }
}
