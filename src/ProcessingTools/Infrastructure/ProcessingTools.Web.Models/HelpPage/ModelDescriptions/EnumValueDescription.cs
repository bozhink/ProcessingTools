// <copyright file="EnumValueDescription.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Enum value description
    /// </summary>
    public class EnumValueDescription
    {
        /// <summary>
        /// Gets or sets the documentation of the enum value.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the name of the enum value.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the string value of the enum value.
        /// </summary>
        public string Value { get; set; }
    }
}
