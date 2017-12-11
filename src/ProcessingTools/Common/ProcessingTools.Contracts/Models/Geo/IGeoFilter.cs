// <copyright file="IGeoFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Base filter for geo-objects.
    /// </summary>
    public interface IGeoFilter : INameable, IIdentifiable<int?>, IFilter
    {
    }
}
