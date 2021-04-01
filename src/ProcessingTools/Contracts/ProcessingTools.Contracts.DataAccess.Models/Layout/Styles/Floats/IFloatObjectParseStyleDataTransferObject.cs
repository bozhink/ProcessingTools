// <copyright file="IFloatObjectParseStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats
{
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse style data transfer object (DTO).
    /// </summary>
    public interface IFloatObjectParseStyleDataTransferObject : IIdentifiedStyleDataTransferObject, IDataTransferObject, IFloatObjectParseStyleModel
    {
    }
}
