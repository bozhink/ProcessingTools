﻿// <copyright file="IGeoFilter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Base filter for geo-objects.
    /// </summary>
    public interface IGeoFilter : INamed, IIdentifiable<int?>, IFilter
    {
    }
}
