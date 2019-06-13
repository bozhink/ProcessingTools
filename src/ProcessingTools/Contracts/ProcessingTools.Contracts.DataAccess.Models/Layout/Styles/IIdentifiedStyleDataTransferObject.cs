// <copyright file="IIdentifiedStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Styles
{
    using ProcessingTools.Contracts.Models.Layout.Styles;

    /// <summary>
    /// Identified style data transfer object (DTO).
    /// </summary>
    public interface IIdentifiedStyleDataTransferObject : IIdentifiedDataTransferObject, IIdentifiedStyleModel
    {
    }
}
