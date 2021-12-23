﻿// <copyright file="IGeoNameDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Deployment.Geo
{
    /// <summary>
    /// Geo-name deploy model.
    /// </summary>
    public interface IGeoNameDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the value of the geo-name as text.
        /// </summary>
        string NameText { get; }

        /// <summary>
        /// Gets the name of the geo-name as verbatim XML.
        /// </summary>
        string NameXml { get; }
    }
}
