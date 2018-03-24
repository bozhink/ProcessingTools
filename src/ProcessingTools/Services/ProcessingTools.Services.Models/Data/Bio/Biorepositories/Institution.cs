// <copyright file="Institution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Biorepositories
{
    using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institution service model.
    /// </summary>
    public class Institution : IInstitution
    {
        /// <summary>
        /// Gets or sets institution code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets institution name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets institution URL.
        /// </summary>
        public string Url { get; set; }
    }
}
