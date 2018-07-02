// <copyright file="ParameterAnnotation.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    using System;

    /// <summary>
    /// Parameter annotation
    /// </summary>
    public class ParameterAnnotation
    {
        /// <summary>
        /// Gets or sets the annotation attribute.
        /// </summary>
        public Attribute AnnotationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public string Documentation { get; set; }
    }
}
