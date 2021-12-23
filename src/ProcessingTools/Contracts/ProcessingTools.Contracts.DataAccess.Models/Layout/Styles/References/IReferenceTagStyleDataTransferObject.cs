// <copyright file="IReferenceTagStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag style data transfer object (DTO).
    /// </summary>
    public interface IReferenceTagStyleDataTransferObject : IIdentifiedStyleDataTransferObject, IDataTransferObject, IReferenceTagStyleModel
    {
    }
}
