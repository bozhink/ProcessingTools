// <copyright file="InstitutionDetails.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Services.Data.Journals;

    /// <summary>
    /// Institution details.
    /// </summary>
    public class InstitutionDetails : IInstitutionDetails
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public ICollection<IAddress> Addresses { get; set; }
    }
}
