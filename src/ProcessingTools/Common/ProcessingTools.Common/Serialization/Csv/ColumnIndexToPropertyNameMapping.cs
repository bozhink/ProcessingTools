// <copyright file="ColumnIndexToPropertyNameMapping.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization.Csv
{
    using System.Collections.Generic;

    /// <summary>
    /// This general class handles mapping columns to object’s properties.
    /// </summary>
    public class ColumnIndexToPropertyNameMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnIndexToPropertyNameMapping"/> class.
        /// </summary>
        public ColumnIndexToPropertyNameMapping()
        {
            this.Mapping = new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets or sets the dictionary of mappings.
        /// A dictionary holding Property Names (Key) and column indexes (Value).
        /// </summary>
        /// <remarks>
        /// Indexes should be 0-based.
        /// </remarks>
        public IDictionary<string, int> Mapping { get; set; }
    }
}
