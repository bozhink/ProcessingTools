// <copyright file="StyleModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles
{
    using ProcessingTools.Contracts.Models.Layout.Styles;

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
