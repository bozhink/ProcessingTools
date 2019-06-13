// <copyright file="INamedGuidIdentified.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with name and GUID ID.
    /// </summary>
    public interface INamedGuidIdentified : INamed, IGuidIdentified
    {
    }
}
