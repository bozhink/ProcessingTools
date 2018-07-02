// <copyright file="Collection.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Biorepositories
{
    using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories collection service model.
    /// </summary>
    public class Collection : ICollection
    {
        /// <summary>
        /// Gets or sets collection code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets collection name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection URL.
        /// </summary>
        public string Url { get; set; }
    }
}
