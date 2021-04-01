// <copyright file="IReferenceParseStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References
{
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse style data transfer object (DTO).
    /// </summary>
    public interface IReferenceParseStyleDataTransferObject : IIdentifiedStyleDataTransferObject, IDataTransferObject, IReferenceParseStyleModel
    {
    }
}
