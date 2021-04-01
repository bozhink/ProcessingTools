// <copyright file="IFileModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents.Files
{
    /// <summary>
    /// File model.
    /// </summary>
    public interface IFileModel : IFileBaseModel, IStringIdentified, ICreated, IModified
    {
    }
}
