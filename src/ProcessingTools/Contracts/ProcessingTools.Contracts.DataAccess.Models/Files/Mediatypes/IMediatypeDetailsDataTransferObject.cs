// <copyright file="IMediatypeDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Files.Mediatypes
{
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatype details data transfer object (DTO).
    /// </summary>
    public interface IMediatypeDetailsDataTransferObject : IMediatypeDataTransferObject, IMediatypeDetailsModel
    {
    }
}
