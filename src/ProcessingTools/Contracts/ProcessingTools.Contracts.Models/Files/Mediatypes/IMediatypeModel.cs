// <copyright file="IMediatypeModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Files.Mediatypes
{
    /// <summary>
    /// Mediatype model.
    /// </summary>
    public interface IMediatypeModel : IMediatypeBaseModel, IStringIdentified, ICreated, IModified
    {
    }
}
