// <copyright file="Institution.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Resources
{
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Institution.
    /// </summary>
    public class Institution : IInstitution
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }
    }
}
