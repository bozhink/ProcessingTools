// <copyright file="IGeoNamesDeployDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Deployment.Geo
{
    using ProcessingTools.Contracts.Models.Deployment.Geo;

    /// <summary>
    /// Geo names deploy data access object.
    /// </summary>
    public interface IGeoNamesDeployDataAccessObject : IDeployDataAccessObject<IGeoNameDeployModel>
    {
    }
}
