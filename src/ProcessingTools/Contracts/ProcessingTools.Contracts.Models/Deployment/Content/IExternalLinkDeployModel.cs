// <copyright file="IExternalLinkDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Deployment.Content
{
    /// <summary>
    /// External link deploy model.
    /// </summary>
    public interface IExternalLinkDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the value of the hyper-reference of the external link.
        /// </summary>
        string Href { get; }

        /// <summary>
        /// Gets the value of the external link content as text.
        /// </summary>
        string ContentText { get; }

        /// <summary>
        /// Gets the value of the external link content as XML.
        /// </summary>
        string ContentXml { get; }
    }
}
