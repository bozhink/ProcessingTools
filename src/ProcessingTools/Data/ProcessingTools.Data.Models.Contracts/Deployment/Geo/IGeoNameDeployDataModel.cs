// <copyright file="IGeoNameDeployDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Deployment.Geo
{
    /// <summary>
    /// Geo name deploy data model.
    /// </summary>
    public interface IGeoNameDeployDataModel : IDeployDataModel
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
