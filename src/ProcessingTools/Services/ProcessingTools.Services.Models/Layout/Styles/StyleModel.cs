// <copyright file="StyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Layout.Styles;

namespace ProcessingTools.Services.Models.Layout.Styles
{
    using System;

    /// <summary>
    /// Style model.
    /// </summary>
    public class StyleModel : IIdentifiedStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
