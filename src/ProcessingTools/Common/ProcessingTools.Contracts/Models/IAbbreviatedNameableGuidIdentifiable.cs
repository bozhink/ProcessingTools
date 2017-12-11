// <copyright file="IAbbreviatedNameableGuidIdentifiable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with abbreviated name and GUID id.
    /// </summary>
    public interface IAbbreviatedNameableGuidIdentifiable : IAbbreviatedNameable, INameableGuidIdentifiable
    {
    }
}
