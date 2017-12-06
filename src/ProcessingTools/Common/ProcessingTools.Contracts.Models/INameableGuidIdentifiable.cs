// <copyright file="INameableGuidIdentifiable.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with name and GUID ID.
    /// </summary>
    public interface INameableGuidIdentifiable : INameable, IGuidIdentifiable
    {
    }
}
