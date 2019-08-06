// <copyright file="IIdentifiedTemplateDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Templates
{
    using ProcessingTools.Contracts.Models.Layout.Templates;

    /// <summary>
    /// Identified template data transfer object (DTO).
    /// </summary>
    public interface IIdentifiedTemplateDataTransferObject : IIdentifiedDataTransferObject, IIdentifiedTemplateModel
    {
    }
}
