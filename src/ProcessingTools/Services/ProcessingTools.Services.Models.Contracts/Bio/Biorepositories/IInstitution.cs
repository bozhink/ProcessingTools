// <copyright file="IInstitution.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories institution service model.
    /// </summary>
    public interface IInstitution
    {
        /// <summary>
        /// Gets institution code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets institution name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets institution URL.
        /// </summary>
        string Url { get; }
    }
}
