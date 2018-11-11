// <copyright file="IMediatypeModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Files.Mediatypes
{
    /// <summary>
    /// Mediatype model.
    /// </summary>
    public interface IMediatypeModel : IMediatypeBaseModel, IStringIdentifiable, ICreated, IModified
    {
    }
}
