// <copyright file="KeyValuePairModelDescription.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    /// <summary>
    /// KeyValuePair model description
    /// </summary>
    public class KeyValuePairModelDescription : ModelDescription
    {
        /// <summary>
        /// Gets or sets the model description of the key.
        /// </summary>
        public ModelDescription KeyModelDescription { get; set; }

        /// <summary>
        /// Gets or sets the model description of the value.
        /// </summary>
        public ModelDescription ValueModelDescription { get; set; }
    }
}
