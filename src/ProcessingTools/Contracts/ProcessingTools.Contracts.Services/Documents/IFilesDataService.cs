// <copyright file="IFilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using ProcessingTools.Contracts.Services.Models.Documents.Files;

    /// <summary>
    /// Files data service.
    /// </summary>
    public interface IFilesDataService : IDataService<IFileModel, IFileDetailsModel, IFileInsertModel, IFileUpdateModel>
    {
    }
}
