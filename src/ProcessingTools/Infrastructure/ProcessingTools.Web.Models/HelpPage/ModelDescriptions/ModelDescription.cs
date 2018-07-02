// <copyright file="ModelDescription.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    using System;

    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the model type.
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
