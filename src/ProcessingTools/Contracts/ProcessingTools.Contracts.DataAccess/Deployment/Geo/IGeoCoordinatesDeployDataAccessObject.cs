// <copyright file="IGeoCoordinatesDeployDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Deployment.Geo
{
    using ProcessingTools.Contracts.Models.Deployment.Geo;

    /// <summary>
    /// Geo coordinates deploy data access object.
    /// </summary>
    public interface IGeoCoordinatesDeployDataAccessObject : IDeployDataAccessObject<IGeoCoordinateDeployModel>
    {
    }
}
