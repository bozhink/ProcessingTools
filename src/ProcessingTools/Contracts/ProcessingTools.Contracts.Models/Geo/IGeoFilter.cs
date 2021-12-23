// <copyright file="IGeoFilter.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    /// <summary>
    /// Base filter for geo-objects.
    /// </summary>
    public interface IGeoFilter : INamed, IIdentified<int?>, IFilter
    {
    }
}
