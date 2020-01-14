// <copyright file="IFileModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
