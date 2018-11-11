// <copyright file="IFileModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Files
{
    /// <summary>
    /// File model.
    /// </summary>
    public interface IFileModel : IFileBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
