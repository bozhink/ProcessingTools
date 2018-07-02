// <copyright file="LayerModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Models
{
    /// <summary>
    /// Layer model.
    /// </summary>
    internal class LayerModel
    {
        /// <summary>
        /// Gets or sets the layer identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the layer name.
        /// </summary>
        public string Layer { get; set; }

        /// <summary>
        /// Gets or sets the layer label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the referent layer.
        /// </summary>
        public string Ref { get; set; }
    }
}
