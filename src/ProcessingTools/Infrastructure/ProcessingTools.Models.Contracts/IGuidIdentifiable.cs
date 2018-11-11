// <copyright file="IGuidIdentifiable.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    using System;

    /// <summary>
    /// Model with GUID ID.
    /// </summary>
    public interface IGuidIdentifiable : IIdentifiable<Guid>
    {
    }
}
