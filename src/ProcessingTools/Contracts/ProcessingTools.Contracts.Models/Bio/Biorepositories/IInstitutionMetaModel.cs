// <copyright file="IInstitutionMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Biorepositories
{
    /// <summary>
    /// Biorepositories institution service model.
    /// </summary>
    public interface IInstitutionMetaModel
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
