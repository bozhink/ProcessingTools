// <copyright file="IMediatypeDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Files.Mediatypes
{
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatype data transfer object (DTO).
    /// </summary>
    public interface IMediatypeDataTransferObject : IDataTransferObject, IMediatypeModel
    {
    }
}
