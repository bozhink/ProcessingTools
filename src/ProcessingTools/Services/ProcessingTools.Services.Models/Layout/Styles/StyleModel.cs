// <copyright file="StyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles
{
    using System;

    /// <summary>
    /// Style model.
    /// </summary>
    public class StyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.IIdentifiedStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
