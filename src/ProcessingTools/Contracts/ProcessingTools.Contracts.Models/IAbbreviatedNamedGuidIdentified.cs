// <copyright file="IAbbreviatedNamedGuidIdentified.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with abbreviated name and GUID id.
    /// </summary>
    public interface IAbbreviatedNamedGuidIdentified : IAbbreviatedNamed, INamedGuidIdentified
    {
    }
}
