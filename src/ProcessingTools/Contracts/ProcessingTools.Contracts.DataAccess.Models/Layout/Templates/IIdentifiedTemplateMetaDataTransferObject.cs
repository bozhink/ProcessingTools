﻿// <copyright file="IIdentifiedTemplateMetaDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Templates
{
    using ProcessingTools.Contracts.Models.Layout.Templates;

    /// <summary>
    /// Identified template meta data transfer object (DTO).
    /// </summary>
    public interface IIdentifiedTemplateMetaDataTransferObject : IIdentifiedDataTransferObject, IIdentifiedTemplateMetaModel
    {
    }
}
