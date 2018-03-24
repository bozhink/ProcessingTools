// <copyright file="EnumTypeModelDescription.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Enum type model description
    /// </summary>
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTypeModelDescription"/> class.
        /// </summary>
        public EnumTypeModelDescription()
        {
            this.Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// Gets the descriptions of the enum values.
        /// </summary>
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}
